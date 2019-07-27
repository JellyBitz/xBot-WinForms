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
		public static void SendClientMessage(string message)
		{
			Packet p = new Packet(Agent.Opcode.SERVER_CHAT_UPDATE);
			p.WriteUInt8(Types.Chat.Notice);
			p.WriteAscii(message);
			Bot.Get.Proxy.Agent.InjectToClient(p);
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
		public static void PlayerPetitionResponse(bool accept, Types.PlayerPetition t)
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_PLAYER_INVITATION_RESPONSE);
			if (accept)
			{
				p.WriteUInt8(1);
				p.WriteUInt8(1);
			}
			else
			{
				switch (t)
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
		public static void InviteToParty(uint uniqueID)
		{






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
			Packet p = new Packet(Agent.Opcode.CLIENT_INVENTORY_USE_ITEM,true);
			p.WriteByte(slot);
			p.WriteUShort((ushort)((uint)item[SRAttribute.RentType] + ((item).ID1 << 2) + ((item).ID2 << 5) + ((item).ID3 << 7) + ((item).ID4 << 11)));
			if (uniqueID != 0)
				p.WriteUInt(uniqueID);
			Bot.Get.Proxy.Agent.InjectToServer(p);
		}
	}
}
