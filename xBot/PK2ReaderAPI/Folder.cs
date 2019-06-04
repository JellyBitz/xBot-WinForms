using System.Collections.Generic;
namespace PK2ReaderAPI
{
	public class Folder
	{
		private string m_Name;
		private long m_Position;
		private List<File> m_Files;
		private List<Folder> m_SubFolders;

		public string Name { get { return m_Name; } set { m_Name = value; } }
		public long Position { get { return m_Position; } set { m_Position = value; } }
		public List<File> Files { get { return m_Files; } set { m_Files = value; } }
		public List<Folder> SubFolders { get { return m_SubFolders; } set { m_SubFolders = value; } }

	}
}
