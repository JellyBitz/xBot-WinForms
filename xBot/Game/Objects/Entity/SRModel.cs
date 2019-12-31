using System;
using System.Diagnostics;
using System.Windows.Forms;
using xBot.Game.Objects.Common;

namespace xBot.Game.Objects.Entity
{
	public class SRModel : SREntity
	{
		private SRCoord m_MovementPosition;

		#region (Properties)
		public uint HP { get; set; }
		public uint MP { get; internal set; }
		public uint HPMax { get; set; }
		public MovementSpeed MovementSpeedType { get; set; }
		public MovementAction MovementActionType { get; set; }
		public LifeState LifeStateType { get; set; }
		public MotionState MotionStateType { get; set; }
		public GameState GameStateType { get; set; }
		public float SpeedWalking { get; set; }
		public float SpeedRunning { get; set; }
		public float SpeedBerserk { get; set; }
		public xDictionary<uint,SRBuff> Buffs { get; set; }
		public BadStatus BadStatusFlags { get; set; }
		public byte unkByte01 { get; set; }
		public byte unkByte02 { get; set; }
		public SRCoord MovementPosition
		{
			get
			{
				return m_MovementPosition;
			}
			set
			{
				if (PositionUpdateTimer == null)
					PositionUpdateTimer = Stopwatch.StartNew();
				m_MovementPosition = value;
			}
		}
		public Stopwatch PositionUpdateTimer { get; set; }
		#endregion

		#region (Constructor)
		public SRModel(uint ID)
		{
			m_data = DataManager.GetModelData(ID);

			this.ID = ID;
			ServerName = m_data["servername"];
			Name = m_data["name"];
			ID1 = 1;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);

			HPMax = uint.Parse(m_data["hp"]);
			HP = HPMax;
		}
		public SRModel(string ServerName)
		{
			m_data = DataManager.GetModelData(ServerName);

			ID = uint.Parse(m_data["id"]);
			this.ServerName = ServerName;
			Name = m_data["name"];
			ID1 = 1;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);

			HPMax = uint.Parse(m_data["hp"]);
			HP = HPMax;
		}
		public SRModel(SREntity value) : base(value)
		{
			HPMax = uint.Parse(m_data["hp"]);
			HP = HPMax;
		}
		#endregion

		#region (Methods)
		public bool isPlayer()
		{
			return ID2 == 1;
		}
		public bool isNPC()
		{
			return ID2 == 2;
		}
		public override SRCoord GetRealtimePosition()
		{
			if (MovementPosition != null)
			{
				SRCoord P = Position;
				SRCoord Q = MovementPosition;
				// Check if it's updated..
				if (!P.Equals(Q))
				{
					// Scale 1920units:192px = 10:1 => To Ms
					double MilisecondsPerUnit = GetMovementSpeed();
					// Checking update times!
					long milisecondsTranscurred = PositionUpdateTimer.ElapsedMilliseconds;
					long milisecondsMaximum = P.TimeTo(Q, MilisecondsPerUnit);
					if (milisecondsTranscurred >= milisecondsMaximum)
					{
						// The entity has reached the position long ago
						Position = Q;
					}
					else
					{
						// Create vector unit
						double PQMod = P.DistanceTo(Q);
						SRCoord PQUnit = new SRCoord((Q.PosX - P.PosX) / PQMod, (Q.PosY - P.PosY) / PQMod);

						double DistanceTillNow = milisecondsTranscurred * MilisecondsPerUnit;
						if (P.inDungeon())
							Position = new SRCoord(PQUnit.PosX * DistanceTillNow + P.PosX, PQUnit.PosY * DistanceTillNow + P.PosY, P.Region, P.Z);
						else
							Position = new SRCoord(PQUnit.PosX * DistanceTillNow + P.PosX, PQUnit.PosY * DistanceTillNow + P.PosY);
					}
					PositionUpdateTimer.Restart();
				}
			}
			return Position;
		}
		public override TreeNode ToTreeNode()
		{
			TreeNode root = base.ToTreeNode();
			root.Nodes.Add("MovementSpeedType: " + MovementSpeedType);
			if (MovementPosition != null)
				root.Nodes.Add("MovementPosition: " + MovementPosition);
			root.Nodes.Add("MovementActionType: " + MovementActionType);
			root.Nodes.Add("LifeStateType: " + LifeStateType);
			root.Nodes.Add("MotionStateType: " + MotionStateType);
			root.Nodes.Add("GameStateType: " + GameStateType);
			root.Nodes.Add("SpeedWalking: " + SpeedWalking);
			root.Nodes.Add("SpeedRunning: " + SpeedRunning);
			root.Nodes.Add("SpeedBerserk: " + SpeedBerserk);
			// Buffs
			TreeNode buffs = new TreeNode("Buffs");
			for (int i = 0; i < Buffs.Count; i++)
			{
				SRBuff buff = Buffs.GetAt(i);
				buffs.Nodes.Add(buff.Name);
			}
			root.Nodes.Add(buffs);
			root.Nodes.Add("unkByte01: " + unkByte01);
			root.Nodes.Add("unkByte02: " + unkByte02);
			return root;
		}

		public float GetMovementSpeed()
		{
			return GetSpeed() * 0.1f / 1000;
		}
		public float GetSpeed()
		{
			if (MovementSpeedType == MovementSpeed.Running)
				return SpeedRunning;
			return SpeedWalking;
		}
		#endregion

		public enum MovementSpeed : byte
		{
			Walking = 0,
			Running = 1
		}
		public enum MovementAction : byte
		{
			Spinning = 0,
			KeyWalking = 1
		}
		public enum LifeState : byte
		{
			Unknown = 0,
			Alive = 1,
			Dead = 2
		}
		public enum MotionState : byte
		{
			StandUp = 0,
			Walking = 2,
			Running = 3,
			Sitting = 4
		}
		public enum GameState : byte
		{
			None = 0,
			Berserk = 1,
			Untouchable = 2,
			GameMasterInvincible = 3,
			GameMasterUntouchable = 4,
			GameMasterInvisible = 5,
			Stealth = 6,
			Invisible = 7
		}
		[Flags]
		public enum BadStatus : uint
		{
			None = 0,
			Freezing = 0x1, // Universal
			Frostbite = 0x2, // None
			ElectricShock = 0x4, // Universal
			Burn = 0x8, // Universal
			Poisoning = 0x10, // Universal
			Zombie = 0x20, // Universal
			Sleep = 0x40, // None
			Bind = 0x80, // None
			Dull = 0x100, // Purification
			Fear = 0x200, // Purification
			ShortSight = 0x400, // Purification
			Bleed = 0x800, // Purification
			Petrify = 0x1000, // None
			Darkness = 0x2000, // Purification
			Stun = 0x4000, // None
			Disease = 0x8000, // Purification
			Confusion = 0x10000, // Purification
			Decay = 0x20000, // Purification
			Weaken = 0x40000, // Purification
			Impotent = 0x80000, // Purification
			Division = 0x100000, // Purification
			Panic = 0x200000, // Purification
			Combustion = 0x400000, // Purification
			Unk01 = 0x800000,
			Hidden = 0x1000000, // Purification
			Unk02 = 0x2000000,
			Unk03 = 0x4000000,
			Unk04 = 0x8000000,
			Unk05 = 0x10000000,
			Unk06 = 0x20000000,
			Unk07 = 0x40000000,
			Unk08 = 0x80000000
		}
	}
}
