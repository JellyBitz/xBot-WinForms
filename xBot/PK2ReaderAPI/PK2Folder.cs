using System.Collections.Generic;
namespace PK2ReaderAPI
{
	public class PK2Folder
	{
		private string m_Name;
		private long m_Position;
		private List<PK2File> m_Files;
		private List<PK2Folder> m_SubFolders;

		public string Name { get { return m_Name; } set { m_Name = value; } }
		public long Position { get { return m_Position; } set { m_Position = value; } }
		public List<PK2File> Files { get { return m_Files; } set { m_Files = value; } }
		public List<PK2Folder> SubFolders { get { return m_SubFolders; } set { m_SubFolders = value; } }

	}
}
