using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PK2ReaderAPI
{
	public class PK2Reader : IDisposable
	{
		#region Properties & Member variables
		private Blowfish m_Blowfish = new Blowfish();

		private long m_Size;
		public long Size { get { return m_Size; } }

		private byte[] m_Key;
		public byte[] Key { get { return m_Key; } }

		private string m_Key_Ascii = string.Empty;
		public string ASCIIKey { get { return m_Key_Ascii; } }

		private sPk2Header m_Header;
		public sPk2Header Header { get { return m_Header; } }

		private List<sPk2EntryBlock> m_EntryBlocks = new List<sPk2EntryBlock>();
		public List<sPk2EntryBlock> EntryBlocks { get { return m_EntryBlocks; } }

		private List<File> m_Files = new List<File>();
		public List<File> Files { get { return m_Files; } }

		private List<Folder> m_Folders = new List<Folder>();
		public List<Folder> Folders { get { return m_Folders; } }

		private string m_Path;
		public string Path { get { return m_Path; } }

		private Folder m_CurrentFolder;
		private Folder m_MainFolder;
		private System.IO.FileStream m_FileStream;
		#endregion

		#region Constructor
		public PK2Reader(string FileName, string BlowfishKey)
		{
			if (!System.IO.File.Exists(FileName))
				throw new Exception("File not found");
			else
			{
				m_Path = FileName;
				m_Key = GenerateFinalBlowfishKey(BlowfishKey);
				m_Key_Ascii = BlowfishKey;

				m_FileStream = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
				m_Size = m_FileStream.Length;

				m_Blowfish.Initialize(m_Key);
				System.IO.BinaryReader reader = new System.IO.BinaryReader(m_FileStream);
				m_Header = (sPk2Header)BufferToStruct(reader.ReadBytes(256), typeof(sPk2Header));
				m_CurrentFolder = new Folder();
				m_CurrentFolder.Name = FileName;
				m_CurrentFolder.Files = new List<File>();
				m_CurrentFolder.SubFolders = new List<Folder>();

				m_MainFolder = m_CurrentFolder;
				Read(reader.BaseStream.Position);
			}
		}
		#endregion

		#region Blowfish:Key_Generation
		private static byte[] GenerateFinalBlowfishKey(string ascii_key)
		{
			//Using the default Base_Key
			return GenerateFinalBlowfishKey(ascii_key, new byte[] { 0x03, 0xF8, 0xE4, 0x44, 0x88, 0x99, 0x3F, 0x64, 0xFE, 0x35 });
		}

		private static byte[] GenerateFinalBlowfishKey(string ascii_key, byte[] base_key)
		{
			byte ascii_key_lenght = (byte)ascii_key.Length;

			//Max count of 56 key bytes
			if (ascii_key_lenght > 56)
			{
				ascii_key_lenght = 56;
			}

			//Get bytes from ascii
			byte[] a_key = Encoding.ASCII.GetBytes(ascii_key);

			//This is the Silkroad bas key used in all versions
			byte[] b_key = new byte[56];

			//Copy key to array to keep the b_key at 56 bytes. b_key has to be bigger than a_key
			//to be able to xor every index of a_key.
			Array.ConstrainedCopy(base_key, 0, b_key, 0, base_key.Length);

			// Their key modification algorithm for the final blowfish key
			byte[] bf_key = new byte[ascii_key_lenght];
			for (byte x = 0; x < ascii_key_lenght; ++x)
			{
				bf_key[x] = (byte)(a_key[x] ^ b_key[x]);
			}

			return bf_key;
		}

		#endregion

		#region Functions & Methods
		public void ExtractFile(File File, string OutputPath)
		{
			Byte[] data = GetFileBytes(File);
			System.IO.FileStream stream = new System.IO.FileStream(OutputPath, System.IO.FileMode.OpenOrCreate);
			System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
			writer.Write(data);

		}
		public void ExtractFile(string Name, string OutputPath)
		{
			Byte[] data = GetFileBytes(Name);
			System.IO.FileStream stream = new System.IO.FileStream(OutputPath, System.IO.FileMode.OpenOrCreate);
			System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
			writer.Write(data);

		}

		public string GetFileExtension(File File)
		{
			int Offset = File.Name.LastIndexOf('.');
			return File.Name.Substring(Offset);
		}
		public string GetFileExtension(string Name)
		{
			if (FileExists(Name))
			{
				int Offset = Name.LastIndexOf('.');
				return Name.Substring(Offset);
			}
			else
				throw new Exception("The file does not exsist");
		}

		public List<File> GetRootFiles()
		{
			return m_MainFolder.Files;
		}
		public List<Folder> GetRootFolders()
		{
			return m_MainFolder.SubFolders;
		}

		public List<File> GetFiles(string ParentFolder)
		{
			List<File> ObjToReturn = new List<File>();
			foreach (File file in Files)
			{
				if (file.ParentFolder.Name == ParentFolder)
				{
					ObjToReturn.Add(file);
				}
			}
			return ObjToReturn;
		}
		public List<Folder> GetSubFolders(string ParentFolder)
		{
			List<Folder> ObjToReturn = new List<Folder>();
			foreach (Folder folder in Folders)
			{
				if (folder.Name == ParentFolder)
				{
					foreach (Folder subFolder in folder.SubFolders)
					{
						ObjToReturn.Add(subFolder);
					}
				}
			}
			return ObjToReturn;
		}

		public bool FileExists(string Name)
		{
			File file = m_Files.Find(item => item.Name.ToLower() == Name.ToLower());
			if (file.Position != 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public byte[] GetFileBytes(string Name)
		{
			if (FileExists(Name))
			{
				System.IO.BinaryReader reader = new System.IO.BinaryReader(m_FileStream);
				File file = m_Files.Find(item => item.Name.ToLower() == Name.ToLower());
				reader.BaseStream.Position = file.Position;
				return reader.ReadBytes((int)file.Size);
			}
			else
			{
				//throw new Exception(string.Format("pk2Reader: File not found: {0}", Name));
				return null;
			}
		}
		public byte[] GetFileBytes(File File)
		{
			if (FileExists(File.Name))
			{
				System.IO.BinaryReader reader = new System.IO.BinaryReader(m_FileStream);
				File file = m_Files.Find(item => item.Name.ToLower() == File.Name.ToLower());
				reader.BaseStream.Position = file.Position;
				return reader.ReadBytes((int)file.Size);
			}
			else
			{
				//throw new Exception(string.Format("pk2Reader: File not found: {0}", Name));
				return null;
			}
		}

		public string GetFileText(string Name)
		{
			if (FileExists(Name))
			{
				byte[] tempBuffer = GetFileBytes(Name);
				if (tempBuffer != null)
				{
					System.IO.TextReader txtReader = new System.IO.StreamReader(new System.IO.MemoryStream(tempBuffer));
					return txtReader.ReadToEnd();
				}
				else
					return null;
			}
			else
				throw new Exception("File does not exsist!");
		}
		public string GetFileText(File File)
		{
			byte[] tempBuffer = GetFileBytes(File.Name);
			if (tempBuffer != null)
			{
				System.IO.TextReader txtReader = new System.IO.StreamReader(new System.IO.MemoryStream(tempBuffer));
				return txtReader.ReadToEnd();
			}
			else
				return null;
		}

		public System.IO.Stream GetFileStream(string Name)
		{
			System.IO.MemoryStream ObjToReturn = new System.IO.MemoryStream(GetFileBytes(Name));
			return ObjToReturn;
		}
		public System.IO.Stream GetFileStream(File File)
		{
			System.IO.MemoryStream ObjToReturn = new System.IO.MemoryStream(GetFileBytes(File.Name));
			return ObjToReturn;
		}

		public List<string> GetFileNames()
		{
			List<string> tmpList = new List<string>();
			foreach (File file in m_Files)
			{
				tmpList.Add(file.Name);
			}
			return tmpList;
		}
		private void Read(long Position)
		{
			System.IO.BinaryReader reader = new System.IO.BinaryReader(m_FileStream);
			reader.BaseStream.Position = Position;
			List<Folder> folders = new List<Folder>();
			sPk2EntryBlock entryBlock = (sPk2EntryBlock)BufferToStruct(m_Blowfish.Decode(reader.ReadBytes(Marshal.SizeOf(typeof(sPk2EntryBlock)))), typeof(sPk2EntryBlock));



			for (int i = 0; i < 20; i++)
			{
				sPk2Entry entry = entryBlock.Entries[i]; //.....
				switch (entry.Type)
				{
					case 0: //Null Entry

						break;
					case 1: //Folder 
						if (entry.Name != "." && entry.Name != "..")
						{
							Folder folder = new Folder();
							folder.Name = entry.Name;
							folder.Position = BitConverter.ToInt64(entry.g_Position, 0);
							folders.Add(folder);
							m_Folders.Add(folder);
							m_CurrentFolder.SubFolders.Add(folder);
						}
						break;
					case 2: //File
						File file = new File();
						file.Position = entry.Position;
						file.Name = entry.Name;
						file.Size = entry.Size;
						file.ParentFolder = m_CurrentFolder;
						m_Files.Add(file);
						m_CurrentFolder.Files.Add(file);
						break;
				}

			}
			if (entryBlock.Entries[19].NextChain != 0)
			{
				Read(entryBlock.Entries[19].NextChain);
			}


			foreach (Folder folder in folders)
			{
				m_CurrentFolder = folder;
				if (folder.Files == null)
				{
					folder.Files = new List<File>();
				}
				if (folder.SubFolders == null)
				{
					folder.SubFolders = new List<Folder>();
				}
				Read(folder.Position);
			}

		}
		#endregion

		#region Structures
		object BufferToStruct(byte[] buffer, Type returnStruct)
		{
			IntPtr pointer = Marshal.AllocHGlobal(buffer.Length);
			Marshal.Copy(buffer, 0, pointer, buffer.Length);
			return Marshal.PtrToStructure(pointer, returnStruct);
		}
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct sPk2Header
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
			public string Name;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] Version;
			[MarshalAs(UnmanagedType.I1, SizeConst = 1)]
			public byte Encryption;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Verify;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 205)]
			public byte[] Reserved;

		}
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct sPk2Entry
		{
			[MarshalAs(UnmanagedType.I1)]
			public byte Type; //files are 2, folger are 1, null entries re 0
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
			public string Name;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] AccessTime;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] CreateTime;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] ModifyTime;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] g_Position;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			private byte[] m_Size;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			private byte[] m_NextChain;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public byte[] Padding;

			public long NextChain { get { return BitConverter.ToInt64(m_NextChain, 0); } }
			public long Position { get { return BitConverter.ToInt64(g_Position, 0); } }
			public uint Size { get { return BitConverter.ToUInt32(m_Size, 0); } }
		}
		[StructLayout(LayoutKind.Sequential, Size = 2560)]
		public struct sPk2EntryBlock
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public sPk2Entry[] Entries;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			m_CurrentFolder = null;
			m_EntryBlocks = null;
			m_Files = null;
			m_FileStream = null;
			m_Folders = null;
			m_Key = null;
			m_Key_Ascii = null;
			m_MainFolder = null;
			m_Path = null;
			m_Size = 0;
		}
		#endregion
	}
}
