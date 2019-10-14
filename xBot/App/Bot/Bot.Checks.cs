using System;
using System.Collections.Generic;
using System.Timers;
using xBot.Game;
using xBot.Game.Objects;

namespace xBot.App
{
	public partial class Bot
	{
		#region (Timers, checks & cooldown controls)
		/// <summary>
		/// Cooldown timer.
		/// </summary>
		Timer tUsingHP, tUsingMP, tUsingVigor,
			tUsingUniversal, tUsingPurification,
			tUsingRecoveryKit, tUsingAbnormalPill,
			tUsingHGP,
			tCycleAutoParty;

		private void InitializeTimers()
		{
			// Preparing all neccesary timers
			tUsingHP = new Timer();
			tUsingMP = new Timer();
			tUsingVigor = new Timer();
			tUsingUniversal = new Timer();
			tUsingPurification = new Timer();
			tUsingRecoveryKit = new Timer();
			tUsingAbnormalPill = new Timer();
			tUsingHGP = new Timer();
			tCycleAutoParty = new Timer();

			// A second is enought for any potion cooldown
			tUsingHP.Interval = tUsingMP.Interval = tUsingVigor.Interval =
			tUsingUniversal.Interval = tUsingPurification.Interval =
			tUsingRecoveryKit.Interval = tUsingAbnormalPill.Interval;

			tCycleAutoParty.Interval = 5000;

			// Callbacks
			tUsingHP.Elapsed += CheckUsingHP;
			tUsingMP.Elapsed += CheckUsingMP;
			tUsingVigor.Elapsed += CheckUsingVigor;
			tUsingUniversal.Elapsed += CheckUsingUniversal;
			tUsingPurification.Elapsed += CheckUsingPurification;
			tCycleAutoParty.Elapsed += CheckAutoParty;
			tUsingRecoveryKit.Elapsed += CheckUsingRecoveryKit;
			tUsingAbnormalPill.Elapsed += CheckUsingAbnormalPill;
			tUsingHGP.Elapsed += CheckUsingHGP;
		}
		public void CheckUsingHP()
		{
			if (!tUsingHP.Enabled)
				CheckUsingHP(tUsingHP,null);
		}
		private void CheckUsingHP(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUseHP.Checked || w.Character_cbxUseHPGrain.Checked)
				{
					byte useHP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseHP, () => {
						useHP = byte.Parse(w.Character_tbxUseHP.Text);
					});
					if (i.Character.GetHPPercent() <= useHP)
					{
						byte slot = 0;
						if (w.Character_cbxUseHPGrain.Checked && FindItem(3, 1, 1, ref slot, "_SPOTION_")
							|| w.Character_cbxUseHP.Checked && FindItem(3, 1, 1, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot);
							tUsingHP.Start();
						}
					}
				}
			}
		}
		public void CheckUsingMP()
		{
			if (!tUsingMP.Enabled)
				CheckUsingMP(tUsingMP, null);
		}
		private void CheckUsingMP(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseMP.Checked || w.Character_cbxUseMPGrain.Checked)
				{
					byte useMP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseMP, () =>
					{
						useMP = byte.Parse(w.Character_tbxUseMP.Text);
					});
					if (i.Character.GetMPPercent() <= useMP)
					{
						byte slot = 0;
						if (w.Character_cbxUseMPGrain.Checked && FindItem(3, 1, 2, ref slot, "_SPOTION_")
							|| w.Character_cbxUseMP.Checked && FindItem(3, 1, 2, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot);
							tUsingMP.Start();
						}
					}
				}
			}
		}
		public void CheckUsingVigor()
		{
			if (!tUsingVigor.Enabled)
				CheckUsingVigor(tUsingVigor, null);
		}
		private void CheckUsingVigor(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseHPVigor.Checked || w.Character_cbxUseMPVigor.Checked)
				{
					byte usePercent = 0;
					WinAPI.InvokeIfRequired(w.Character_tbxUseHPVigor, () => {
						usePercent = byte.Parse(w.Character_tbxUseHPVigor.Text);
					});
					// Check hp %
					if (i.Character.GetHPPercent() <= usePercent)
					{
						byte slot = 0;
						if (FindItem(3, 1, 3, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot);
							tUsingVigor.Start();
						}
					}
					else
					{
						// Check mp %
						WinAPI.InvokeIfRequired(w.Character_tbxUseMPVigor, () => {
							usePercent = byte.Parse(w.Character_tbxUseMPVigor.Text);
						});
						if (i.Character.GetMPPercent() <= usePercent)
						{
							byte slot = 0;
							if (FindItem(3, 1, 3, ref slot))
							{
								PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot);
								tUsingVigor.Start();
							}
						}
					}
				}
			}
		}
		public void CheckUsingUniversal()
		{
			if (!tUsingUniversal.Enabled)
				CheckUsingUniversal(tUsingUniversal, null);
		}
		public void CheckUsingUniversal(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePillUniversal.Checked)
				{
					Types.BadStatus status = (Types.BadStatus)i.Character[SRProperty.BadStatusFlags];
					if (status.HasFlag(Types.BadStatus.Freezing
						| Types.BadStatus.ElectricShock 
						| Types.BadStatus.Burn 
						| Types.BadStatus.Poisoning 
						| Types.BadStatus.Zombie))
					{
						byte slot = 0;
						if (FindItem(3, 2, 6, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot);
							tUsingUniversal.Start();
						}
					}
				}
			}
		}
		public void CheckUsingPurification()
		{
			if (!tUsingPurification.Enabled)
				CheckUsingPurification(tUsingPurification, null);
		}
		private void CheckUsingPurification(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePillPurification.Checked)
				{
					Types.BadStatus status = (Types.BadStatus)i.Character[SRProperty.BadStatusFlags];
					if (status.HasFlag( Types.BadStatus.Dull 
						| Types.BadStatus.Fear 
						| Types.BadStatus.ShortSight
						| Types.BadStatus.Bleed
						| Types.BadStatus.Darkness
						| Types.BadStatus.Disease
						| Types.BadStatus.Confusion
						| Types.BadStatus.Decay 
						| Types.BadStatus.Weaken 
						| Types.BadStatus.Impotent
						| Types.BadStatus.Division
						| Types.BadStatus.Panic
						| Types.BadStatus.Combustion 
						| Types.BadStatus.Hidden))
					{
						byte slot = 0;
						if (FindItem(3, 2, 1, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot);
							tUsingPurification.Start();
						}
					}
				}
			}
		}
		public void CheckAutoParty()
		{
			if (!tCycleAutoParty.Enabled)
				CheckAutoParty(tCycleAutoParty, null);
		}
		private void CheckAutoParty(object sender, ElapsedEventArgs e)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if (w.Party_cbxInviteAll.Checked)
			{
				if (i.PlayersNear.Count > 0)
				{
					if (inParty)
					{
						Dictionary<string, SRObject> PlayersNearWithNoParty = new Dictionary<string, SRObject>(i.PlayersNear);
						// Remove players nears with party
						for (byte j = 0; j < i.PartyList.Count; j++)
						{
							string PlayerName = i.PartyList[j].Name.ToUpper();
							if (PlayersNearWithNoParty.ContainsKey(PlayerName)){
								PlayersNearWithNoParty.Remove(PlayerName);
							}
						}
						if (PlayersNearWithNoParty.Count == 0)
							return;

						// Check invitations setup
						if (!w.Party_cbxInviteOnlyPartySetup.Checked
							|| PartySetupFlags == GetPartySetup())
						{
							int memberMax = PartySetupFlags.HasFlag(Types.PartySetup.ExpShared) ? 8 : 4;
							if (i.PartyList.Count < memberMax)
							{
								List<SRObject> randomizePlayers = new List<SRObject>(PlayersNearWithNoParty.Values);
								PacketBuilder.InviteToParty((uint)randomizePlayers[rand.Next(randomizePlayers.Count)][SRProperty.UniqueID]);
								tCycleAutoParty.Start();
							}
						}
					}
					else
					{
						List<SRObject> randomizePlayers = new List<SRObject>(i.PlayersNear.Values);
						PacketBuilder.CreateParty((uint)randomizePlayers[rand.Next(randomizePlayers.Count)][SRProperty.UniqueID], GetPartySetup());
						tCycleAutoParty.Start();
					}
				}
				else
				{
					tCycleAutoParty.Start();
				}
			}
			else if(w.Party_cbxInvitePartyList.Checked)
			{
				if (i.PlayersNear.Count > 0)
				{
					List<string> PlayersToInvite = new List<string>();
					WinAPI.InvokeIfRequired(w.Party_lstvPartyList, () => {
						for (int j = 0; j < w.Party_lstvPartyList.Items.Count; j++)
						{
							PlayersToInvite.Add(w.Party_lstvPartyList.Items[j].Name);
						}
					});
					// Remove if are in party already
					for (int j = 0; j < PlayersToInvite.Count; j++)
					{
						for (int k = 0; k < i.PartyList.Count; k++)
						{
							if (PlayersToInvite[j].Equals(i.PartyList[k].Name, StringComparison.OrdinalIgnoreCase))
							{
								PlayersToInvite.RemoveAt(j--);
								break;
							}
						}
					}
					if(PlayersToInvite.Count > 0)
					{
						// Shuffle and check the party list with near players
						PlayersToInvite = WinAPI.GetShuffle(PlayersToInvite, rand);
						SRObject PlayerToInvite = null;
						for (int j = 0; j < PlayersToInvite.Count; j++)
						{
							if (i.PlayersNear.TryGetValue(PlayersToInvite[j],out PlayerToInvite)){
								break;
							}
						}
						if (PlayerToInvite != null)
						{
							if (inParty)
							{
								if (!w.Party_cbxInviteOnlyPartySetup.Checked
									|| PartySetupFlags == GetPartySetup())
								{
									int maxMembers = PartySetupFlags.HasFlag(Types.PartySetup.ExpShared) ? 8 : 4;
									if (i.PartyList.Count < maxMembers)
									{
										PacketBuilder.InviteToParty((uint)PlayerToInvite[SRProperty.UniqueID]);
										tCycleAutoParty.Start();
									}
								}
							}
							else
							{
								PacketBuilder.CreateParty((uint)PlayerToInvite[SRProperty.UniqueID], GetPartySetup());
								tCycleAutoParty.Start();
							}
						}
						else
						{
							// No players to invite, try later
							tCycleAutoParty.Start();
						}
					}
				}
				else
				{
					// No players near to invite, try later
					tCycleAutoParty.Start();
				}
			}
		}
		public bool CheckPartyLeaving()
		{
			Window w = Window.Get;
			if (w.Party_cbxLeavePartyNoneLeader.Checked)
			{
				Info i = Info.Get;
				SRObject[] members = i.PartyList.ToArray();

				bool found = false;
				WinAPI.InvokeIfRequired(w.Party_lstvLeaderList, () => {
					for (byte j = 0; j < i.PartyList.Count; j++){
						if (w.Party_lstvLeaderList.Items.ContainsKey(i.PartyList[j].Name.ToUpper()))
						{
							found = true;
							break;
            }
					}
				});
				if (!found)
				{
					PacketBuilder.LeaveParty();
					return true;
				}
			}
			return false;
		}
		public void CheckPartyMatchAutoReform()
		{
			PacketBuilder.RequestPartyMatch();
		}
		public void CheckUsingRecoveryKit()
		{
			if (!tUsingRecoveryKit.Enabled)
			{
				CheckUsingRecoveryKit(tUsingRecoveryKit, null);
			}
		}
		private void CheckUsingRecoveryKit(object sender, ElapsedEventArgs e)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			// Checking pet using recovery kit by priority
			SRObject pet = null;

			// Vehicle or Transport
			if (w.Character_cbxUseTransportHP.Checked){
				pet = i.GetPets().Find(p => p.ID4 == 1 || p.ID4 == 2);
				// Check % if there is at least one pet
				if(pet != null)
				{
					byte useHP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseTransportHP, () => {
						useHP = byte.Parse(w.Character_tbxUseTransportHP.Text);
					});
					if (pet.GetHPPercent() <= useHP)
					{
						byte slot = 0;
						if (FindItem(3, 1, 4, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot,(uint)pet[SRProperty.UniqueID]);
							tUsingRecoveryKit.Start();
						}
						return; // Avoid checking other pets
					}
				}
			}

			// Attacking pet
			if (w.Character_cbxUsePetHP.Checked){
				pet = i.GetPets().Find(p => p.ID4 == 3);
				// Check % if there is at least one pet
				if(pet != null)
				{
					byte useHP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUsePetHP, () => {
						useHP = byte.Parse(w.Character_tbxUsePetHP.Text);
					});
					if (pet.GetHPPercent() <= useHP)
					{
						byte slot = 0;
						if (FindItem(3, 1, 4, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot,(uint)pet[SRProperty.UniqueID]);
							tUsingRecoveryKit.Start();
						}
						return; // Avoid checking other pets
					}
				}
			}
		}
		public void CheckUsingAbnormalPill()
		{
			if (!tUsingAbnormalPill.Enabled)
				CheckUsingAbnormalPill(tUsingAbnormalPill, null);
		}
		private void CheckUsingAbnormalPill(object sender, ElapsedEventArgs e)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if (w.Character_cbxUsePetsPill.Checked)
			{
				// Checking pet bad status for using abnormal
				SRObject pet = null;
				// As priority pet transport
				pet = i.GetPets().Find(p =>
					(p.ID4 == 1 || p.ID4 == 2)
					&& p.Contains(SRProperty.BadStatusFlags)
					&& !((Types.BadStatus)p[SRProperty.BadStatusFlags]).HasFlag(Types.BadStatus.None)
				);
				if (pet == null)
				{
					pet = i.GetPets().Find(p =>
						p.ID4 == 3
						&& p.Contains(SRProperty.BadStatusFlags)
						&& !((Types.BadStatus)p[SRProperty.BadStatusFlags]).HasFlag(Types.BadStatus.None)
					);
				}
				// At least one pet has bad status
				if (pet != null)
				{
					byte slot = 0; // dummy
					if (FindItem(3, 2, 7, ref slot))
					{
						PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot, (uint)pet[SRProperty.UniqueID]);
						tUsingAbnormalPill.Start();
					}
				}
			}
		}
		public void CheckUsingHGP()
		{
			if (!tUsingHGP.Enabled || tUsingHGP.Interval != 1000)
			{
				CheckUsingHGP(tUsingHGP, null);
			}
		}
		private void CheckUsingHGP(object sender, ElapsedEventArgs e)
		{
			Window w = Window.Get;
			if (w.Character_cbxUsePetHGP.Checked)
			{
				Info i = Info.Get;
        SRObject pet = i.GetPets().Find(p => p.ID4 == 3);
				if(pet != null)
				{
					byte usePercent = 0;
					WinAPI.InvokeIfRequired(w.Character_tbxUsePetHGP, () => {
						usePercent = byte.Parse(w.Character_tbxUsePetHGP.Text);
					});
					// Check hgp %
					int HGPPercent = (int)(((ushort)pet[SRProperty.HGP])*0.01); // 10000 = 100%
          if (HGPPercent <= usePercent)
					{
						byte slot = 0;
						if (FindItem(3, 1, 9, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRProperty.Inventory])[slot], slot, (uint)pet[SRProperty.UniqueID]);
							WinAPI.ResetTimer(ref tUsingHGP, 1000);
							return;
						}
					}
					WinAPI.ResetTimer(ref tUsingHGP, 300000); // 1% decrease : -100 HGP every 5min
				}
			}
		}
		#endregion
	}
}
