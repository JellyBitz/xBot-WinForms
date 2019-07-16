using SecurityAPI;
using System;
using System.Collections.Generic;
using System.IO;
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

		private List<PK2File> m_Files = new List<PK2File>();
		public List<PK2File> Files { get { return m_Files; } }

		private List<PK2Folder> m_Folders = new List<PK2Folder>();
		public List<PK2Folder> Folders { get { return m_Folders; } }

		private string m_Path;
		public string Path { get { return m_Path; } }

		private PK2Folder m_CurrentFolder;
		private PK2Folder m_MainFolder;
		private System.IO.FileStream m_FileStream;
		#endregion

		#region Constructor
		public PK2Reader(string FileName, string BlowfishKey)
		{
			if (!File.Exists(FileName))
				throw new Exception("File not found");
			else
			{
				m_Path = FileName;
				// Set default key for most clients
				if (BlowfishKey == "")
					m_Key_Ascii = "169841";
				else
					m_Key_Ascii = BlowfishKey;
				m_Key = GenerateFinalBlowfishKey(m_Key_Ascii);

				m_FileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				m_Size = m_FileStream.Length;

				m_Blowfish.Initialize(m_Key);
				BinaryReader reader = new BinaryReader(m_FileStream);
				m_Header = (sPk2Header)BufferToStruct(reader.ReadBytes(256), typeof(sPk2Header));
				m_CurrentFolder = new PK2Folder();
				m_CurrentFolder.Name = FileName;
				m_CurrentFolder.Files = new List<PK2File>();
				m_CurrentFolder.SubFolders = new List<PK2Folder>();

				m_MainFolder = m_CurrentFolder;
				Read(reader.BaseStream.Position);
			}
		}
		#endregion

		#region Functions & Methods
		public void ExtractFile(PK2File File, string OutputPath)
		{
			Byte[] data = GetFileBytes(File);
			FileStream stream = new FileStream(OutputPath, FileMode.OpenOrCreate);
			BinaryWriter writer = new BinaryWriter(stream);
			writer.Write(data);

		}
		public string GetFileExtension(PK2File File)
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
		public List<PK2File> GetRootFiles()
		{
			return m_MainFolder.Files;
		}
		public List<PK2Folder> GetRootFolders()
		{
			return m_MainFolder.SubFolders;
		}

		public List<PK2File> GetFiles(string ParentFolder)
		{
			List<PK2File> ObjToReturn = new List<PK2File>();
			foreach (PK2File file in Files)
			{
				if (file.ParentFolder.Name == ParentFolder)
				{
					ObjToReturn.Add(file);
				}
			}
			return ObjToReturn;
		}
		public List<PK2Folder> GetSubFolders(string ParentFolder)
		{
			List<PK2Folder> ObjToReturn = new List<PK2Folder>();
			foreach (PK2Folder folder in Folders)
			{
				if (folder.Name == ParentFolder)
				{
					foreach (PK2Folder subFolder in folder.SubFolders)
					{
						ObjToReturn.Add(subFolder);
					}
				}
			}
			return ObjToReturn;
		}
		/// <summary>
		/// Check if a file name exists into the PK2Files loaded.
		/// </summary>
		/// <param name="Name">PK2File name</param>
		public bool FileExists(string Name)
		{
			PK2File file = m_Files.Find(item => item.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
			return file != null && file.Position != 0;
		}
		/// <summary>
		/// Extract the bytes from the PK2 File.
		/// </summary>
		public byte[] GetFileBytes(PK2File File)
		{
			BinaryReader reader = new BinaryReader(m_FileStream);
			reader.BaseStream.Position = File.Position;
			return reader.ReadBytes((int)File.Size);
		}
		/// <summary>
		/// Extract the bytes from the PK2 File name in the path specified.
		/// </summary>
		public byte[] GetFileBytes(string Name, string Path="")
		{
			BinaryReader reader = new BinaryReader(m_FileStream);
			PK2File file = GetFile(Name, Path);
			reader.BaseStream.Position = file.Position;
			return reader.ReadBytes((int)file.Size);
		}
		/// <summary>
		/// Extract the stream from the PK2File.
		/// </summary>
		public Stream GetFileStream(PK2File File)
		{
			return new MemoryStream(GetFileBytes(File));
		}
		/// <summary>
		/// Extract the stream from the PK2File name in the path specified.
		/// </summary>
		/// <param name="File"></param>
		public Stream GetFileStream(string Name,string Path="")
		{
			return new MemoryStream(GetFileBytes(Name,Path));
		}
		/// <summary>
		/// Look for the PK2File name in the path specified.
		/// </summary>
		/// <param name="Name">PK2File name</param>
		/// <param name="Path">PK2Folders Path separated by "/" or Empty to specify the root</param>
		public PK2File GetFile(string Name, string Path="")
		{
			if (Path == "")
				return GetRootFiles().Find(f => f.Name.Equals(Name,StringComparison.OrdinalIgnoreCase));

			List<string> Route = new List<string>();
			Route.AddRange(Path.Split('/')); // split path

			return SearchFileFromPath(Name,GetRootFolders(),Route);
		}
		/// <summary>
		/// Search recursively the exact pk2 path.
		/// </summary>
		private PK2File SearchFileFromPath(string Name,List<PK2Folder> Folders, List<string> Path)
		{
			PK2File File = null;
			foreach (PK2Folder Folder in Folders)
			{
				if (Folder.Name.Equals(Path[0], StringComparison.OrdinalIgnoreCase))
				{
					if (Path.Count == 1)
					{
						return Folder.Files.Find(f => f.Name.Equals(Name, StringComparison.OrdinalIgnoreCase) );
					}
					else
					{
						List<string> copyPath = new List<string>(Path);
						copyPath.Remove(Folder.Name);
						File = SearchFileFromPath(Name, Folder.SubFolders, copyPath);
						if (File != null)
							break;
					}
				}
			}
			return File;
    }
		/// <summary>
		/// Look for the PK2File name in the path specified.
		/// </summary>
		/// <param name="Name">PK2File name</param>
		/// <param name="Path">PK2Folders Path separated by "/" or Empty to specify the root</param>
		public string GetFileText(string Name, string Path="")
		{
			byte[] tempBuffer = GetFileBytes(GetFile(Name,Path));
			if (tempBuffer != null)
			{
				TextReader txtReader = new StreamReader(new MemoryStream(tempBuffer));
				return txtReader.ReadToEnd();
			}
			return null;
		}
		public List<string> GetFileNames()
		{
			List<string> tmpList = new List<string>();
			foreach (PK2File file in m_Files)
			{
				tmpList.Add(file.Name);
			}
			return tmpList;
		}
		private void Read(long Position)
		{
			BinaryReader reader = new BinaryReader(m_FileStream);
			reader.BaseStream.Position = Position;
			List<PK2Folder> folders = new List<PK2Folder>();
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
							PK2Folder folder = new PK2Folder();
							folder.Name = entry.Name;
							folder.Position = BitConverter.ToInt64(entry.g_Position, 0);
							folders.Add(folder);
							m_Folders.Add(folder);
							m_CurrentFolder.SubFolders.Add(folder);
						}
						break;
					case 2: //File
						PK2File file = new PK2File();
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

			foreach (PK2Folder folder in folders)
			{
				m_CurrentFolder = folder;
				if (folder.Files == null)
				{
					folder.Files = new List<PK2File>();
				}
				if (folder.SubFolders == null)
				{
					folder.SubFolders = new List<PK2Folder>();
				}
				Read(folder.Position);
			}
		}
		#endregion

		#region Blowfish:Key_Generation
		private static byte[] GenerateFinalBlowfishKey(string ascii_key)
		{
			// Using the default Base_Key
			return GenerateFinalBlowfishKey(ascii_key, new byte[] { 0x03, 0xF8, 0xE4, 0x44, 0x88, 0x99, 0x3F, 0x64, 0xFE, 0x35 });
		}
		private static byte[] GenerateFinalBlowfishKey(string ascii_key, byte[] base_key)
		{
			byte ascii_key_length = (byte)ascii_key.Length;

			// Max count of 56 key bytes
			if (ascii_key_length > 56)
			{
				ascii_key_length = 56;
			}

			// Get bytes from ascii
			byte[] a_key = Encoding.ASCII.GetBytes(ascii_key);

			// This is the Silkroad bas key used in all versions
			byte[] b_key = new byte[56];

			// Copy key to array to keep the b_key at 56 bytes. b_key has to be bigger than a_key
			// to be able to xor every index of a_key.
			Array.ConstrainedCopy(base_key, 0, b_key, 0, base_key.Length);

			// Their key modification algorithm for the final blowfish key
			byte[] bf_key = new byte[ascii_key_length];
			for (byte x = 0; x < ascii_key_length; ++x)
			{
				bf_key[x] = (byte)(a_key[x] ^ b_key[x]);
			}

			return bf_key;
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