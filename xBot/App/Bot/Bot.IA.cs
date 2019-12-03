using System.Drawing;
using System.IO;
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
		Script currentScript;

		#region (Handle everything about botting)
		/// <summary>
		/// Start botting.
		/// </summary>
		public void Start()
		{
			if (inGame && !isBotting)
			{
				tBotting = new Thread(this.ThreadBotting);
				tBotting.Priority = ThreadPriority.AboveNormal;
				tBotting.Start();
				// ...
				Window w = Window.Get;
				w.Log("Starting bot");
				// Update GUI
				WinAPI.InvokeIfRequired(w.btnBotStart,()=> {
					w.btnBotStart.ForeColor = Color.Lime;
					w.ToolTips.SetToolTip(w.btnBotStart, "Stop Bot");
				});
			}
		}
		/// <summary>
		/// Stop botting.
		/// </summary>
		public void Stop()
		{
			if (isBotting)
			{
				tBotting.Abort();
				tBotting = null;
				// ...
				Window w = Window.Get;
				w.LogProcess("Bot stopped");

				w.Log("Stopping bot");
				// Update GUI
				WinAPI.InvokeIfRequired(w.btnBotStart, () => {
					w.btnBotStart.ForeColor = Color.Red;
					w.ToolTips.SetToolTip(w.btnBotStart, "Start Bot");
				});
				// ...
				
			}
		}
		private void ThreadBotting()
		{
			// 1.1 TOWN ?
			// 1.1.1 Do TOWN
			// 1.1.2 Wait at starting script

			// 1.2 TRAINING AREA ?
			// 1.2.1 Kill Mobs

			// 1.3 OUTSIDE TOWN AND OUTSIDE TRAINING AREA ? 
			// 1.3.1 Find the walking nearest point
			// 1.3.1.1 Follow it
			// 1.3.1.2 Go to 1.2
			// 1.3.2 Use return scroll

			Window w = Window.Get;
			Info i = Info.Get;
			while (true)
			{
				// Checking where am I ?
				w.LogProcess("Checking current location...");
				SRCoord myPosition = i.Character.GetPosition();
				currentScript = Script.GetNearestTownScript(myPosition,50);
				if (currentScript != null) {
					// I'm near town loop script
					TownLoop(currentScript);
				}
				else
				{
					// Checking training area
					SRCoord trainingPosition = w.TrainingArea_GetPosition();
					if (trainingPosition == null)
					{
						w.Log("Training area it's not activated");
						Stop();
						return;
					}
					else
					{
						// Check if I'm inside training area
						int trainingRadius = w.TrainingArea_GetRadius();
						if (myPosition.DistanceTo(trainingPosition) <= trainingRadius)
						{
							AttackLoop();
						}
						else
						{
							// Check through script
              string scriptPath = w.TrainingArea_GetScript();
							if (scriptPath != "")
							{
								w.Log("Script support not implemented yet!!");
								Stop();
								return;

								currentScript = new Script(scriptPath);
								int nearIndex = currentScript.GetNearMovement(myPosition, 50);
                if (nearIndex != -1)
								{
									currentScript.Run(nearIndex);
								}
								else
								{
									w.Log("Too far away from script!");
									Stop();
									return;
								}
							}
							else
							{
								w.Log("Script not found");
								Stop();
								return;
							}
						}
					}
				}
			}
		}
		private void TownLoop(Script town)
		{
			Window w = Window.Get;
			w.LogProcess("Starting town script [" + town.FileName + "]");
			w.Log("Town scripts not implemented yet!!");
			Stop();
			return;
		}
		private void AttackLoop()
		{
			Window w = Window.Get;
			Info i = Info.Get;

			SRCoord myPosition, trainingPosition;
			int trainingRadius;

			bool doMovement = true;
			while (true)
			{
				// Check attacking params
				trainingPosition = w.TrainingArea_GetPosition();
				if(trainingPosition == null)
				{
					w.Log("Training area it's not activated");
					Stop();
					return;
				}
				myPosition = i.Character.GetPosition();
				trainingRadius = w.TrainingArea_GetRadius();
				
				// Check movement
				if (doMovement)
				{
					// Avoid getting far away from training area
					if (myPosition.DistanceTo(trainingPosition) - trainingRadius < 50)
					{
						// Default time walking
						int timeTraveling = 3000;
						// Try to make a training movement
						if (w.Training_cbxWalkToCenter.Checked)
						{
							if (!myPosition.Equals(trainingPosition))
							{
								// Move and wait
								timeTraveling = myPosition.TimeTo(trainingPosition, i.Character.GetMovementSpeed());
								MoveTo(trainingPosition);
								w.LogProcess("Walking to center ("+ timeTraveling + "ms)...");
								WaitHandle.WaitAny(new WaitHandle[] { MonitorMobSpawnDespawnChanged, MonitorBuffChanged }, timeTraveling);
							}
						}
						else
						{
							// Random walk
							int random = rand.Next(-trainingRadius, trainingRadius);
							// Take care about where am I
							SRCoord newPosition;
							if (trainingPosition.inDungeon())
								newPosition = new SRCoord(trainingPosition.PosX + random, trainingPosition.PosY + random, trainingPosition.Region, trainingPosition.Z);
							else
								newPosition = new SRCoord(trainingPosition.PosX + random, trainingPosition.PosY + random);
							// Move and wait
							timeTraveling = myPosition.TimeTo(trainingPosition, i.Character.GetMovementSpeed());
							MoveTo(newPosition);
							w.LogProcess("Walking randomly (" + timeTraveling + "ms)...");
							WaitHandle.WaitAny(new WaitHandle[] { MonitorMobSpawnDespawnChanged, MonitorBuffChanged }, timeTraveling);
						}
						doMovement = false;
					}
					else
					{
						// Too far away from training area
						w.Log("Attacking stopped, too far away from training area");
						w.LogProcess("Far away from training area");
						return;
					}

					// Check buffs
					BuffLoop();

					if (trainingRadius > 0)
					{
						// Attacking
						SRObjectCollection mobs = i.Mobs.FindAll(m => trainingPosition.DistanceTo(m.GetPosition()) <= trainingRadius);
						SRObject mob = GetMobFiltered(mobs);
						if (mob == null)
						{
							// No mob to attack
							w.LogProcess("No mobs around to attack");
							doMovement = true;
							continue;
						}
						else
						{
							// Load skills and iterate it
							SRObject[] skillshots = w.Skills_GetSkillShots((Types.Mob)mob[SRProperty.MobType]);
							if (skillshots != null && skillshots.Length != 0)
							{
								// Try to select mob
								uint mobUniqueID = (uint)mob[SRProperty.UniqueID];
								if (WaitSelectEntity(mobUniqueID,2,250,"Selecting " + mob.Name + " (" + mob[SRProperty.MobType] + ")..."))
								{
									// Iterate skills indefinitely
									for (int k = 0;;k++)
									{
										// loop control
										if (k == skillshots.Length)
											k = 0;
										SRObject skillshot = skillshots[k];

										// Check if skill is enabled
										if (!skillshot.isCastingEnabled())
											continue;

										// Check and fix the weapon used for this skillshot
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
													if (maxWeaponChangeAttempts == 0)
													{
														w.LogProcess("Weapon changing failed!");
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
											// mob selection failed
											break;
										}
									}
								}
							}
							else
							{
								w.LogProcess("Skillshots not found");
							}
						}
					}
				}
			}
		}
		private SRObject GetMobFiltered(SRObjectCollection mobs)
		{
			SRObject mob = null;
			// Get nearest around me
			SRCoord myPosition = Info.Get.Character.GetPosition();
      double minDistance = 0;
			for (int j = 0; j < mobs.Count; j++)
			{
				double d = mobs[j].GetPosition().DistanceTo(myPosition);
				if (mob == null || d < minDistance)
				{
					minDistance = d;
					mob = mobs[j];
				}
			}
			return mob;
		}
		private void BuffLoop()
		{

		}
		private void WalkLoop()
		{

		}
		public bool WaitMovement(SRCoord position,int maxAttempts)
		{
			Info i = Info.Get;
			int attemps = 0;
			SRCoord myPosition;

			while (!(myPosition = i.Character.GetPosition()).Equals(position))
			{
				if (attemps >= maxAttempts)
					return false;
				else
					attemps++;
				// Move
				int timeWalking = myPosition.TimeTo(position, i.Character.GetSpeed());
				MoveTo(position);
        Window.Get.LogProcess("Walking to new position ("+ timeWalking + "ms)");
				Thread.Sleep(timeWalking/2);
			}
			return true;
		}
		public bool WaitSelectEntity(uint uniqueID, int maxAttempts,int delay,string logProcess = "")
		{
			int attemps = 0;
			while (SelectedEntityUID != uniqueID)
			{
				// Check if entity is near
				if (!Info.Get.SpawnList.ContainsKey(uniqueID))
					return false;

				if (attemps >= maxAttempts)
					return false;
				else
					attemps++;
				// Selecting
				PacketBuilder.SelectEntity(uniqueID);
				if (logProcess != "") Window.Get.LogProcess(logProcess);
				MonitorEntitySelected.WaitOne(delay);
			}
			return true;
		}
		#endregion
	}
}
