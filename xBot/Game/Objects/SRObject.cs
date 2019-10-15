using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace xBot.Game.Objects
{
	public enum SRType
	{
		None = 0,
		Entity,
		Model,
		Item,
		Teleport,
		BuffZone,
		Mastery,
		Skill,
		Quest,
		Objective,
		Book,
		Param
	}
	public class SRObject
	{
		public uint ID { get; private set; }
		public SRType Type { get; private set; }
		public string ServerName { get; private set; }
		public string Name { get; set; }
		public byte ID1 { get; private set; }
		public byte ID2 { get; private set; }
		public byte ID3 { get; private set; }
		public byte ID4 { get; private set; }
		/// <summary>
		/// Create a clean object.
		/// </summary>
		public SRObject()
		{
			
		}
		/// <summary>
		/// Only used for a safe clonation/copy.
		/// </summary>
		private SRObject(SRObject value)
		{
			CopyFrom(value);
    }
		/// <summary>
		/// Create an object from the type specified.
		/// </summary>
		public SRObject(SRType Type)
		{
			this.Type = Type;
		}
		/// <summary>
		/// Create an load an object from the type specified.
		/// </summary>
		public SRObject(uint ID, SRType Type)
		{
			LoadDefaultProperties(ID, Type);
		}
		/// <summary>
		/// Create an load an object from the type specified.
		/// </summary>
		public SRObject(string ServerName, SRType Type)
		{
			LoadDefaultProperties(ServerName,Type);
		}

		#region (Generic setup)
		private Dictionary<SRProperty, object> Properties = new Dictionary<SRProperty, object>();
		public object this[SRProperty name]
		{
			get
			{
				object result = null;
				Properties.TryGetValue(name, out result);
				return result;
			}
			set
			{
				Properties[name] = value;
			}
		}
		public bool Contains(SRProperty name)
		{
			return Properties.ContainsKey(name);
		}
		public void CopyFrom(SRObject value)
		{
			// A safe copy
			this.ID = value.ID;
			this.Type = value.Type;
			this.ID1 = value.ID1;
			this.ID2 = value.ID2;
			this.ID3 = value.ID3;
			this.ID4 = value.ID4;
			this.Name = value.Name;
			this.ServerName = value.ServerName;
			this.Properties = value.Properties;
		}
		public bool isType(byte ID1, byte ID2, byte ID3, byte ID4)
		{
			return this.ID1 == ID1 && this.ID2 == ID2 && this.ID3 == ID3 && this.ID4 == ID4;
		}
		public SRObject Clone()
		{
			return new SRObject(this);
		}
		public void LoadDefaultProperties(uint ID,SRType Type)
		{
			this.ID = ID;
			this.Type = Type;

			Info i = Info.Get;
			NameValueCollection data = null;
			// Filter
			switch (Type)
			{
				case SRType.Entity:
					if (ID == uint.MaxValue)
					{
						this.Type = SRType.BuffZone;
					}
					else if ((data = i.GetModel(ID)) != null)
					{
						this.Type = SRType.Model;
					}
					else if ((data = i.GetItem(ID)) != null)
					{
						this.Type = SRType.Item;
					}
					else if ((data = i.GetTeleport(ID)) != null || (data = i.GetTeleportLink(ID)) != null)
					{
						this.Type = SRType.Teleport;
					}
					break;
				case SRType.Model:
					data = i.GetModel(ID);
					break;
				case SRType.Item:
					data = i.GetItem(ID);
					break;
				case SRType.Teleport:
					if ((data = i.GetTeleport(ID)) == null)
						data = i.GetTeleportLink(ID);
					break;
				case SRType.Skill:
					data = i.GetSkill(ID);
					break;
				case SRType.Mastery:
					data = i.GetMastery(ID);
					break;
				case SRType.Quest:

					break;
				case SRType.None:
				default:
					break;
			}
			LoadDefaultProperties(data);
		}
		public void LoadDefaultProperties(string ServerName,SRType Type)
		{
			this.ServerName = ServerName;
			this.Type = Type;

			Info i = Info.Get;
			NameValueCollection data = null;
			// Filter
			switch (Type)
			{
				case SRType.Entity:
					if (ID == uint.MaxValue)
					{
						Type = SRType.BuffZone;
					}
					else if ((data = i.GetModel(ServerName)) != null)
					{
						Type = SRType.Model;
					}
					else if ((data = i.GetItem(ServerName)) != null)
					{
						Type = SRType.Item;
					}
					else if ((data = i.GetTeleport(ServerName)) != null || (data = i.GetTeleportLink(ServerName)) != null)
					{
						Type = SRType.Teleport;
					}
					break;
				case SRType.Model:
					data = i.GetModel(ServerName);
					break;
				case SRType.Item:
					data = i.GetItem(ServerName);
					break;
				case SRType.Teleport:
					if ((data = i.GetTeleport(ServerName)) == null)
						data = i.GetTeleportLink(ServerName);
					break;
				case SRType.Skill:
					data = i.GetSkill(ServerName);
					break;
				case SRType.None:
				default:
					break;
			}
			LoadDefaultProperties(data);
		}
		private void LoadDefaultProperties(NameValueCollection data)
		{
			// Load from data if is possible
			switch (Type)
			{
				case SRType.Model:
					this.ID = uint.Parse(data["id"]);
					this.ServerName = data["servername"];
					this.Name = data["name"];
          ID1 = 1;
					ID2 = byte.Parse(data["tid2"]);
					ID3 = byte.Parse(data["tid3"]);
					ID4 = byte.Parse(data["tid4"]);
					this[SRProperty.HPMax] = this[SRProperty.HP] = uint.Parse(data["hp"]);
					break;
				case SRType.Item:
					this.ID = uint.Parse(data["id"]);
					this.ServerName = data["servername"];
					this.Name = data["name"];
					ID1 = 3;
					ID2 = byte.Parse(data["tid2"]);
					ID3 = byte.Parse(data["tid3"]);
					ID4 = byte.Parse(data["tid4"]);
					this[SRProperty.Icon] = data["icon"];
					this[SRProperty.QuantityMax] = ushort.Parse(data["stack"]);
					break;
				case SRType.Teleport:
					this.ID = uint.Parse(data["id"]);
					this.ServerName = data["servername"];
					this.Name = data["name"];
					ID1 = 4;
					ID2 = byte.Parse(data["tid2"]);
					ID3 = byte.Parse(data["tid3"]);
					ID4 = byte.Parse(data["tid4"]);
					break;
				case SRType.Skill:
					this.ID = uint.Parse(data["id"]);
					this.ServerName = data["servername"];
					this.Name = data["name"];
					this[SRProperty.GroupID] = data["group_id"];
					this[SRProperty.GroupName] = data["group_name"];
					this[SRProperty.Cooldown] = uint.Parse(data["cooldown"]);
					this[SRProperty.DurationMax] = uint.Parse(data["duration"]);
					this[SRProperty.Casttime] = uint.Parse(data["casttime"]);
					this[SRProperty.MP] = uint.Parse(data["mana"]);
					this[SRProperty.Level] = byte.Parse(data["level"]);
					this[SRProperty.SkillParams] = data["params"].Split('|');
					this[SRProperty.Icon] = data["icon"];
					break;
				case SRType.Mastery:
					this.ServerName = "";
					this.Name = data["name"];
					this[SRProperty.Description] = data["description"];
					break;
				case SRType.None:
				default:
					break;
			}
		}
		#endregion (Generic setup)

		public TreeNode ToNode()
		{
			TreeNode root = new TreeNode();
			root.Nodes.Add(new TreeNode("ID : " + ID + " (" + Type + ")"));
			// Node name to show
			string text;
			if (Type == SRType.Model
				&& ID1 == 1 && ID2 == 2 && ID3 == 3 && (ID4 == 3 || ID4 == 4))
			{
				// Pet identification
				text = (string)this[SRProperty.PetName];
				if (text == "")
					text = "No Name";
				text += " (" + this[SRProperty.OwnerName] + ")";
			}
			else
			{
				text = this.Name;
			}
			root.Text = text;
			// Header info
			switch (Type)
			{
				case SRType.Model:
				case SRType.Item:
				case SRType.Teleport:
					root.Nodes.Add(new TreeNode("Type ID's [" + ID1 + "][" + ID2 + "][" + ID3 + "][" + ID4 + "]"));
					break;
			}
			// Print all the nodes
			foreach (KeyValuePair<SRProperty, object> Property in Properties)
			{
				switch (Property.Value.GetType().Name)
				{
					case "SRObject":
						{
							root.Nodes.Add(((SRObject)Property.Value).ToNode());
							break;
						}
					case "SRObjectCollection":
						{
							TreeNode obj = new TreeNode(Property.Key.ToString());
							obj.Nodes.AddRange(((SRObjectCollection)Property.Value).ToNodes());
							root.Nodes.Add(obj);
							break;
						}
					case "String[]":
						{
							TreeNode node = new TreeNode(Property.Key.ToString());
							string[] array = (string[])Property.Value;
							foreach (string _string in array)
							{
								node.Nodes.Add(_string.ToString());
							}
							root.Nodes.Add(node);
							break;
						}
					case "Byte[]":
						{
							TreeNode node = new TreeNode(Property.Key.ToString());
							byte[] array2 = (byte[])Property.Value;
							foreach (byte _byte in array2)
							{
								node.Nodes.Add(_byte.ToString());
							}
							root.Nodes.Add(node);
							break;
						}
					case "Int32[]":
						{
							TreeNode ints = new TreeNode(Property.Key.ToString());
							int[] array3 = (int[])Property.Value;
							foreach (int _int in array3)
							{
								ints.Nodes.Add(_int.ToString());
							}
							root.Nodes.Add(ints);
							break;
						}
					case "UInt32[]":
						{
							TreeNode uints = new TreeNode(Property.Key.ToString());
							uint[] array = (uint[])Property.Value;
							foreach (uint _uint in array)
							{
								uints.Nodes.Add(_uint.ToString());
							}
							root.Nodes.Add(uints);
							break;
						}
					default:
						root.Nodes.Add(new TreeNode("\"" + Property.Key + "\" : " + Property.Value));
						break;
				}
			}
			return root;
		}

		#region (Extended exclusive object methods)
		public bool isPlayer()
		{
			return ID1 == 1 && ID2 == 1 && ID3 == 0 && ID4 == 0;
		}
		public double GetExpPercent()
		{
			return Math.Round((ulong)this[SRProperty.Exp] * 100f / (ulong)this[SRProperty.ExpMax],2);
		}
		public int GetHPPercent()
		{
			return (int)Math.Round((uint)this[SRProperty.HP] * 100f / (uint)this[SRProperty.HPMax]);
		}
		public int GetMPPercent()
		{
			return (int)Math.Round((uint)this[SRProperty.MP] * 100f / (uint)this[SRProperty.MPMax]);
		}
		public bool hasJobMode()
		{
			SRObjectCollection inventory = (SRObjectCollection)this[SRProperty.Inventory];
			return inventory.Exists((SRObject item) => item != null && item.ID1 == 3 && item.ID2 == 1 && item.ID3 == 7);
		}
		public bool hasAutoTransferEffect()
		{
			return Params.Exists((string[])this[SRProperty.SkillParams], Params.Effect.AUTO_TRANSFER);
		}
		/// <summary>
		/// Update the game position.
		/// </summary>
		private void UpdateTimePosition()
		{
			// Check if the player is using a horse
			if (this.isPlayer() && (bool)this[SRProperty.isRiding])
			{
				SRObject Ride = Info.Get.EntityList[(uint)this[SRProperty.RidingUniqueID]];
				this[SRProperty.Position] = Ride.GetPosition();
				return;
			}

			if ((bool)this[SRProperty.hasMovement])
			{
				SRCoord P = (SRCoord)this[SRProperty.Position];
				SRCoord Q = (SRCoord)this[SRProperty.MovementPosition];

				if ((int)Math.Round(P.PosX) == (int)Math.Round(Q.PosX)
					&& (int)Math.Round(P.PosY) == (int)Math.Round(Q.PosY))
				{
					// Do nothing, it's updated.
					return;
				}
				double PQMod = P.DistanceTo(Q);
				SRCoord PQUnit = new SRCoord((Q.PosX - P.PosX) / PQMod, (Q.PosY - P.PosY) / PQMod);

				double SpeedPerMs = GetSpeed() * 0.1 / 1000;
				double DistanceMsTillNow = DateTime.UtcNow.Subtract((DateTime)this[SRProperty.LastUpdateTimeUtc]).TotalMilliseconds;
				double DistanceMsMaximum = PQMod / SpeedPerMs;

				if (DistanceMsTillNow >= DistanceMsMaximum)
				{
					// The entity has reached the position long ago.
					this[SRProperty.Position] = Q;
					this[SRProperty.hasMovement] = false;
					this[SRProperty.MotionStateType] = Types.MotionState.StandUp;
				}
				else
				{
					double DistancePositionTillNow = DistanceMsTillNow * SpeedPerMs;

					if (P.inDungeon())
						this[SRProperty.Position] = new SRCoord(PQUnit.PosX * DistancePositionTillNow + P.PosX, PQUnit.PosY * DistancePositionTillNow + P.PosY, P.Region, P.Z);
					else
						this[SRProperty.Position] = new SRCoord(PQUnit.PosX * DistancePositionTillNow + P.PosX, PQUnit.PosY * DistancePositionTillNow + P.PosY);

				}
			}
		}

		public Types.MotionState GetSpeedMotionState()
		{
			if ((Types.MovementSpeed)this[SRProperty.MovementSpeedType] == Types.MovementSpeed.Running)
			{
				return Types.MotionState.Running;
			}
			return Types.MotionState.Walking;
		}

		private float GetSpeed()
		{
			if ((Types.MovementSpeed)this[SRProperty.MovementSpeedType] == Types.MovementSpeed.Running)
			{
				return (float)this[SRProperty.SpeedRunning];
			}
			return (float)this[SRProperty.SpeedWalking];
		}
		/// <summary>
		/// Update and return the current game position.
		/// </summary>
		/// <returns></returns>
		public SRCoord GetPosition()
		{
			UpdateTimePosition();
			return (SRCoord)this[SRProperty.Position];
		}
		public bool inDungeon()
		{
			return ((SRCoord)this[SRProperty.Position]).inDungeon();
		}
		public int GetDegreeAngle()
		{
			return (ushort)this[SRProperty.Angle] * 360 / 0xFFFF;
		}
		public bool isPet()
		{
			return ID1 == 1 && ID2 == 2 && ID3 == 3;
		}
		public bool isMob()
		{
			return ID1 == 1 && ID3 == 1;
		}
		public bool isNPC()
		{
			return ID1 == 1 && ID2 == 2 && ID3 != 3;
		}
		public bool isItem()
		{
			return ID1 == 3;
		}
		#endregion
	}
}
