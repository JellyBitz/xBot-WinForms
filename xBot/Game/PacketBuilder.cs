using SecurityAPI;
using xBot.App;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Item;
using xBot.Game.Objects.Party;
using xBot.Network;

namespace xBot.Game
{
	public static class PacketBuilder
	{
		public static void Login(string username, string password, ushort serverID)
		{
			Packet p = new Packet(Gateway.Opcode.CLIENT_LOGIN_REQUEST, true);
			p.WriteByte(DataManager.Locale);
			p.WriteAscii(username);
			p.WriteAscii(password);
			p.WriteUShort(serverID);
			Bot.Get.Proxy.Gateway.InjectToServer(p);
		}
		public static void RequestCharacterList()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST, true);
			p.WriteByte(SRTypes.CharacterSelectionAction.List);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SelectCharacter(string charname)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_JOIN_REQUEST);
			p.WriteAscii(charname);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void DeleteCharacter(string charname)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteSByte(SRTypes.CharacterSelectionAction.Delete);
			p.WriteAscii(charname);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CheckCharacterName(string charname)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteSByte(SRTypes.CharacterSelectionAction.CheckName);
			p.WriteAscii(charname);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static bool CreateCharacter(string charname, bool male, string type = "CH")
		{
			uint model, chest, legs, shoes, weapon;
			try {
				if (type == "CH")
				{
					if (male)
					{
						model = uint.Parse(DataManager.GetModelData("CHAR_CH_MAN_ADVENTURER")["id"]);
						chest = uint.Parse(DataManager.GetItemData("ITEM_CH_M_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(DataManager.GetItemData("ITEM_CH_M_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(DataManager.GetItemData("ITEM_CH_M_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(DataManager.GetItemData("ITEM_CH_SWORD_01_A_DEF")["id"]);
					}
					else
					{
						model = uint.Parse(DataManager.GetModelData("CHAR_CH_WOMAN_ADVENTURER")["id"]);
						chest = uint.Parse(DataManager.GetItemData("ITEM_CH_W_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(DataManager.GetItemData("ITEM_CH_W_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(DataManager.GetItemData("ITEM_CH_W_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(DataManager.GetItemData("ITEM_CH_SWORD_01_A_DEF")["id"]);
					}
				}
				else
				{
					if (male)
					{
						model = uint.Parse(DataManager.GetModelData("CHAR_EU_MAN_NOBLE")["id"]);
						chest = uint.Parse(DataManager.GetItemData("ITEM_EU_M_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(DataManager.GetItemData("ITEM_EU_M_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(DataManager.GetItemData("ITEM_EU_M_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(DataManager.GetItemData("ITEM_EU_SWORD_01_A_DEF")["id"]);
					}
					else
					{
						model = uint.Parse(DataManager.GetModelData("CHAR_EU_WOMAN_NOBLE")["id"]);
						chest = uint.Parse(DataManager.GetItemData("ITEM_EU_W_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(DataManager.GetItemData("ITEM_EU_W_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(DataManager.GetItemData("ITEM_EU_W_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(DataManager.GetItemData("ITEM_EU_SWORD_01_A_DEF")["id"]);
					}
				}
			}
			catch
			{
				Window.Get.Log("Error loading data...");
				return false;
			}
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteByte(SRTypes.CharacterSelectionAction.Create);
			p.WriteAscii(charname);
			p.WriteUInt(model);
			p.WriteByte(0); // Scale
			p.WriteUInt(chest);
			p.WriteUInt(legs);
			p.WriteUInt(shoes);
			p.WriteUInt(weapon);
			Bot.Get.Proxy.Agent.InjectToServer(p);
			return true;
		}
		public static void SendChatAll(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.All);
			p.WriteByte(Window.Get.Chat_rtbxAll.Lines.Length); // Client chat current index
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatPrivate(string player, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.Private);
			p.WriteByte(Window.Get.Chat_rtbxPrivate.Lines.Length);
			p.WriteAscii(player);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatParty(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.Party);
			p.WriteByte(Window.Get.Chat_rtbxParty.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatGuild(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.Guild);
			p.WriteByte(Window.Get.Chat_rtbxGuild.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatUnion(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.Union);
			p.WriteByte(Window.Get.Chat_rtbxUnion.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatAcademy(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.Academy);
			p.WriteByte(Window.Get.Chat_rtbxAcademy.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatStall(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(SRTypes.Chat.Stall);
			p.WriteByte(Window.Get.Chat_rtbxStall.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatGlobal(byte slotGlobal,SRItem item,string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_USE,true);
			p.WriteByte(slotGlobal);
			p.WriteUShort(item.GetUsageType());
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendMail(string title, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_MAIL_SEND_REQUEST);
			p.WriteAscii(title);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void MoveTo(ushort region, int x, int y, int z, uint petUniqueID = 0u)
		{
			Packet p;
			if (petUniqueID == 0)
			{
				p = new Packet(Agent.Opcode.CLIENT_CHARACTER_MOVEMENT);
			}
			else
			{
				p = new Packet(Agent.Opcode.CLIENT_CHARACTER_PET_ACTION);
				p.WriteUInt(petUniqueID);
				p.WriteByte(SRCoService.Action.Movement); // Action
			}
			p.WriteByte(1); // Movement Type
			p.WriteUShort(region);
			if (region >= short.MaxValue)
			{
				p.WriteInt(x);
				p.WriteInt(z);
				p.WriteInt(y);
			}
			else
			{
				p.WriteShort(x);
				p.WriteShort(z);
				p.WriteShort(y);
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void MoveTo(SRCoord position, uint petUniqueID = 0u)
		{
			MoveTo(position.Region, position.X, position.Y, position.Z, petUniqueID);
		}
		public static void AddStatPointINT()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ADD_INT_REQUEST);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void AddStatPointSTR()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ADD_STR_REQUEST);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void PlayerPetitionResponse(bool accept, SRTypes.PlayerPetition type)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PLAYER_INVITATION_RESPONSE);
			if (accept)
			{
				p.WriteByte(1);
				p.WriteByte(1);
			}
			else
			{
				switch (type)
				{
					case SRTypes.PlayerPetition.PartyInvitation:
					case SRTypes.PlayerPetition.PartyCreation:
						p.WriteByte(2);
						p.WriteByte(0xC);
						p.WriteByte(0x2C);
						break;
					default:
						p.WriteByte(1);
						p.WriteByte(0);
						break;
				}
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void InviteToExchange(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_EXCHANGE_INVITATION_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CreateParty(uint uniqueID, SRParty.Setup setup)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_CREATION_REQUEST);
			p.WriteUInt(uniqueID);
			p.WriteByte(setup);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void InviteToParty(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_INVITATION_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void BanFromParty(uint joinID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_KICK_REQUEST);
			p.WriteUInt(joinID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void LeaveParty()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_LEAVE);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RequestPartyMatch(byte pageIndex = 0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_LIST_REQUEST);
			p.WriteByte(pageIndex);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CreatePartyMatch(SRPartyMatch match)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_CREATION_REQUEST);
			p.WriteUInt(0); // Party number
			p.WriteUInt(0); // Unknown 
			p.WriteByte((byte)match.Setup);
			p.WriteByte((byte)match.Purpose);
			p.WriteByte(match.LevelMin);
			p.WriteByte(match.LevelMax);
			p.WriteAscii(match.Title);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RemovePartyMatch(uint number)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_DELETE_REQUEST);
			p.WriteUInt(number);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void JoinToPartyMatch(uint number)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_JOIN_REQUEST);
			p.WriteUInt(number);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void PartyMatchJoinResponse(uint requestID, uint joinID, bool accept)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_JOIN_RESPONSE);
			p.WriteUInt(requestID);
			p.WriteUInt(joinID);
			p.WriteByte(accept ? 1 : 0);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SelectEntity(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_ENTITY_SELECTION);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void TalkNPC(uint uniqueID,byte talkID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_ENTITY_TALK_REQUEST);
			p.WriteUInt(uniqueID);
			p.WriteByte(talkID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void BuyNPC(uint uniqueID,byte tab,byte slot,ushort quantity)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_ENTITY_TALK_REQUEST);
			p.WriteByte(8); // unknown
			p.WriteByte(tab);
			p.WriteByte(slot);
			p.WriteUShort(quantity);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CloseNPC(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_NPC_CLOSE_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void UseItem(SRItem item,byte slot,uint uniqueID = 0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_USE,true);
			p.WriteByte(slot);
			p.WriteUShort(item.GetUsageType());
			if (uniqueID != 0)
				p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void MoveItem(byte slotInitial,byte slotFinal, SRTypes.InventoryItemMovement type,ushort quantity = 0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_MOVEMENT);
			p.WriteByte(type);
			p.WriteByte(slotInitial);
			p.WriteByte(slotFinal);
			p.WriteUShort(quantity);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void MoveItem(byte slotInitial,byte slotFinal, SRTypes.InventoryItemMovement type,uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_MOVEMENT);
			p.WriteByte(type);
			p.WriteByte(slotInitial);
			p.WriteByte(slotFinal);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void DropItem(byte slot)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_MOVEMENT);
			p.WriteByte(SRTypes.InventoryItemMovement.InventoryToGround);
			p.WriteByte(slot);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void PickUpItem(uint itemUniqueID,uint petPickUniqueID = 0)
		{
			Packet p;
      if (petPickUniqueID == 0)
			{
				p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ACTION_REQUEST);
				p.WriteByte(1);
				p.WriteByte(SRTypes.CharacterAction.ItemPickUp);
				p.WriteByte(1);
				p.WriteUInt(itemUniqueID);
			}
			else
			{
				p = new Packet(Agent.Opcode.CLIENT_CHARACTER_PET_ACTION);
				p.WriteUInt(petPickUniqueID);
				p.WriteByte(SRCoService.Action.ItemPickUp);
				p.WriteUInt(itemUniqueID);
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CreateStall(string title,string note)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_CREATE_REQUEST);
			p.WriteAscii(title); // title.LengthMax = 63
			Bot.Get.Proxy.Agent.InjectToServer(p);
			p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.Note);
			p.WriteAscii(note);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void EditStallTitle(string title)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.Title);
			p.WriteAscii(title);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void EditStallNote(string note)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.Note);
			p.WriteAscii(note);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void EditStallState(bool open)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.State);
			p.WriteByte(open ? 1 : 0);
			p.WriteShort(0); // unknown
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void AddItemStall(byte slotStall,byte slotInventory,ushort quantity,ulong price)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.ItemAdded);
			p.WriteByte(slotStall);
			p.WriteByte(slotInventory);
			p.WriteUShort(quantity);
			p.WriteULong(price);
			p.WriteUInt(1);// FleaMarketNetworkTidGroup. Using the same stall network category
			p.WriteUShort(0); // unknown
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RemoveItemStall(byte slotStall)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.ItemRemoved);
			p.WriteByte(slotStall);
			p.WriteUShort(0); // unknown
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void EditItemStall(byte slotStall,ushort quantity,ulong price)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteByte(SRTypes.StallUpdate.ItemUpdate);
			p.WriteByte(slotStall);
			p.WriteUShort(quantity);
			p.WriteULong(price);
			p.WriteUShort(0); // unknown
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CloseStall()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_DESTROY_REQUEST);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void OpenStall(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_TALK_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void ExitStall()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_LEAVE_REQUEST);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void BuyStallItem(byte slotStall)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_BUY_REQUEST);
			p.WriteByte(slotStall);
			Bot.Get.Proxy.Agent.InjectToServer(p);				
		}
		public static void RequestStorageData(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STORAGE_DATA_REQUEST);
			p.WriteUInt(uniqueID);
			p.WriteByte(0);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void UnsummonPet(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PET_UNSUMMON_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void ResurrectAtPresentPoint()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_AUTORESURRECTION);
			p.WriteByte(2);
			Bot.Get.Proxy.Agent.InjectToServer(p,1000);
		}
		public static void CastSkill(uint skillID, uint targetUniqueID = 0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ACTION_REQUEST);
			p.WriteByte(1);
			p.WriteByte(SRTypes.CharacterAction.SkillCast);
			p.WriteUInt(skillID);
			if (targetUniqueID != 0)
			{
				p.WriteByte(1);
				p.WriteUInt(targetUniqueID);
			}
			else
			{
				p.WriteByte(0);
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void AttackTarget(uint targetUniqueID, uint skillID = 1)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ACTION_REQUEST);
			p.WriteByte(1);
			// Check if is common attack
			if (skillID == 1)
			{
				p.WriteByte(1);
			}
			else
			{
				p.WriteByte(SRTypes.CharacterAction.SkillCast);
				p.WriteUInt(skillID);
			}
			p.WriteByte(1); // has target? always.
			p.WriteUInt(targetUniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RemoveBuff(uint skillID, uint targetUniqueID = 0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ACTION_REQUEST);
			p.WriteByte(1);
			p.WriteByte(SRTypes.CharacterAction.SkillRemove);
			p.WriteUInt(skillID);
			if (targetUniqueID != 0)
			{
				p.WriteUInt(targetUniqueID);
			}
			p.WriteByte(0);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void UseTeleport(uint sourceUniqueID,uint destinationID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_TELEPORT_USE_REQUEST);
			p.WriteUInt(sourceUniqueID);
			p.WriteByte(2); // unknown
			p.WriteUInt(destinationID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void DesignateRecall(uint teleportUniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_TELEPORT_RECALL_REQUEST);
			p.WriteUInt(teleportUniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void InviteToGuild(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_GUILD_INVITATION_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void EditGuildNotice(string title, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_GUILD_NOTICE_EDIT_REQUEST);
			p.WriteAscii(title);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void AddItemExchange(byte slot)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_MOVEMENT);
			p.WriteByte(SRTypes.InventoryItemMovement.InventoryToExchange);
			p.WriteByte(slot);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RemoveItemExchange(byte slot)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_MOVEMENT);
			p.WriteByte(SRTypes.InventoryItemMovement.ExchangeToInventory);
			p.WriteByte(slot);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void EditGoldExchange(ulong newGold)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_MOVEMENT);
			p.WriteByte(SRTypes.InventoryItemMovement.InventoryGoldToExchange);
			p.WriteULong(newGold);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void ConfirmExchange()
		{
			Bot.Get.Proxy.InjectToServer(new Packet(Agent.Opcode.CLIENT_EXCHANGE_CONFIRM_REQUEST));
		}
		public static void ApproveExchange()
		{
			Bot.Get.Proxy.InjectToServer(new Packet(Agent.Opcode.CLIENT_EXCHANGE_APPROVE_REQUEST));
		}
		public static void InviteToAcademy(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_ACADEMY_INVITATION_REQUEST);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		internal class Client
		{
			public static void CreatePickUpPacket(SRItem item, byte inventorySlot)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_INVENTORY_ITEM_MOVEMENT);
				p.WriteByte(1); // Success
				p.WriteByte(SRTypes.InventoryItemMovement.GroundToInventory);
				p.WriteByte(inventorySlot);
				p.WriteUInt(item.Rentable.ID);
				if (item.Rentable.RentableType == SRRentable.Type.LimitedTime)
				{
					p.WriteUShort(item.Rentable.CanDelete);
					p.WriteUInt(item.Rentable.PeriodBeginTime);
					p.WriteUInt(item.Rentable.PeriodEndTime);
				}
				else if (item.Rentable.RentableType == SRRentable.Type.LimitedDistance)
				{
					p.WriteUShort(item.Rentable.CanDelete);
					p.WriteUShort(item.Rentable.CanRecharge);
					p.WriteUInt(item.Rentable.MeterRateTime);
				}
				else if (item.Rentable.RentableType == SRRentable.Type.Package)
				{
					p.WriteUShort(item.Rentable.CanDelete);
					p.WriteUShort(item.Rentable.CanRecharge);
					p.WriteUInt(item.Rentable.PeriodBeginTime);
					p.WriteUInt(item.Rentable.PeriodEndTime);
					p.WriteUInt(item.Rentable.PackingTime);
				}
				p.WriteUInt(item.ID);
				if (item.isEquipable())
				{
					SREquipable equip = (SREquipable)item;
					p.WriteByte(equip.Plus);
					p.WriteULong(equip.Variance);
					p.WriteUInt(equip.Durability);
					p.WriteByte(equip.MagicOptions.Count);
					for (byte j = 0; j < equip.MagicOptions.Count; j++)
					{
						p.WriteUInt(equip.MagicOptions[j].ID);
						p.WriteUInt(equip.MagicOptions[j].Value);
					}
					// 1 = Socket
					p.WriteByte(1);
					if (equip.Sockets != null)
					{
						p.WriteByte(equip.Sockets.Count);
						for (byte j = 0; j < equip.Sockets.Count; j++)
						{
							p.WriteByte(equip.Sockets[j].Slot);
							p.WriteUInt(equip.Sockets[j].ID);
							p.WriteUInt(equip.Sockets[j].Value);
						}
					}
					else
					{
						p.WriteByte(0);
					}
					// 2 = Advanced elixir
					p.WriteByte(2);
					if (equip.AdvancedElixirs != null)
					{
						p.WriteByte(equip.AdvancedElixirs.Count);
						for (byte j = 0; j < equip.AdvancedElixirs.Count; j++)
						{
							p.WriteByte(equip.AdvancedElixirs[j].Slot);
							p.WriteUInt(equip.AdvancedElixirs[j].ID);
							p.WriteUInt(equip.AdvancedElixirs[j].Value);
						}
					}
					else
					{
						p.WriteByte(0);
					}
				}
				else if (item.isCoS())
				{
					SRCoS cos = (SRCoS)item;
					if (cos.isPet())
					{
						// ITEM_COS_P
						p.WriteByte(cos.StateType);
						if (cos.StateType != SRCoS.State.NeverSummoned)
						{
							p.WriteUInt(cos.ModelID);
							p.WriteAscii(cos.ModelName);
							if (cos.ID4 == 2)
								p.WriteUInt(cos.Rentable.PeriodEndTime);
							p.WriteByte(cos.unkByte01);
						}
					}
					else if (cos.isTransform())
					{
						if (cos.ModelID == 0)
							cos.ModelID = new SRModel("MOB_CH_MANGNYANG").ID;
						p.WriteUInt(cos.ModelID);
					}
					else if (cos.isCube())
					{
						p.WriteUInt(cos.Quantity);
					}
				}
				else if (item.isEtc())
				{
					SREtc etc = (SREtc)item;
					p.WriteUShort(etc.Quantity);
					if (etc.isAlchemy())
					{
						if (item.ID4 == 1 || item.ID4 == 2)
						{
							// MAGIC/ATRIBUTTE STONE
							p.WriteByte(etc.AssimilationProbability);
						}
					}
					else if (item.ID3 == 14 && item.ID4 == 2)
					{
						// ITEM_MALL_GACHA_CARD_WIN
						// ITEM_MALL_GACHA_CARD_LOSE
						p.WriteByte(0); // paramCount
					}
				}
				Bot.Get.Proxy.Agent.InjectToClient(p);
			}
			public static void CreatePickUpSpecialtyGoodsPacket(SRItem item, byte inventorySlot, uint transportUniqueID,string OwnerName, uint unkUInt01)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_INVENTORY_ITEM_MOVEMENT);
				p.WriteByte(1); // Success
				p.WriteByte(SRTypes.InventoryItemMovement.GroundToPet);
				p.WriteUInt(transportUniqueID);
				p.WriteByte(inventorySlot);
				p.WriteUInt(unkUInt01);
				p.WriteUInt(item.ID);
				p.WriteUShort(item.Quantity);
				p.WriteAscii(OwnerName);
				Bot.Get.Proxy.Agent.InjectToClient(p);
			}
			public static void SendNotice(string message)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_CHAT_UPDATE);
				p.WriteByte(SRTypes.Chat.Notice);
				p.WriteAscii(message);
				Bot.Get.Proxy.Agent.InjectToClient(p);
			}
			public static void CreateAgentLogin(byte flag, uint loginID, string host, ushort port)
			{
				Packet p = new Packet(Gateway.Opcode.SERVER_LOGIN_RESPONSE, true);
				p.WriteByte(flag);
				p.WriteUInt(loginID);
				p.WriteAscii(host);
				p.WriteUShort(port);
				Bot.Get.Proxy.Gateway.InjectToClient(p);
			}
		}
		public static void SendGMCommand(SRTypes.GMCommandAction action, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_GAMEMASTER_COMMAND_REQUEST);
			p.WriteUShort(action);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
	}
}
