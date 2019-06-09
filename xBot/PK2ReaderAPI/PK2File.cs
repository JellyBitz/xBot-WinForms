namespace PK2ReaderAPI
{
	public class PK2File
	{
		private string m_Name;
		private long m_Position;
		private uint m_Size;
		private PK2Folder m_ParentFolder;
		public string Name { get { return m_Name; } set { m_Name = value; } }
		public long Position { get { return m_Position; } set { m_Position = value; } }
		public uint Size { get { return m_Size; } set { m_Size = value; } }
		public PK2Folder ParentFolder { get { return m_ParentFolder; } set { m_ParentFolder = value; } }
	}
}