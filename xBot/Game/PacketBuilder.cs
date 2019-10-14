using SecurityAPI;
using xBot.App;
using xBot.Game.Objects;
using xBot.Network;

namespace xBot.Game
{
	public static class PacketBuilder
	{
		public static void Login(string username, string password, ushort serverID)
		{
			Packet p = new Packet(Gateway.Opcode.CLIENT_LOGIN_REQUEST, true);
			p.WriteByte(Info.Get.Locale);
			p.WriteAscii(username);
			p.WriteAscii(password);
			p.WriteUShort(serverID);
			Bot.Get.Proxy.Gateway.InjectToServer(p);
		}
		public static void RequestCharacterList()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST, true);
			p.WriteByte(Types.CharacterSelectionAction.List);
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
			p.WriteSByte(Types.CharacterSelectionAction.Delete);
			p.WriteAscii(charname);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CheckCharacterName(string charname)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteSByte(Types.CharacterSelectionAction.CheckName);
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
			p.WriteByte(Types.CharacterSelectionAction.Create);
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
			p.WriteByte(Types.Chat.All);
			p.WriteByte(Window.Get.Chat_rtbxAll.Lines.Length); // Client chat current index
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatPrivate(string player, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(Types.Chat.Private);
			p.WriteByte(Window.Get.Chat_rtbxPrivate.Lines.Length);
			p.WriteAscii(player);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatParty(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(Types.Chat.Party);
			p.WriteByte(Window.Get.Chat_rtbxParty.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatGuild(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(Types.Chat.Guild);
			p.WriteByte(Window.Get.Chat_rtbxGuild.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatUnion(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(Types.Chat.Union);
			p.WriteByte(Window.Get.Chat_rtbxUnion.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatAcademy(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(Types.Chat.Academy);
			p.WriteByte(Window.Get.Chat_rtbxAcademy.Lines.Length);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void SendChatStall(string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHAT_REQUEST);
			p.WriteByte(Types.Chat.Stall);
			p.WriteByte(Window.Get.Chat_rtbxStall.Lines.Length);
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
				p = new Packet(28705);
			}
			else
			{
				p = new Packet(28869);
				p.WriteUInt(petUniqueID);
				p.WriteByte(1); // Unknkown
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
		public static void PlayerPetitionResponse(bool accept, Types.PlayerPetition type)
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
					case Types.PlayerPetition.PartyInvitation:
					case Types.PlayerPetition.PartyCreation:
						p.WriteByte(2);
						p.WriteByte(0xC);
						p.WriteByte(0x2C);
						break;
					case Types.PlayerPetition.Resurrection:
						p.WriteByte(1);
						p.WriteByte(0);
						break;
				}
			}
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CreateParty(uint uniqueID, Types.PartySetup setup)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_CREATION_REQUEST);
			p.WriteUInt(uniqueID);
			p.WriteByte((byte)setup);
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
			Packet p = new Packet(Agent.Opcode.CLIENT_PARTY_BANISH_REQUEST);
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
		public static void UseItem(SRObject item,byte slot,uint uniqueID = 0)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_ITEM_USE,true);
			p.WriteByte(slot);
			ushort usageType = (ushort)((ushort)((uint)item[SRProperty.RentType])
				| (item.ID1 << 2) | (item.ID2 << 5) | (item.ID3 << 7) | (item.ID4 << 11));
			p.WriteUShort(usageType);
			if (uniqueID != 0)
				p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void OpenStall(string title,string annotation)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_CREATE_REQUEST);
			p.WriteAscii(title);
			Bot.Get.Proxy.Agent.InjectToServer(p);
			p = new Packet(Agent.Opcode.CLIENT_STALL_UPDATE_REQUEST);
			p.WriteAscii(annotation);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void CloseStall()
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_STALL_DESTROY_REQUEST);
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
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
		public static void RemoveBuff(uint skillID)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_ACTION_REQUEST);
			p.WriteByte(1);
			p.WriteByte(Types.CharacterAction.SkillRemove);
			p.WriteUInt(skillID);
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
		internal class Client
		{
			public static void CreatePickUpPacket(SRObject item, byte inventorySlot)
			{
				Packet p = new Packet(Agent.Opcode.SERVER_INVENTORY_ITEM_MOVEMENT);
				p.WriteByte(1); // Success
				p.WriteByte((byte)Types.InventoryItemMovement.GroundToInventory);
				p.WriteByte(inventorySlot);

				// Temporaly (probably) since it only will be used to buy town stuffs only
				if (!item.Contains(SRProperty.RentType))
					item[SRProperty.RentType] = (uint)(0);
				p.WriteUInt((uint)item[SRProperty.RentType]);
				if ((uint)item[SRProperty.RentType] == 1)
				{
					p.WriteUShort((ushort)item[SRProperty.RentCanDelete]);
					p.WriteUInt((uint)item[SRProperty.RentPeriodBeginTime]);
					p.WriteUInt((uint)item[SRProperty.RentPeriodEndTime]);
				}
				else if ((uint)item[SRProperty.RentType] == 2)
				{
					p.WriteUShort((ushort)item[SRProperty.RentCanDelete]);
					p.WriteUShort((ushort)item[SRProperty.RentCanRecharge]);
					p.WriteUInt((uint)item[SRProperty.RentMeterRateTime]);
				}
				else if ((uint)item[SRProperty.RentType] == 3)
				{
					p.WriteUShort((ushort)item[SRProperty.RentCanDelete]);
					p.WriteUShort((ushort)item[SRProperty.RentCanRecharge]);
					p.WriteUInt((uint)item[SRProperty.RentPeriodBeginTime]);
					p.WriteUInt((uint)item[SRProperty.RentPeriodEndTime]);
					p.WriteUInt((uint)item[SRProperty.RentPackingTime]);
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
						if (!item.Contains(SRProperty.Plus)) // Temporaly
							item[SRProperty.Plus] = (byte)(0);
						p.WriteByte((byte)item[SRProperty.Plus]);
						if (!item.Contains(SRProperty.Variance)) // Temporaly
							item[SRProperty.Variance] = 0ul;
						p.WriteULong((ulong)item[SRProperty.Variance]);
						if (!item.Contains(SRProperty.Durability)) // Temporaly
							item[SRProperty.Durability] = 64u;
						p.WriteUInt((uint)item[SRProperty.Durability]);

						if (!item.Contains(SRProperty.MagicParams)) // Temporaly
							item[SRProperty.MagicParams] = new SRObjectCollection();
						SRObjectCollection MagicParams = (SRObjectCollection)item[SRProperty.MagicParams];
						p.WriteByte((byte)MagicParams.Count);
						for (byte j = 0; j < MagicParams.Count; j++)
						{
							SRObject param = MagicParams[j];
							p.WriteUInt(param.ID);
							p.WriteUInt((uint)param[SRProperty.Value]);
						}

						// 1 = Socket
						p.WriteByte(1);
						if (!item.Contains(SRProperty.SocketParams)) // Temporaly
							item[SRProperty.SocketParams] = new SRObjectCollection();
						SRObjectCollection SocketParams = (SRObjectCollection)item[SRProperty.SocketParams];
						p.WriteByte((byte)SocketParams.Count);
						for (byte j = 0; j < SocketParams.Capacity; j++)
						{
							SRObject param = SocketParams[j];
							p.WriteByte((byte)param[SRProperty.Slot]);
							p.WriteUInt(param.ID);
							p.WriteUInt((uint)param[SRProperty.Value]);
						}

						// 2 = Advanced elixir
						p.WriteByte(2);
						if (!item.Contains(SRProperty.AdvanceElixirParams)) // Temporaly
							item[SRProperty.AdvanceElixirParams] = new SRObjectCollection();
						SRObjectCollection AdvanceElixirParams = (SRObjectCollection)item[SRProperty.AdvanceElixirParams];
						p.WriteByte((byte)AdvanceElixirParams.Count);
						for (byte j = 0; j < AdvanceElixirParams.Capacity; j++)
						{
							SRObject param = AdvanceElixirParams[j];
							p.WriteByte((byte)param[SRProperty.Slot]);
							p.WriteUInt(param.ID);
							p.WriteUInt((uint)param[SRProperty.Plus]);
						}
					}
					else if (item.ID2 == 2)
					{
						// ITEM_COS
						if (item.ID3 == 1)
						{
							// ITEM_COS_P
							if (!item.Contains(SRProperty.PetState)) // Temporaly
								item[SRProperty.PetState] = (byte)Types.PetState.NeverSummoned;
							p.WriteByte((byte)((Types.PetState)item[SRProperty.PetState]));
							switch ((Types.PetState)item[SRProperty.PetState])
							{
								case Types.PetState.Summoned:
								case Types.PetState.Unsummoned:
								case Types.PetState.Dead:
									p.WriteUInt((uint)item[SRProperty.PetModelID]);
									p.WriteAscii((string)item[SRProperty.PetName]);
									if (item.ID4 == 2)
									{
										// ITEM_COS_P (Ability)
										p.WriteUInt((uint)item[SRProperty.RentPeriodEndTime]);
									}
									p.WriteByte((byte)item[SRProperty.unkByte01]);
									break;
							}
						}
						else if (item.ID3 == 2)
						{
							// ITEM_ETC_TRANS_MONSTER
							if (!item.Contains(SRProperty.PetModelID)) // Temporaly
								item[SRProperty.PetModelID] = (new SRObject("MOB_CH_MANGNYANG", SRType.Model)).ID;
							p.WriteUInt((uint)item[SRProperty.PetModelID]);
						}
						else if (item.ID3 == 3)
						{
							// MAGIC_CUBE
							if (!item.Contains(SRProperty.Amount)) // Temporaly
								item[SRProperty.Amount] = (uint)(0);
							p.WriteUInt((uint)item[SRProperty.Amount]);
						}
					}
					else if (item.ID2 == 3)
					{
						// ITEM_ETC
						p.WriteUShort((ushort)item[SRProperty.Quantity]);
						if (item.ID3 == 11)
						{
							if (item.ID4 == 1 || item.ID4 == 2)
							{
								// MAGIC/ATRIBUTTE STONE
								if (!item.Contains(SRProperty.AssimilationProbability)) // Temporaly
									item[SRProperty.AssimilationProbability] = (byte)(0);
								p.WriteByte((byte)item[SRProperty.AssimilationProbability]);
							}
						}
						else if (item.ID3 == 14 && item.ID4 == 2)
						{
							// ITEM_MALL_GACHA_CARD_WIN
							// ITEM_MALL_GACHA_CARD_LOSE
							if (!item.Contains(SRProperty.MagicParams)) // Temporaly
								item[SRProperty.MagicParams] = new SRObjectCollection();
							SRObjectCollection MagicParams = (SRObjectCollection)item[SRProperty.MagicParams];
							p.WriteByte((byte)MagicParams.Count);
							for (byte j = 0; j < MagicParams.Count; j++)
							{
								SRObject param = MagicParams[j];
								p.WriteUInt(param.ID);
								p.WriteUInt((uint)param[SRProperty.Value]);
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
				p.WriteUInt((uint)item[SRProperty.unkUInt01]);
				p.WriteUInt(item.ID);
				p.WriteUShort((ushort)item[SRProperty.Quantity]);
				p.WriteAscii((string)item[SRProperty.OwnerName]);
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
		public static void GMConsole(Types.GMConsoleAction action, string message)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PET_UNSUMMON_REQUEST);
			p.WriteByte((byte)action);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
	}
}
