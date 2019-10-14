namespace xBot.Game
{
	public static class Params
	{
		public enum Effect : uint
		{
			AUTO_TRANSFER = 1701213281
		}
		public enum Type : uint
		{
			/// <summary>
			/// <para>1. On casting</para>
			/// <para>2. Through time</para> 
			/// </summary>
			MP_CONSUME = 1869506150,
			SKILL_DURATION = 1685418593,

		}
		public static bool Exists(string[] Params, Effect Effect)
		{
			string strEffect = ((uint)Effect).ToString();
			for (byte i = 0; i < Params.Length; i++)
				if (Params[i] == strEffect)
					return true;
			return false;
		}
		public static bool Exists(string[] Params, Type Type)
		{
			string strType = ((uint)Type).ToString();
			for (byte i = 0; i < Params.Length; i++)
				if (Params[i] == strType)
					return true;
			return false;
		}
		public static string ReadValue(string[] Params, Type Type, byte Position = 0)
		{
			string strType = ((uint)Type).ToString();
			Position++;

			for (byte i = 0; i < Params.Length; i++)
				if (Params[i] == strType)
					return Params[i + Position];
			return "";
		}
	}
}
