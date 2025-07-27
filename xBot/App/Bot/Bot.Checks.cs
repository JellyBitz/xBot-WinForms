using System;
using System.Collections.Generic;
using System.Timers;
using xBot.Game;
using xBot.Game.Objects;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Common;
using System.Globalization;
using System.Linq;
using xBot.Game.Objects.Item;

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

        
        public bool IsSwitchingWeapon { get; set; }
        public Timer TimerSwitchingWeapon { get; set; } = null;


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

            // Manual reset
            tUsingHP.AutoReset = tUsingMP.AutoReset = tUsingVigor.AutoReset =
            tUsingUniversal.AutoReset = tUsingPurification.AutoReset =
            tUsingRecoveryKit.AutoReset = tUsingAbnormalPill.AutoReset =
            tCycleAutoParty.AutoReset = false;

            // A second is enought for any potion cooldown
            tUsingHP.Interval = tUsingMP.Interval = tUsingVigor.Interval =
            tUsingUniversal.Interval = tUsingPurification.Interval =
            tUsingRecoveryKit.Interval = tUsingAbnormalPill.Interval = 1000;

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
                CheckUsingHP(tUsingHP, null);
        }
        private void CheckUsingHP(object sender, ElapsedEventArgs e)
        {
            if (InfoManager.Character.LifeStateType == SRModel.LifeState.Alive)
            {
                Window w = Window.Get;
                if (w.Character_cbxUseHP.Checked || w.Character_cbxUseHPGrain.Checked)
                {
                    byte useHP = 0; // dummy
                    w.Character_tbxUseHP.InvokeIfRequired(() => {
                        useHP = byte.Parse(w.Character_tbxUseHP.Text);
                    });
                    if (InfoManager.Character.GetHPPercent() <= useHP)
                    {
                        byte slot = 0;
                        if (w.Character_cbxUseHPGrain.Checked && FindItem(3, 1, 1, ref slot, "_SPOTION_")
                            || w.Character_cbxUseHP.Checked && FindItem(3, 1, 1, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot);
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
            if (InfoManager.Character.LifeStateType == SRModel.LifeState.Alive)
            {
                Window w = Window.Get;
                if (w.Character_cbxUseMP.Checked || w.Character_cbxUseMPGrain.Checked)
                {
                    byte useMP = 0; // dummy
                    WinAPI.InvokeIfRequired(w.Character_tbxUseMP, () => {
                        useMP = byte.Parse(w.Character_tbxUseMP.Text);
                    });
                    if (InfoManager.Character.GetMPPercent() <= useMP)
                    {
                        byte slot = 0;
                        if (w.Character_cbxUseMPGrain.Checked && FindItem(3, 1, 2, ref slot, "_SPOTION_")
                            || w.Character_cbxUseMP.Checked && FindItem(3, 1, 2, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot);
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
            if (InfoManager.Character.LifeStateType == SRModel.LifeState.Alive)
            {
                Window w = Window.Get;
                if (w.Character_cbxUseHPVigor.Checked || w.Character_cbxUseMPVigor.Checked)
                {
                    byte usePercent = 0;
                    WinAPI.InvokeIfRequired(w.Character_tbxUseHPVigor, () => {
                        usePercent = byte.Parse(w.Character_tbxUseHPVigor.Text);
                    });
                    // Check hp %
                    if (InfoManager.Character.GetHPPercent() <= usePercent)
                    {
                        byte slot = 0;
                        if (FindItem(3, 1, 3, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot);
                            tUsingVigor.Start();
                        }
                    }
                    else
                    {
                        // Check mp %
                        WinAPI.InvokeIfRequired(w.Character_tbxUseMPVigor, () => {
                            usePercent = byte.Parse(w.Character_tbxUseMPVigor.Text);
                        });
                        if (InfoManager.Character.GetMPPercent() <= usePercent)
                        {
                            byte slot = 0;
                            if (FindItem(3, 1, 3, ref slot))
                            {
                                PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot);
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
            if (InfoManager.Character.LifeStateType == SRModel.LifeState.Alive)
            {
                Window w = Window.Get;
                if (w.Character_cbxUsePillUniversal.Checked)
                {
                    if (((uint)InfoManager.Character.BadStatusFlags).HasFlags
                        ((uint)(SRModel.BadStatus.Freezing
                        | SRModel.BadStatus.ElectricShock
                        | SRModel.BadStatus.Burn
                        | SRModel.BadStatus.Poisoning
                        | SRModel.BadStatus.Zombie)))
                    {
                        byte slot = 0;
                        if (FindItem(3, 2, 6, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot);
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
            if (InfoManager.Character.LifeStateType == SRModel.LifeState.Alive)
            {
                Window w = Window.Get;
                if (w.Character_cbxUsePillPurification.Checked)
                {
                    if (((uint)InfoManager.Character.BadStatusFlags).HasFlags
                        ((uint)(SRModel.BadStatus.Dull
                        | SRModel.BadStatus.Fear
                        | SRModel.BadStatus.ShortSight
                        | SRModel.BadStatus.Bleed
                        | SRModel.BadStatus.Darkness
                        | SRModel.BadStatus.Disease
                        | SRModel.BadStatus.Confusion
                        | SRModel.BadStatus.Decay
                        | SRModel.BadStatus.Weaken
                        | SRModel.BadStatus.Impotent
                        | SRModel.BadStatus.Division
                        | SRModel.BadStatus.Panic
                        | SRModel.BadStatus.Combustion
                        | SRModel.BadStatus.Hidden)))
                    {
                        byte slot = 0;
                        if (FindItem(3, 2, 1, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot);
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
            if (w.Party_cbxInviteAll.Checked)
            {
                // Check players around
                if (InfoManager.Players.Count > 0)
                {
                    if (InfoManager.inParty)
                    {
                        xDictionary<string, SRPlayer> PlayersNearWithNoParty = new xDictionary<string, SRPlayer>(InfoManager.Players);
                        // Remove players nears with party
                        for (byte j = 0; j < InfoManager.Party.Members.Count; j++)
                        {
                            string PlayerName = InfoManager.Party.Members.GetAt(j).Name.ToUpper();
                            if (PlayersNearWithNoParty.ContainsKey(PlayerName))
                            {
                                PlayersNearWithNoParty.RemoveKey(PlayerName);
                            }
                        }
                        if (PlayersNearWithNoParty.Count == 0)
                            return;

                        // Check invitations setup
                        if (!w.Party_cbxInviteOnlyPartySetup.Checked
                            || InfoManager.Party.SetupFlags == w.GetPartySetup())
                        {
                            if (!InfoManager.Party.isFull)
                            {
                                PacketBuilder.InviteToParty(PlayersNearWithNoParty.GetAt(rand.Next(PlayersNearWithNoParty.Count)).UniqueID);
                                tCycleAutoParty.Start();
                            }
                        }
                    }
                    else
                    {
                        PacketBuilder.CreateParty(InfoManager.Players.GetAt(rand.Next(InfoManager.Players.Count)).UniqueID, InfoManager.Party.SetupFlags);
                        tCycleAutoParty.Start();
                    }
                }
                else
                {
                    tCycleAutoParty.Start();
                }
            }
            else if (w.Party_cbxInvitePartyList.Checked)
            {
                if (InfoManager.Players.Count > 0)
                {
                    List<string> PlayersToInvite = new List<string>();
                    w.Party_lstvPartyList.InvokeIfRequired(() => {
                        for (int j = 0; j < w.Party_lstvPartyList.Items.Count; j++)
                            PlayersToInvite.Add(w.Party_lstvPartyList.Items[j].Name);
                    });
                    // Remove if are in party already
                    for (int j = 0; j < PlayersToInvite.Count; j++)
                    {
                        for (byte k = 0; k < InfoManager.Party.Members.Count; k++)
                        {
                            if (PlayersToInvite[j].Equals(InfoManager.Party.Members.GetAt(k).Name, StringComparison.OrdinalIgnoreCase))
                            {
                                PlayersToInvite.RemoveAt(j--);
                                break;
                            }
                        }
                    }
                    if (PlayersToInvite.Count > 0)
                    {
                        // Shuffle and check the party list with near players
                        PlayersToInvite.Shuffle();
                        SRPlayer PlayerToInvite = null;
                        for (int j = 0; j < PlayersToInvite.Count; j++)
                        {
                            if ((PlayerToInvite = InfoManager.Players[PlayersToInvite[j]]) != null)
                                break;
                        }
                        if (PlayerToInvite != null)
                        {
                            if (InfoManager.inParty)
                            {
                                if (!w.Party_cbxInviteOnlyPartySetup.Checked
                                    || InfoManager.Party.SetupFlags == w.GetPartySetup())
                                {
                                    if (!InfoManager.Party.isFull)
                                    {
                                        PacketBuilder.InviteToParty(PlayerToInvite.UniqueID);
                                        tCycleAutoParty.Start();
                                    }
                                }
                            }
                            else
                            {
                                PacketBuilder.CreateParty(PlayerToInvite.UniqueID, w.GetPartySetup());
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
                bool found = false;
                w.Party_lstvLeaderList.InvokeIfRequired(() => {
                    for (byte j = 0; j < InfoManager.Party.Members.Count; j++)
                    {
                        if (w.Party_lstvLeaderList.Items.ContainsKey(InfoManager.Party.Members.GetAt(j).Name.ToUpper()))
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
            // Checking pet using recovery kit by priority
            Window w = Window.Get;
            // Vehicle or Transport
            if (w.Character_cbxUseTransportHP.Checked)
            {
                SRCoService pet = InfoManager.MyPets.Find(p => p.isHorse() || p.isTransport());
                // Check % if there is at least one pet
                if (pet != null)
                {
                    byte useHP = 0; // dummy
                    w.Character_tbxUseTransportHP.InvokeIfRequired(() => {
                        useHP = byte.Parse(w.Character_tbxUseTransportHP.Text);
                    });
                    if (pet.GetHPPercent() <= useHP)
                    {
                        byte slot = 0;
                        if (FindItem(3, 1, 4, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot, pet.UniqueID);
                            tUsingRecoveryKit.Start();
                        }
                        return; // Avoid checking other pets
                    }
                }
            }

            // Attacking pet
            if (w.Character_cbxUsePetHP.Checked)
            {
                SRCoService pet = InfoManager.MyPets.Find(p => p.isAttackPet());
                // Check % if there is at least one pet
                if (pet != null)
                {
                    byte useHP = 0; // dummy
                    w.Character_tbxUsePetHP.InvokeIfRequired(() => {
                        useHP = byte.Parse(w.Character_tbxUsePetHP.Text);
                    });
                    if (pet.GetHPPercent() <= useHP)
                    {
                        byte slot = 0;
                        if (FindItem(3, 1, 4, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot, pet.UniqueID);
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
            if (w.Character_cbxUsePetsPill.Checked)
            {
                // Checking pet bad status for using abnormal
                SRCoService pet = null;
                // As priority pet transport
                pet = InfoManager.MyPets.Find(p => (p.isHorse() || p.isTransport()) && p.BadStatusFlags != SRModel.BadStatus.None);
                if (pet == null)
                    pet = InfoManager.MyPets.Find(p => p.isAttackPet() && p.BadStatusFlags != SRModel.BadStatus.None);
                // At least one pet has bad status
                if (pet != null)
                {
                    byte slot = 0; // dummy
                    if (FindItem(3, 2, 7, ref slot))
                    {
                        PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot, pet.UniqueID);
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
                SRCoService pet = InfoManager.MyPets.Find(p => p.isAttackPet());
                if (pet != null)
                {
                    SRAttackPet atkPet = (SRAttackPet)pet;

                    byte usePercent = 0;
                    w.Character_tbxUsePetHGP.InvokeIfRequired(() => {
                        usePercent = byte.Parse(w.Character_tbxUsePetHGP.Text);
                    });
                    // Check hgp %
                    int HGPPercent = (int)(atkPet.HGP * 0.01); // 10000 = 100%
                    if (HGPPercent <= usePercent)
                    {
                        byte slot = 0;
                        if (FindItem(3, 1, 9, ref slot))
                        {
                            PacketBuilder.UseItem(InfoManager.Character.Inventory[slot], slot, pet.UniqueID);
                            tUsingHGP.ResetTimer(1000);
                            return;
                        }
                    }
                    tUsingHGP.ResetTimer(300000); // 1% decrease : -100 HGP every 5min
                }
            }
        }
        private void CheckLoginOptions(object sender, ElapsedEventArgs e)
        {
            Window w = Window.Get;

            if (w.Login_cbxGoClientless.Checked)
                GoClientless();

            if (w.Login_cbxUseReturnScroll.Checked)
                UseReturnScroll();

            if (w.Party_cbxMatchAutoReform.Checked)
                CheckPartyMatchAutoReform();

            CheckAutoParty();
        }
        public void CheckWeaponSwitch(SRSkill skill)
        {
            if (IsSwitchingWeapon)
                return;

            // Check if the skill requires a weapon or an item to be used
            if (skill.RequiredWeaponPrimary == SRTypes.Weapon.None && skill.RequiredWeaponSecondary == SRTypes.Weapon.None && skill.RequiredItems.Count == 0)
                return;

            // Check my weapon being used
            var myWeapon = GetMyWeaponType();
            if (myWeapon == SRTypes.Weapon.None || (myWeapon != skill.RequiredWeaponPrimary && myWeapon != skill.RequiredWeaponSecondary))
            {
                // Find the item used to switch weapon
                var slot = InfoManager.Character.Inventory.FindIndex(i => i != null && i.isEquipable() && ((skill.RequiredWeaponPrimary != SRTypes.Weapon.None && ((SREquipable)i).IsWeapon() && (SRTypes.Weapon)i.ID4 == skill.RequiredWeaponPrimary) || (skill.RequiredWeaponSecondary != SRTypes.Weapon.None && ((SREquipable)i).IsWeapon() && (SRTypes.Weapon)i.ID4 == skill.RequiredWeaponSecondary)), 13);
                if (slot != -1)
                {
                    IsSwitchingWeapon = true;
                    PacketBuilder.MoveItem((byte)slot, 6, SRTypes.InventoryItemMovement.InventoryToInventory);

                    // Check if the skill can equip a shield: sword (ch), blade, sword (eu), dark staff, cleric
                    var shieldWeapons = new SRTypes.Weapon[] { SRTypes.Weapon.Sword, SRTypes.Weapon.Blade, SRTypes.Weapon.OneHandSword, SRTypes.Weapon.Warlock, SRTypes.Weapon.Cleric };
                    var itemWeaponType = (SRTypes.Weapon)InfoManager.Character.Inventory[slot].ID4;
                    if (shieldWeapons.Any(any => any == itemWeaponType))
                    {
                        // Check if Auto-Shield feature is enabled
                        bool useShield = false;
                        Window.Get.Character_cbxPVPModeUseShield.InvokeIfRequired(() => { useShield = Window.Get.Character_cbxPVPModeUseShield.Checked; });
                        if (useShield)
                        {
                            var myRace = InfoManager.Character.GetRace();
                            slot = InfoManager.Character.Inventory.FindIndex(i => i != null && i.isEquipable() && ((SREquipable)i).IsShield() && ((SREquipable)i).GetRace() == myRace, 13);
                            if (slot != -1)
                                PacketBuilder.MoveItem((byte)slot, 7, SRTypes.InventoryItemMovement.InventoryToInventory);
                        }
                    }
                    return;
                }
            }

            // Check if the skill requires an item
            if (skill.RequiredItems.Count != 0)
            {
                // Warrior forced filter
                if (skill.ServerName.StartsWith("SKILL_EU_WARRIOR"))
                {
                    // Check if is a shield skill
                    if (skill.ServerName.Contains("_SHIELD"))
                    {
                        // Ignore if has shield equiped already
                        if(GetMySecondaryEquipableType() == SREquipable.Equipable.Shield)
                            return;

                        var myRace = InfoManager.Character.GetRace();
                        var slot = InfoManager.Character.Inventory.FindIndex(i => i != null && i.isEquipable() && ((SREquipable)i).IsShield() && ((SREquipable)i).GetRace() == myRace, 13);
                        if (slot != -1)
                        {
                            IsSwitchingWeapon = true;
                            PacketBuilder.MoveItem((byte)slot, 7, SRTypes.InventoryItemMovement.InventoryToInventory);
                        }
                    }
                    else
                    {
                        List<SRTypes.Weapon> weapons = new List<SRTypes.Weapon>();
                        if (skill.ServerName.StartsWith("SKILL_EU_WARRIOR_FRENZYA_DAMAGE"))
                            weapons.Add(SRTypes.Weapon.TwoHandSword);
                        else
                        {
                            weapons.Add(SRTypes.Weapon.OneHandSword);
                            weapons.Add(SRTypes.Weapon.TwoHandSword);
                            weapons.Add(SRTypes.Weapon.DualAxes);
                        }

                        // Ignore if weapon is equiped already
                        if (weapons.Any(any => any == myWeapon))
                            return;

                        // Find weapon
                        var slot = InfoManager.Character.Inventory.FindIndex(i => i != null && i.isEquipable() && ((SREquipable)i).IsWeapon() && weapons.Any(any => any == (SRTypes.Weapon)i.ID4), 13);
                        if (slot != -1)
                        {
                            IsSwitchingWeapon = true;
                            PacketBuilder.MoveItem((byte)slot, 6, SRTypes.InventoryItemMovement.InventoryToInventory);
                        }
                    }
                }
                // Bicheon forced filter
                else if (skill.ServerName.StartsWith("SKILL_CH_SWORD"))
                {
                    // Check if is a shield skill
                    if (skill.ServerName.Contains("_SHIELD"))
                    {
                        // Ignore if has shield equiped already
                        if (GetMySecondaryEquipableType() == SREquipable.Equipable.Shield)
                            return;
                        
                        var myRace = InfoManager.Character.GetRace();
                        var slot = InfoManager.Character.Inventory.FindIndex(i => i != null && i.isEquipable() && ((SREquipable)i).IsShield() && ((SREquipable)i).GetRace() == myRace, 13);
                        if (slot != -1)
                        {
                            IsSwitchingWeapon = true;
                            PacketBuilder.MoveItem((byte)slot, 7, SRTypes.InventoryItemMovement.InventoryToInventory);
                        }
                    }
                    else
                    {
                        List<SRTypes.Weapon> weapons = new List<SRTypes.Weapon>()
                        {
                            SRTypes.Weapon.Sword,
                            SRTypes.Weapon.Blade,
                        };

                        // Ignore if weapon is equiped already
                        if (weapons.Any(any => any == myWeapon))
                            return;

                        // Find weapon
                        var slot = InfoManager.Character.Inventory.FindIndex(i => i != null && i.isEquipable() && ((SREquipable)i).IsWeapon() && weapons.Any(any => any == (SRTypes.Weapon)i.ID4), 13);
                        if (slot != -1)
                        {
                            IsSwitchingWeapon = true;
                            PacketBuilder.MoveItem((byte)slot, 6, SRTypes.InventoryItemMovement.InventoryToInventory);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
