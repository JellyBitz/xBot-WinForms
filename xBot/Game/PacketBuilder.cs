using SecurityAPI;
using xBot.Network;

namespace xBot.Game
{
	public static class PacketBuilder
	{
		public static void Login(string username, string password, ushort serverID)
		{
			Packet p = new Packet(Gateway.Opcode.CLIENT_LOGIN_REQUEST, true);
			p.WriteUInt8(Info.Get.Locale);
			p.WriteAscii(username);
			p.WriteAscii(password);
			p.WriteUInt16(serverID);
			Bot.Get.Proxy.Gateway.InjectToServer(p);
		}
		public static void RequestCharacterList()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST, true);
			p.WriteUInt8(Types.CharacterSelectionAction.List);
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
			p.WriteInt8(Types.CharacterSelectionAction.Delete);
			p.WriteAscii(charname);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CheckCharacterName(string charname)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteInt8(Types.CharacterSelectionAction.CheckName);
			p.WriteAscii(charname);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static bool CreateCharacter(string charname, bool male, string type = "CH")
		{
			uint model, chest, legs, shoes, weapon;
			try {
				Info i = Info.Get;
        if (type == "CH")
				{
					if (male)
					{
						model = uint.Parse(i.GetModel("CHAR_CH_MAN_ADVENTURER")["id"]);
						chest = uint.Parse(i.GetItem("ITEM_CH_M_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(i.GetItem("ITEM_CH_M_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(i.GetItem("ITEM_CH_M_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(i.GetItem("ITEM_CH_SWORD_01_A_DEF")["id"]);
					}
					else
					{
						model = uint.Parse(i.GetModel("CHAR_CH_WOMAN_ADVENTURER")["id"]);
						chest = uint.Parse(i.GetItem("ITEM_CH_W_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(i.GetItem("ITEM_CH_W_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(i.GetItem("ITEM_CH_W_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(i.GetItem("ITEM_CH_SWORD_01_A_DEF")["id"]);
					}
				}
				else
				{
					if (male)
					{
						model = uint.Parse(i.GetModel("CHAR_EU_MAN_NOBLE")["id"]);
						chest = uint.Parse(i.GetItem("ITEM_EU_M_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(i.GetItem("ITEM_EU_M_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(i.GetItem("ITEM_EU_M_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(i.GetItem("ITEM_EU_SWORD_01_A_DEF")["id"]);
					}
					else
					{
						model = uint.Parse(i.GetModel("CHAR_EU_WOMAN_NOBLE")["id"]);
						chest = uint.Parse(i.GetItem("ITEM_EU_W_LIGHT_01_BA_A_DEF")["id"]);
						legs = uint.Parse(i.GetItem("ITEM_EU_W_LIGHT_01_LA_A_DEF")["id"]);
						shoes = uint.Parse(i.GetItem("ITEM_EU_W_LIGHT_01_FA_A_DEF")["id"]);
						weapon = uint.Parse(i.GetItem("ITEM_EU_SWORD_01_A_DEF")["id"]);
					}
				}
			}
			catch
			{
				Window.Get.Log("Error loading data...");
				return false;
			}
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteUInt8(Types.CharacterSelectionAction.Create);
			p.WriteAscii(charname);
			p.WriteUInt32(model);
			p.WriteUInt8(0); // Scale
			p.WriteUInt32(chest);
			p.WriteUInt32(legs);
			p.WriteUInt32(shoes);
			p.WriteUInt32(weapon);
			Bot.Get.Proxy.Agent.InjectToServer(p);
			return true;
		}
		public static void SendChatAll(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.All);
			p.WriteUInt8(Window.Get.Chat_rtbxAll.Lines.Length); // Client chat current index
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatPrivate(string player, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.Private);
			p.WriteUInt8(Window.Get.Chat_rtbxPrivate.Lines.Length);
			p.WriteAscii(player);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatParty(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.Party);
			p.WriteUInt8(Window.Get.Chat_rtbxParty.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatGuild(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.Guild);
			p.WriteUInt8(Window.Get.Chat_rtbxGuild.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatUnion(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.Union);
			p.WriteUInt8(Window.Get.Chat_rtbxUnion.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatAcademy(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.Academy);
			p.WriteUInt8(Window.Get.Chat_rtbxAcademy.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatStall(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteUInt8(Types.Chat.Stall);
			p.WriteUInt8(Window.Get.Chat_rtbxStall.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void MoveTo(ushort region,int x,int y,int z){
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_MOVEMENT);
			p.WriteUInt8(1); // 1 = Movement with coordinates; 0 = Movement with Angle
			p.WriteUInt16(region);
			if(region >= short.MaxValue){
				p.WriteUInt32(x);
				p.WriteUInt32(z);
				p.WriteUInt32(y);
			}else{
				p.WriteUInt16(x);
				p.WriteUInt16(z);
				p.WriteUInt16(y);
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
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
		public static void PlayerPetitionResponse(bool accept, Types.PlayerPetition type)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PLAYER_INVITATION_RESPONSE);
			if (accept)
			{
				p.WriteUInt8(1);
				p.WriteUInt8(1);
			}
			else
			{
				switch (type)
				{
					case Types.PlayerPetition.PartyInvitation:
					case Types.PlayerPetition.PartyCreation:
						p.WriteUInt8(2);
						p.WriteUInt8(0xC);
						p.WriteUInt8(0x2C);
						break;
					case Types.PlayerPetition.Resurrection:
						p.WriteUInt8(1);
						p.WriteUInt8(0);
						break;
				}
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void InviteToParty(uint uniqueID,byte partySetup)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_INVITATION_REQUEST);
			p.WriteUInt(uniqueID);
			p.WriteByte(partySetup);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void LeaveParty()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_LEAVE);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RequestPartyMatch(int pageIndex)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_REQUEST);
			p.WriteUInt8(pageIndex);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void JoinToPartyMatch(uint number)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_MATCH_JOIN);
			p.WriteUInt(number);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SelectEntity(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_ENTITY_SELECTION);
			p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void UseItem(SRObject item,byte slot,uint uniqueID=0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_USE,true);
			p.WriteByte(slot);
			ushort usageType = (ushort)((ushort)((uint)item[SRAttribute.RentType])
				| (item.ID1 << 2) | (item.ID2 << 5) | (item.ID3 << 7) | (item.ID4 << 11));
			p.WriteUShort(usageType);
			if (uniqueID != 0)
				p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void OpenStall(string title,string annotation)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_OPEN_REQUEST);
			p.WriteAscii(title);
			Bot.Get.Proxy.Agent.InjectToServer(p);
			p = new Packet(Agent.Opcode.CLIENT_STALL_ANOTATION_REQUEST);
			p.WriteAscii(annotation);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CloseStall()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_CLOSE_REQUEST);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RequestStorageData(uint uniqueID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STORAGE_DATA_REQUEST);
			p.WriteUInt(uniqueID);
			p.WriteUInt8(0);
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
			Packet p = new Packet(0x3053);
			p.WriteByte(2);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		internal class Client
		{
			public static void CreatePickUpPacket(SRObject item, byte inventorySlot)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_INVENTORY_ITEM_MOVEMENT);
				p.WriteByte(1); // Success
				p.WriteByte((byte)Types.InventoryItemMovement.GroundToInventory);
				p.WriteByte(inventorySlot);

				// Temporaly (probably) since it only will be used to buy town stuffs only
				if (!item.Contains(SRAttribute.RentType))
					item[SRAttribute.RentType] = (uint)(0);
				p.WriteUInt((uint)item[SRAttribute.RentType]);
				if ((uint)item[SRAttribute.RentType] == 1)
				{
					p.WriteUShort((ushort)item[SRAttribute.RentCanDelete]);
					p.WriteUInt((uint)item[SRAttribute.RentPeriodBeginTime]);
					p.WriteUInt((uint)item[SRAttribute.RentPeriodEndTime]);
				}
				else if ((uint)item[SRAttribute.RentType] == 2)
				{
					p.WriteUShort((ushort)item[SRAttribute.RentCanDelete]);
					p.WriteUShort((ushort)item[SRAttribute.RentCanRecharge]);
					p.WriteUInt((uint)item[SRAttribute.RentMeterRateTime]);
				}
				else if ((uint)item[SRAttribute.RentType] == 3)
				{
					p.WriteUShort((ushort)item[SRAttribute.RentCanDelete]);
					p.WriteUShort((ushort)item[SRAttribute.RentCanRecharge]);
					p.WriteUInt((uint)item[SRAttribute.RentPeriodBeginTime]);
					p.WriteUInt((uint)item[SRAttribute.RentPeriodEndTime]);
					p.WriteUInt((uint)item[SRAttribute.RentPackingTime]);
				}
				p.WriteUInt((uint)item.ID);
				if (item.ID1 == 3)
				{
					// ITEM_
					if (item.ID2 == 1)
					{
						// ITEM_CH_
						// ITEM_EU_
						// ITEM_AVATAR_
						if (!item.Contains(SRAttribute.Plus)) // Temporaly
							item[SRAttribute.Plus] = (byte)(0);
						p.WriteByte((byte)item[SRAttribute.Plus]);
						if (!item.Contains(SRAttribute.Variance)) // Temporaly
							item[SRAttribute.Variance] = (ulong)(0);
						p.WriteULong((ulong)item[SRAttribute.Variance]);
						if (!item.Contains(SRAttribute.Durability)) // Temporaly
							item[SRAttribute.Durability] = (uint)(64);
						p.WriteUInt((uint)item[SRAttribute.Durability]);

						if (!item.Contains(SRAttribute.MagicParams)) // Temporaly
							item[SRAttribute.MagicParams] = new SRObjectCollection(0);
						SRObjectCollection MagicParams = (SRObjectCollection)item[SRAttribute.MagicParams];
						p.WriteByte((byte)MagicParams.Count);
						for (byte j = 0; j < MagicParams.Count; j++)
						{
							SRObject MagicParam = MagicParams[j];
							p.WriteUInt((uint)MagicParam[SRAttribute.Type]);
							p.WriteUInt((uint)MagicParam[SRAttribute.Value]);
						}

						// 1 = Socket
						p.WriteByte(1);
						if (!item.Contains(SRAttribute.SocketParams)) // Temporaly
							item[SRAttribute.SocketParams] = new SRObjectCollection(0);
						SRObjectCollection SocketParams = (SRObjectCollection)item[SRAttribute.SocketParams];
						p.WriteByte((byte)SocketParams.Count);
						for (byte j = 0; j < SocketParams.Capacity; j++)
						{
							SRObject SocketParam = SocketParams[j];
							p.WriteByte((byte)SocketParam[SRAttribute.Slot]);
							p.WriteUInt((uint)SocketParam[SRAttribute.ID]);
							p.WriteUInt((uint)SocketParam[SRAttribute.Value]);
						}

						// 2 = Advanced elixir
						p.WriteByte(2);
						if (!item.Contains(SRAttribute.AdvanceElixirParams)) // Temporaly
							item[SRAttribute.AdvanceElixirParams] = new SRObjectCollection(0);
						SRObjectCollection AdvanceElixirParams = (SRObjectCollection)item[SRAttribute.AdvanceElixirParams];
						p.WriteByte((byte)AdvanceElixirParams.Count);
						for (byte j = 0; j < AdvanceElixirParams.Capacity; j++)
						{
							SRObject AdvanceElixirParam = AdvanceElixirParams[j];
							p.WriteByte((byte)AdvanceElixirParam[SRAttribute.Slot]);
							p.WriteUInt((uint)AdvanceElixirParam[SRAttribute.ID]);
							p.WriteUInt((uint)AdvanceElixirParam[SRAttribute.Plus]);
						}
					}
					else if (item.ID2 == 2)
					{
						// ITEM_COS
						if (item.ID3 == 1)
						{
							// ITEM_COS_P
							if (!item.Contains(SRAttribute.PetState)) // Temporaly
								item[SRAttribute.PetState] = (byte)Types.PetState.NeverSummoned;
							p.WriteByte((byte)((Types.PetState)item[SRAttribute.PetState]));
							switch ((Types.PetState)item[SRAttribute.PetState])
							{
								case Types.PetState.Summoned:
								case Types.PetState.Unsummoned:
								case Types.PetState.Dead:
									p.WriteUInt((uint)item[SRAttribute.ModelID]);
									p.WriteAscii((string)item[SRAttribute.PetName]);
									if (item.ID4 == 2)
									{
										// ITEM_COS_P (Ability)
										p.WriteUInt((uint)item[SRAttribute.RentPeriodEndTime]);
									}
									p.WriteByte((byte)item[SRAttribute.unkByte01]);
									break;
							}
						}
						else if (item.ID3 == 2)
						{
							// ITEM_ETC_TRANS_MONSTER
							if (!item.Contains(SRAttribute.ModelID)) // Temporaly
								item[SRAttribute.ModelID] = (new SRObject("MOB_CH_MANGNYANG", SRObject.Type.Model)).ID;
							p.WriteUInt((uint)item[SRAttribute.ModelID]);
						}
						else if (item.ID3 == 3)
						{
							// MAGIC_CUBE
							if (!item.Contains(SRAttribute.Amount)) // Temporaly
								item[SRAttribute.Amount] = (uint)(0);
							p.WriteUInt((uint)item[SRAttribute.Amount]);
						}
					}
					else if (item.ID2 == 3)
					{
						// ITEM_ETC
						p.WriteUShort((ushort)item[SRAttribute.Quantity]);
						if (item.ID3 == 11)
						{
							if (item.ID4 == 1 || item.ID4 == 2)
							{
								// MAGIC/ATRIBUTTE STONE
								if (!item.Contains(SRAttribute.AssimilationProbability)) // Temporaly
									item[SRAttribute.AssimilationProbability] = (byte)(0);
								p.WriteByte((byte)item[SRAttribute.AssimilationProbability]);
							}
						}
						else if (item.ID3 == 14 && item.ID4 == 2)
						{
							// ITEM_MALL_GACHA_CARD_WIN
							// ITEM_MALL_GACHA_CARD_LOSE
							if (!item.Contains(SRAttribute.MagicParams)) // Temporaly
								item[SRAttribute.MagicParams] = new SRObjectCollection(0);
							SRObjectCollection MagicParams = (SRObjectCollection)item[SRAttribute.MagicParams];
							p.WriteByte((byte)MagicParams.Count);
							for (byte j = 0; j < MagicParams.Count; j++)
							{
								SRObject MagicParam = MagicParams[j];
								p.WriteUInt((uint)MagicParam[SRAttribute.Type]);
								p.WriteUInt((uint)MagicParam[SRAttribute.Value]);
							}
						}
					}
				}
				Bot.Get.Proxy.Agent.InjectToClient(p);
			}
			public static void CreatePickUpSpecialtyGoodsPacket(SRObject item, byte inventorySlot, uint transportUniqueID)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_INVENTORY_ITEM_MOVEMENT);
				p.WriteByte(1); // Success
				p.WriteByte((byte)Types.InventoryItemMovement.GroundToTransport);
				p.WriteUInt(transportUniqueID);
				p.WriteByte(inventorySlot);
				p.WriteUInt((uint)item[SRAttribute.unkUInt01]);
				p.WriteUInt(item.ID);
				p.WriteUShort((ushort)item[SRAttribute.Quantity]);
				p.WriteAscii((string)item[SRAttribute.OwnerName]);
				Bot.Get.Proxy.Agent.InjectToClient(p);
			}
			public static void SendNoticeToClient(string message)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_CHAT_UPDATE);
				p.WriteByte((byte)Types.Chat.Notice);
				p.WriteAscii(message);
				Bot.Get.Proxy.Agent.InjectToClient(p);
			}
		}
		public static void GMConsola(Types.GMConsoleAction action, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PET_UNSUMMON_REQUEST);
			p.WriteByte((byte)action);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
	}
}
