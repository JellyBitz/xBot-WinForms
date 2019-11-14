using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using xBot.Game;
using xBot.Game.Objects;

namespace xBot.App
{
	public partial class Bot
	{
		public bool isBotting { get { return tBotting != null; } }
		/// <summary>
		/// Thread controlling all botting actions.
		/// </summary>
		Thread tBotting;
		#region (Handle everything about botting)
		/// <summary>
		/// Start botting.
		/// </summary>
		public void Start()
		{
			if (inGame && !isBotting)
			{
				Window w = Window.Get;
				w.Log("Starting bot");
				// Update GUI
				WinAPI.InvokeIfRequired(w.btnBotStart,()=> {
					w.btnBotStart.ForeColor = Color.Lime;
					w.ToolTips.SetToolTip(w.btnBotStart, "Stop Bot");
				});
				// ...
				tBotting = new Thread(this.ThreadBotting);
				tBotting.Priority = ThreadPriority.AboveNormal;
				tBotting.Start();
			}
		}
		/// <summary>
		/// Stop botting.
		/// </summary>
		public void Stop()
		{
			if (isBotting)
			{
				Window w = Window.Get;
				w.Log("Stopping bot");
				// Update GUI
				WinAPI.InvokeIfRequired(w.btnBotStart, () => {
					w.btnBotStart.ForeColor = Color.Red;
					w.ToolTips.SetToolTip(w.btnBotStart, "Start Bot");
				});
				// ...
				tBotting.Abort();
				tBotting = null;
				w.LogProcess("Bot stopped");
			}
		}
		private void ThreadBotting()
		{
			Window w = Window.Get;
			Info i = Info.Get;
			w.LogProcess("Checking current location...");
			// CHECK WHERE AM I?

			// 1.1 TOWN ?
			// 1.1.1 Do TOWN
			// 1.1.2 Wait at starting scriptu

			// 1.2 TRAINING AREA ?
			// 1.2.1 Kill Mobs

			// 1.3 OUTSIDE TOWN AND TRAINING AREA ? 
			// 1.3.1 Find the walking point
			// 1.3.1.1 Follow it
			// 1.3.1.2 Go to 1.2
			// 1.3.2 Use return scroll
			bool moveToTrainingArea = true;
			SRCoord myPosition = i.Character.GetPosition();
			switch (myPosition.GetLocation())
			{
				case SRLocation.WorldMap:
					// Check training area selected
					SRCoord trainingPosition = w.TrainingArea_GetPosition();
					if (trainingPosition == null)
					{
						w.Log("Training area has not been activated");
						Stop();
						return;
					}
					// Check if I'm out training area but near it (max 75mts)
					myPosition = i.Character.GetPosition();
					int maxRadius = w.TrainingArea_GetRadius();
					if (myPosition.DistanceTo(trainingPosition) - maxRadius < 75)
					{
						if(moveToTrainingArea)
						{
							// Try to make a training movement
							if (w.Training_cbxWalkToCenter.Checked)
							{
								if(!myPosition.Equals(trainingPosition)){
									MoveTo(trainingPosition);

									// TEST
									long travelTime = myPosition.TimeTo(trainingPosition, i.Character.GetSpeed() * 0.1 / 1000);
									w.Log("Walking to training area (" + travelTime + ")" + myPosition + "->" + trainingPosition + ")...");
								}
							}
							else
							{
								// Random walk
								int random = rand.Next(-maxRadius, maxRadius);
								// Take care about where am I
								SRCoord newPosition;
								if (trainingPosition.inDungeon())
									newPosition = new SRCoord(trainingPosition.PosX + random, trainingPosition.PosY + random, trainingPosition.Region, trainingPosition.Z);
								else
									newPosition = new SRCoord(trainingPosition.PosX + random, trainingPosition.PosY + random);
								MoveTo(newPosition);
								
								// TEST
								long travelTime = myPosition.TimeTo(newPosition, i.Character.GetSpeed() * 0.1 / 1000);
								w.Log("Walking to training area (" + travelTime + ")" + myPosition + "->" + newPosition + ")...");
							}
							moveToTrainingArea = false;
							MonitorMobSpawnDespawnOrBuffChanged.WaitOne(2500);
						}

						// Start training stuffs
						// Check Buffs
						// w.LogProcess("Buffing...");

						// Check mobs
						if (maxRadius > 0)
						{
							myPosition = i.Character.GetPosition();
							// Get mobs inside the radius
							SRObjectCollection mobs = i.Mobs.FindAll(m => trainingPosition.DistanceTo(m.GetPosition()) < maxRadius);
							// Filter mob
							SRObject mob = null;
							double minDistance = 0;
							// Get nearest around me
							for (int j = 0; j < mobs.Count; j++)
							{
								double d = mobs[j].GetPosition().DistanceTo(myPosition);
								if (mob == null || d < minDistance)
								{
									minDistance = d;
									mob = mobs[j];
								}
							}
							if (mob != null)
							{
								// Load skills and iterate it
								SRObject[] skillshots = w.Skills_GetSkillShots((Types.Mob)mob[SRProperty.MobType]);
								if (skillshots != null && skillshots.Length != 0)
								{
									uint mobUniqueID = (uint)mob[SRProperty.UniqueID];
									byte maxEntitySelectAttempts = 5; // Check max. 4 times to skip the mob (max. 1 seconds actually)
									while (EntitySelected != mobUniqueID && maxEntitySelectAttempts > 0)
									{
										w.LogProcess("Selecting " + mob.Name + " (" + (Types.Mob)mob[SRProperty.MobType] + ")...");
										PacketBuilder.SelectEntity(mobUniqueID);
										maxEntitySelectAttempts--;
										// Wait at least 250ms to try checking again
										MonitorEntitySelected.WaitOne(250);
									}
									if (maxEntitySelectAttempts != 0)
									{
										// Entity successfully selected
										for (int k = 0; k <= skillshots.Length; k++)
										{
											// loop skills again
											if (k == skillshots.Length)
												k = 0;
											SRObject skillshot = skillshots[k];

											// Check if skill is enabled
											if (!skillshot.isCastingEnabled())
												continue;
											// Check and fix the weapon
											Types.Weapon myWeapon = GetWeaponUsed();
											if (skillshot.ID == 1)
											{
												// Common attack, fix the basic skill
												if (myWeapon != Types.Weapon.None)
												{
													skillshot = new SRObject(i.GetCommonAttack(myWeapon), SRType.Skill);
													skillshot.Name = "Common Attack";
                        }
											}
											else
											{
												// Check the required weapon
												Types.Weapon weaponRequired = (Types.Weapon)skillshot[SRProperty.WeaponRequired01];
												w.LogProcess("Checking weapon required (" + weaponRequired + ")...");
												if (myWeapon != weaponRequired)
												{
													SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
													// Check the first 4 slots from inventory
													int slotInventory = inventory.FindIndex(item => item.ID2 == 1 && item.ID3 == 6 && item.ID3 == (byte)weaponRequired, 13, 16);
													if (slotInventory != -1)
													{
														w.LogProcess("Changing weapon (" + myWeapon + ")...");
														// Try to change it
														byte maxWeaponChangeAttempts = 5; // Check max. 4 times to skip the mob (max. 1 seconds actually)
														while (myWeapon != weaponRequired && maxWeaponChangeAttempts > 0)
														{
															PacketBuilder.MoveItem((byte)slotInventory, 6, Types.InventoryItemMovement.InventoryToInventory);
															maxWeaponChangeAttempts--;
															MonitorWeaponChanged.WaitOne(250);
															myWeapon = GetWeaponUsed();
                            }
														if(maxWeaponChangeAttempts == 0)
														{
															w.LogProcess("Weapon failed to change...");
															continue;
														}
													}
													else
													{
														w.LogProcess("Weapon required not found (" + myWeapon + ")...");
														continue;
													}
												}
												MonitorWeaponChanged.WaitOne(250);
												myWeapon = GetWeaponUsed();
											}

											// Check if mob is alive
											if (i.Mobs.ContainsKey(mobUniqueID))
											{
												w.LogProcess("Casting skill " + skillshot.Name + " (" + (int)skillshot[SRProperty.Casttime] + "ms)...");
												PacketBuilder.AttackTarget(mobUniqueID, skillshot.ID);
												if (MonitorSkillCast.WaitOne(500))
												{
													// Skill casted, create character cooldown
													Thread.Sleep((int)skillshot[SRProperty.Casttime]);
												}
												else
												{
													// Timeout: Skill not casted
													if (!i.Mobs.ContainsKey(mobUniqueID))
													{
														// Mob it's dead?
														break;
													}
													else
													{
														// Recast skillshot
														k--;
														continue;
													}
												}
											}
											else
											{
												break;
											}
										}
									}
								}
							}
							else
							{
								w.LogProcess("No mobs around...");
								// Wait at least any mob change to check it out
								if(MonitorMobSpawnDespawnOrBuffChanged.WaitOne(2500)){
									// Timeout, make a movement
									moveToTrainingArea = true;
								}
							}
						}
						else
						{
							w.Log("Radius not set. Buffing support not implemented yet!");
							Stop();
							return;
						}
					}
					else
					{
						w.LogProcess("Otuside training area..");
						// Check script
						string scriptPath = w.TrainingArea_GetScript();
						if (w.TrainingArea_GetScript() != "")
						{
							w.Log("Script support not implemented yet!");
							// TO DO:
							Stop();
							return;
							// Read script path
							// Try to find the nearest point to my current position
						}else{
							w.Log("Script not found");
							Stop();
							return;
						}
					}
					goto case SRLocation.WorldMap;
			}
		}
		private void WaitSelectEntity(uint uniqueID,byte maxAttempts)
		{
			
		}
		#endregion
	}
}
