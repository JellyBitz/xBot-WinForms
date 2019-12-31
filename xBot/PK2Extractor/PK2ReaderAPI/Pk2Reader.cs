using SecurityAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace xBot.PK2Extractor.PK2ReaderAPI
{
	public class Pk2Reader : IDisposable
	{
		#region Properties & Member variables
		private Blowfish m_Blowfish = new Blowfish();
		
		public long Size { get; private set; }
		public byte[] Key { get; private set; }
		public string ASCIIKey { get; private set; }
		public string FullPath { get; }

		public sPk2Header Header { get; private set; }
		private Dictionary<string, Pk2File> m_Files = new Dictionary<string, Pk2File>();
		public List<Pk2File> Files { get { return new List<Pk2File>(m_Files.Values); } }
		private Dictionary<string, Pk2Folder> m_Folders = new Dictionary<string, Pk2Folder>();
		public List<Pk2Folder> Folders { get { return new List<Pk2Folder>(m_Folders.Values); } }

		private Pk2Folder m_CurrentFolder;
		private Pk2Folder m_MainFolder;
		private FileStream m_FileStream;
		#endregion

		#region Constructor
		public Pk2Reader(string FilePath, string BlowfishKey)
		{
			if (!File.Exists(FilePath))
				throw new Exception("File not found");
			else
			{
				this.FullPath = Path.GetFullPath(FilePath);
				// Set default key for most clients
				if (BlowfishKey == "")
					ASCIIKey = "169841";
				else
					ASCIIKey = BlowfishKey;
				Key = GenerateFinalBlowfishKey(ASCIIKey);

				m_FileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
				Size = m_FileStream.Length;

				m_Blowfish.Initialize(Key);
				BinaryReader reader = new BinaryReader(m_FileStream);
				Header = (sPk2Header)BufferToStruct(reader.ReadBytes(256), typeof(sPk2Header));

				m_CurrentFolder = new Pk2Folder();
				m_CurrentFolder.Name = FilePath;
				m_CurrentFolder.Files = new List<Pk2File>();
				m_CurrentFolder.SubFolders = new List<Pk2Folder>();

				m_MainFolder = m_CurrentFolder;
				Read(reader.BaseStream.Position,"");
			}
		}
		#endregion

		#region Functions & Methods
		/// <summary>
		/// Read the Pk2 structure recursively.
		/// </summary>
		private void Read(long Position, string RootPath)
		{
			BinaryReader reader = new BinaryReader(m_FileStream);
			reader.BaseStream.Position = Position;
			List<Pk2Folder> folders = new List<Pk2Folder>();
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
							Pk2Folder folder = new Pk2Folder();
							folder.Name = entry.Name;
							folder.Position = BitConverter.ToInt64(entry.g_Position, 0);
							folders.Add(folder);
							m_Folders[(RootPath + entry.Name).ToUpper()] = folder;
							m_CurrentFolder.SubFolders.Add(folder);
						}
						break;
					case 2: //File
						Pk2File file = new Pk2File();
						file.Position = entry.Position;
						file.Name = entry.Name;
						file.Size = entry.Size;
						file.ParentFolder = m_CurrentFolder;
						m_Files[(RootPath + entry.Name).ToUpper()] = file;
						m_CurrentFolder.Files.Add(file);
						break;
				}

			}
			if (entryBlock.Entries[19].NextChain != 0)
			{
				Read(entryBlock.Entries[19].NextChain, RootPath);
			}

			foreach (Pk2Folder folder in folders)
			{
				m_CurrentFolder = folder;
				if (folder.Files == null)
				{
					folder.Files = new List<Pk2File>();
				}
				if (folder.SubFolders == null)
				{
					folder.SubFolders = new List<Pk2Folder>();
				}
				Read(folder.Position, RootPath + folder.Name + "\\");
			}
		}
		/// <summary>
		/// Close the Pk2 file.
		/// </summary>
		public void Close()
		{
			m_FileStream.Close();
		}		
		/// <summary>
		/// Gets all the root files.
		/// </summary>
		public List<Pk2File> GetRootFiles()
		{
			return m_MainFolder.Files;
		}
		/// <summary>
		/// Gets all the root folders.
		/// </summary>
		public List<Pk2Folder> GetRootFolders()
		{
			return m_MainFolder.SubFolders;
		}
		public Pk2Folder GetFolder(string FolderPath)
		{
			if (FolderPath == "")
				return null;

			// Normalize to the same dictionary key path format
			FolderPath = FolderPath.ToUpper();
			FolderPath = FolderPath.Replace("/", "\\");
			if (FolderPath.EndsWith("\\"))
				FolderPath = FolderPath.Substring(0, FolderPath.Length - 1);

			Pk2Folder folder = null;
			m_Folders.TryGetValue(FolderPath, out folder);
			return folder;
		}
		/// <summary>
		/// Get a file from Pk2 path specified.
		/// </summary>
		public Pk2File GetFile(string FilePath)
		{
			if (FilePath == "")
				return null;

			// Normalize to the same dictionary key path format
			FilePath = FilePath.ToUpper();
			FilePath = FilePath.Replace("/", "\\");

			Pk2File file = null;
			m_Files.TryGetValue(FilePath, out file);
			return file;
		}
		/// <summary>
		/// Extract the bytes from the file.
		/// </summary>
		public byte[] GetFileBytes(Pk2File File)
		{
			BinaryReader reader = new BinaryReader(m_FileStream);
			reader.BaseStream.Position = File.Position;
			return reader.ReadBytes((int)File.Size);
		}
		/// <summary>
		/// Extract the bytes from Pk2 path specified.
		/// </summary>
		public byte[] GetFileBytes(string Path)
		{
			BinaryReader reader = new BinaryReader(m_FileStream);
			Pk2File file = GetFile(Path);
			reader.BaseStream.Position = file.Position;
			return reader.ReadBytes((int)file.Size);
		}
		/// <summary>
		/// Extract the stream from the file.
		/// </summary>
		public Stream GetFileStream(Pk2File File)
		{
			return new MemoryStream(GetFileBytes(File));
		}
		/// <summary>
		/// Extract the stream from Pk2 path specified.
		/// </summary>
		public Stream GetFileStream(string Path)
		{
			return new MemoryStream(GetFileBytes(Path));
		}
		/// <summary>
		/// Extract the string text from the file.
		/// </summary>
		public string GetFileText(Pk2File File)
		{
			byte[] tempBuffer = GetFileBytes(File);
			if (tempBuffer != null)
			{
				TextReader txtReader = new StreamReader(new MemoryStream(tempBuffer));
				return txtReader.ReadToEnd();
			}
			return null;
		}
		/// <summary>
		/// Extract the string text from Pk2 path specified.
		/// </summary>
		public string GetFileText(string Path)
		{
			byte[] tempBuffer = GetFileBytes(Path);
			if (tempBuffer != null){
				TextReader txtReader = new StreamReader(new MemoryStream(tempBuffer));
				return txtReader.ReadToEnd();
			}
			return null;
		}
		/// <summary>
		/// Extract a file from the buffer to an output path.
		/// </summary>
		public void ExtractFile(Pk2File File, string OutputPath)
		{
			byte[] data = GetFileBytes(File);
			FileStream stream = new FileStream(OutputPath, FileMode.Create);
			BinaryWriter writer = new BinaryWriter(stream);
			writer.Write(data);
			stream.Close();
    }
		/// <summary>
		/// Get the file extension from the file.
		/// </summary>
		public string GetFileExtension(Pk2File File)
		{
			int Offset = File.Name.LastIndexOf('.');
			return File.Name.Substring(Offset);
		}
		/// <summary>
		/// Get files from Pk2 path specified.
		/// </summary>
		public List<Pk2File> GetFiles(string Path)
		{
			List<Pk2File> files = new List<Pk2File>();

			Pk2Folder folder = null;
			m_Folders.TryGetValue(Path, out folder);

			if (folder != null)
				files.AddRange(folder.Files);
			return files;
		}
		/// <summary>
		/// Get folders from Pk2 path specified.
		/// </summary>
		public List<Pk2Folder> GetSubFolders(string Path)
		{
			List<Pk2Folder> folders = new List<Pk2Folder>();

			Pk2Folder folder = null;
			m_Folders.TryGetValue(Path, out folder);

			if (folder != null)
				folders.AddRange(folder.SubFolders);
			return folders;
		}
		/// <summary>
		/// Check if a file exists in the Pk2 path specified.
		/// </summary>
		public bool FileExists(string FileName,string Path)
		{
			Pk2Folder folder = null;
			m_Folders.TryGetValue(Path, out folder);

			if (folder != null)
				return folder.Files.Exists(file => file.Name.Equals(FileName, StringComparison.OrdinalIgnoreCase));
			return false;
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
			m_Files = null;
			m_FileStream = null;
			m_Folders = null;
			Key = null;
			ASCIIKey = null;
			m_MainFolder = null;
			Size = 0;
		}
		#endregion
	}
}