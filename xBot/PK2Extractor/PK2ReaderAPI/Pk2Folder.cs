using System.Collections.Generic;
namespace xBot.PK2Extractor.PK2ReaderAPI
{
	public class Pk2Folder
	{
		private string m_Name;
		private long m_Position;
		private List<Pk2File> m_Files;
		private List<Pk2Folder> m_SubFolders;

		public string Name { get { return m_Name; } set { m_Name = value; } }
		public long Position { get { return m_Position; } set { m_Position = value; } }
		public List<Pk2File> Files { get { return m_Files; } set { m_Files = value; } }
		public List<Pk2Folder> SubFolders { get { return m_SubFolders; } set { m_SubFolders = value; } }

	}
}
