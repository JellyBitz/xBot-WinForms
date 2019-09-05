using System.Collections.Generic;
using System.Timers;
using xBot.Game;

namespace xBot
{
	public partial class Bot
	{
		#region (Checking stuffs to do some action)
		public void CheckUsingHP()
		{
			if (!tUsingHP.Enabled)
				CheckUsingHP(tUsingHP,null);
		}
		private void CheckUsingHP(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUseHP.Checked || w.Character_cbxUseHPGrain.Checked)
				{
					byte useHP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseHP, () => {
						useHP = byte.Parse(w.Character_tbxUseHP.Text);
					});
					if ((int)i.Character.GetHPPercent() <= useHP)
					{
						byte slot = 0;
						if (w.Character_cbxUseHPGrain.Checked && FindItem(3, 1, 1, ref slot, "_SPOTION_")
							|| w.Character_cbxUseHP.Checked && FindItem(3, 1, 1, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
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
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseMP.Checked || w.Character_cbxUseMPGrain.Checked)
				{
					byte useMP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseMP, () =>
					{
						useMP = byte.Parse(w.Character_tbxUseMP.Text);
					});
					if ((int)i.Character.GetMPPercent() <= useMP)
					{
						byte slot = 0;
						if (w.Character_cbxUseMPGrain.Checked && FindItem(3, 1, 2, ref slot, "_SPOTION_")
							|| w.Character_cbxUseMP.Checked && FindItem(3, 1, 2, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
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
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseHPVigor.Checked || w.Character_cbxUseMPVigor.Checked)
				{
					byte usePercent = 0;
					WinAPI.InvokeIfRequired(w.Character_tbxUseHPVigor, () => {
						usePercent = byte.Parse(w.Character_tbxUseHPVigor.Text);
					});
					// Check hp %
					if ((int)i.Character.GetHPPercent() <= usePercent)
					{
						byte slot = 0;
						if (FindItem(3, 1, 3, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingVigor.Start();
						}
					}
					else
					{
						// Check mp %
						WinAPI.InvokeIfRequired(w.Character_tbxUseMPVigor, () => {
							usePercent = byte.Parse(w.Character_tbxUseMPVigor.Text);
						});
						if ((int)i.Character.GetMPPercent() <= usePercent)
						{
							byte slot = 0;
							if (FindItem(3, 1, 3, ref slot))
							{
								PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
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
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePillUniversal.Checked)
				{
					Types.BadStatus status = (Types.BadStatus)i.Character[SRAttribute.BadStatusType];
					if (status.HasFlag(Types.BadStatus.Freezing
						| Types.BadStatus.Frostbite
						| Types.BadStatus.ElectricShock
						| Types.BadStatus.Burn
						| Types.BadStatus.Poisoning
						| Types.BadStatus.Zombie))
					{
						byte slot = 0;
						if (FindItem(3, 2, 6, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
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
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePillPurification.Checked)
				{
					Types.BadStatus status = (Types.BadStatus)i.Character[SRAttribute.BadStatusType];
					if (status.HasFlag(Types.BadStatus.Bleed
						| Types.BadStatus.Decay
						| Types.BadStatus.Weaken
						| Types.BadStatus.Impotent
						| Types.BadStatus.Division))
					{
						byte slot = 0;
						if (FindItem(3, 2, 1, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingPurification.Start();
						}
					}
				}
			}
		}
		public void CheckAutoParty()
		{
			if (!tAutoParty.Enabled)
				CheckAutoParty(tAutoParty, null);
		}
		private void CheckAutoParty(object sender, ElapsedEventArgs e)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if (w.Party_cbxInviteAll.Checked)
			{
				List<SRObject> nearPlayers = i.GetPlayers();
				if (nearPlayers.Count > 0)
				{
					if (hasParty)
					{
						// Remove party members (not trying invite them again)
						SRObject[] partyPlayers = i.PartyList.ToArray();
						foreach (SRObject member in partyPlayers)
						{
							SRObject playerFound = nearPlayers.Find(p => (string)p[SRAttribute.Name] == (string)member[SRAttribute.Name]);
							if (playerFound != null)
								nearPlayers.Remove(playerFound);
						}
						// Invite players
						if (!w.Party_cbxInviteOnlyPartySetup.Checked
							|| w.Party_cbxInviteOnlyPartySetup.Checked && PartySetupType == GetPartySetup())
						{
							int maxMembers = ((Types.PartySetup)PartySetupType).HasFlag(Types.PartySetup.ExpShared) ? 8 : 4;
							if (i.PartyList.Count < maxMembers)
							{
								if (nearPlayers.Count != 0)
									// Randomize the character invitation to avoid stuck at only one
									PacketBuilder.InviteToParty((uint)nearPlayers[rand.Next(nearPlayers.Count)][SRAttribute.UniqueID], (byte)PartySetupType);
								// If no players to invite, try later
								tAutoParty.Start();
							}
						}
					}
					else
					{
						PacketBuilder.InviteToParty((uint)nearPlayers[rand.Next(nearPlayers.Count)][SRAttribute.UniqueID], GetPartySetup());
						tAutoParty.Start();
					}
				}
				else
				{
					// If no players to invite, try later
					tAutoParty.Start();
				}
				// Has no sense continue checking
				return;
			}
			if (w.Party_cbxInvitePartyList.Checked)
			{
				// Copy the current party list to invite players
				List<string> invitePlayers = new List<string>();
				WinAPI.InvokeIfRequired(w.Party_lstvPartyList, () => {
					for (int j = 0; j < w.Party_lstvPartyList.Items.Count; j++)
						invitePlayers.Add(w.Party_lstvPartyList.Items[j].Name);
				});
				if(invitePlayers.Count> 0)
				{
					List<SRObject> nearPlayers = i.GetPlayers();
					if (nearPlayers.Count > 0)
					{
						// Randomized to avoid invite the same player always
						invitePlayers = WinAPI.GetShuffle(invitePlayers, rand);

						// Remove party members (not trying invite them again)
						List<SRObject> partyPlayers = new List<SRObject>(i.PartyList.ToArray());
						int k = 0;
						while (k < invitePlayers.Count)
						{
							// Look for same nick at the same party
							SRObject playerFound = partyPlayers.Find(p => invitePlayers[k].Equals((string)p[SRAttribute.Name], System.StringComparison.OrdinalIgnoreCase));
							if (playerFound != null)
							{
								partyPlayers.Remove(playerFound);
								invitePlayers.RemoveAt(k);
							}
							else
							{
								k++;
							}
						}
						// Check if the player from party list is near to invite him 
						SRObject invitePlayer = null;
						foreach (string player in invitePlayers)
						{
							invitePlayer = nearPlayers.Find(p => player.Equals((string)p[SRAttribute.Name], System.StringComparison.OrdinalIgnoreCase));
							if (invitePlayer != null)
								break;
						}

						if (hasParty)
						{
							// Invite players
							if (!w.Party_cbxInviteOnlyPartySetup.Checked
								|| w.Party_cbxInviteOnlyPartySetup.Checked && PartySetupType == GetPartySetup())
							{
								int maxMembers = ((Types.PartySetup)PartySetupType).HasFlag(Types.PartySetup.ExpShared) ? 8 : 4;
								if (i.PartyList.Count < maxMembers)
								{
									if (invitePlayer != null)
										PacketBuilder.InviteToParty((uint)invitePlayer[SRAttribute.UniqueID], (byte)PartySetupType);
									// If none player to invite, try later
									tAutoParty.Start();
								}
							}
						}
						else
						{
							if (invitePlayer != null)
								PacketBuilder.InviteToParty((uint)invitePlayer[SRAttribute.UniqueID], GetPartySetup());
							// If no player to invite, try later
							tAutoParty.Start();
						}
					}
					else
					{
						// If no players to invite, try later
						tAutoParty.Start();
					}
				}
			}
		}
		public bool CheckPartyLeaving()
		{
			Window w = Window.Get;
			if (w.Party_cbxLeavePartyNoneLeader.Checked)
			{
				Info i = Info.Get;

				bool NotFound = true;
				SRObject[] players = i.PartyList.ToArray();

				WinAPI.InvokeIfRequired(w.Party_lstvLeaderList, () => {
					foreach (SRObject member in players)
					{
						if (w.Party_lstvLeaderList.Items.ContainsKey(((string)member[SRAttribute.Name]).ToLower()))
						{
							NotFound = false;
							break;
						}
					}
				});
				if (NotFound)
				{
					PacketBuilder.LeaveParty();
					return true;
				}
			}
			return false;
		}
		public void CheckUsingPetHP()
		{
			if (!tUsingPetHP.Enabled)
				CheckUsingPetHP(tUsingPetHP, null);
		}
		private void CheckUsingPetHP(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			SRObject pet = i.GetPets().Find(p => p.ID4 == 3);
			if(pet != null)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePetHP.Checked)
				{
					byte useHP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUsePetHP, () => {
						useHP = byte.Parse(w.Character_tbxUsePetHP.Text);
					});
					if ((int)pet.GetHPPercent() <= useHP)
					{
						byte slot = 0;
						if (FindItem(3, 1, 4, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot,(uint)pet[SRAttribute.UniqueID]);
							tUsingPetHP.Start();
						}
					}
				}
			}
		}
		public void CheckUsingTransportHP()
		{
			if (!tUsingTransportHP.Enabled)
				CheckUsingTransportHP(tUsingTransportHP, null);
		}
		private void CheckUsingTransportHP(object sender, ElapsedEventArgs e)
		{
			Info i = Info.Get;
			SRObject transport = i.GetPets().Find(p => p.ID4 == 1 || p.ID4 == 2);
			if(transport != null)
			{
				Window w = Window.Get;
				if (w.Character_cbxUseTransportHP.Checked)
				{
					byte useHP = 0; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseTransportHP, () => {
						useHP = byte.Parse(w.Character_tbxUseTransportHP.Text);
					});
					if ((int)transport.GetHPPercent() <= useHP)
					{
						byte slot = 0;
						if (FindItem(3, 1, 4, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot,(uint)transport[SRAttribute.UniqueID]);
							tUsingTransportHP.Start();
						}
					}
				}
			}
		}
		#endregion
	}
}
