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
		public static void CreateCharacter(string charname,string type="EU")
		{
			Packet p = new Packet(Agent.Opcode.CLIENT_CHARACTER_SELECTION_ACTION_REQUEST);
			p.WriteInt8(Types.CharacterSelectionAction.Create);
			p.WriteAscii(charname);
			uint model, chest, legs, shoes, weapon;
			try {
				if (type == "CH")
				{
					model = new SRObject("CHAR_CH_MAN_ADVENTURER", SRObject.Type.Model).ID;
					chest = new SRObject("ITEM_CH_M_HEAVY_01_BA_A_DEF", SRObject.Type.Item).ID;
					legs = new SRObject("ITEM_CH_M_HEAVY_01_LA_A_DEF", SRObject.Type.Item).ID;
					shoes = new SRObject("ITEM_CH_M_HEAVY_01_FA_A_DEF", SRObject.Type.Item).ID;
					weapon = new SRObject("ITEM_CH_SWORD_01_A_DEF", SRObject.Type.Item).ID;
				}
				else
				{
					model = new SRObject("CHAR_EU_MAN_NOBLE", SRObject.Type.Model).ID;
					chest = new SRObject("ITEM_EU_M_HEAVY_01_BA_A_DEF", SRObject.Type.Item).ID;
					legs = new SRObject("ITEM_EU_M_HEAVY_01_LA_A_DEF", SRObject.Type.Item).ID;
					shoes = new SRObject("ITEM_EU_M_HEAVY_01_FA_A_DEF", SRObject.Type.Item).ID;
					weapon = new SRObject("ITEM_EU_SWORD_01_A_DEF", SRObject.Type.Item).ID;
				}
			}
			catch
			{
				Window.Get.Log("Error loading item data..");
				return;
			}
			p.WriteUInt8(Types.CharacterSelectionAction.Create);
			p.WriteAscii(charname);
			p.WriteUInt32(model);
			p.WriteUInt8(0); // Scale
			p.WriteUInt32(chest);
			p.WriteUInt32(legs);
			p.WriteUInt32(shoes);
			p.WriteUInt32(weapon);
			Bot.Get.Proxy.Agent.InjectToServer(p);
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
	}
}
