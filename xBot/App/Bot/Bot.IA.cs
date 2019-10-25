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
									int travelTime = myPosition.TimeTo(trainingPosition, i.Character.GetSpeed() * 0.1 / 1000);
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
								int travelTime = myPosition.TimeTo(newPosition, i.Character.GetSpeed() * 0.1 / 1000);
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
									byte skipMob = 4; // Check max. 4 times for skip mob (max. 2 seconds actually)
									while (EntitySelected != mobUniqueID && skipMob > 0)
									{
										w.LogProcess("Selecting " + mob.Name + " (" + (Types.Mob)mob[SRProperty.MobType] + ")...");
										PacketBuilder.SelectEntity(mobUniqueID);
										skipMob--;
										// Wait at least 500ms to try checking again
										MonitorEntitySelected.WaitOne(500);
									}
									if (skipMob != 0)
									{
										w.LogProcess("Attacking " + mob.Name + "...");
										for (int k = 0; k <= skillshots.Length; k++)
										{
											// loop skills again
											if (k == skillshots.Length)
												k = 0;

											// Check if skill is enabled
											if (!skillshots[k].isCastingEnabled())
												continue;
											// Check if mob is alive
											if (i.Mobs.ContainsKey(mobUniqueID))
											{
												w.LogProcess("Casting skill " + skillshots[k].Name + " (" + (int)skillshots[k][SRProperty.Casttime] + "ms)...");
												PacketBuilder.AttackTarget(mobUniqueID, skillshots[k].ID);
												if (MonitorSkillCast.WaitOne(500))
												{
													// Skill casted, create character cooldown
													Thread.Sleep((int)skillshots[k][SRProperty.Casttime]);
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
		/// <summary>
		/// Stop botting.
		/// </summary>
		public void Stop()
		{
			if(isBotting)
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
		#endregion
	}
}
