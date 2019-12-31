using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace xBot.App
{
	public class SQLDatabase
	{
		private string Path { get; }
		private SQLiteConnection db;
		private SQLiteCommand q;
		public SQLDatabase(string Path)
		{
			this.Path = Path;
		}
		/// <summary>
		/// Creates a zero-byte database file to be used correctly by SQLite. Return success.
		/// </summary>
		public bool Create()
		{
			try
			{
				SQLiteConnection.CreateFile(Path);
				return true;
			}
			catch { return false; }
		}
		/// <summary>
		/// Connect database. Returns sucess.
		/// </summary>
		public bool Connect()
		{
			try
			{
				db = new SQLiteConnection("Data Source=" + Path + ";Version=3;");
				q = new SQLiteCommand(db);
				q.CommandTimeout = 1000; // Wait for queue to execute query
				db.Open();
				return true;
			}
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// Execute SQL query and return the number of columns affected. Returns (-1) if the database is not connected.
		/// </summary>
		/// <param name="sql">SQLite query</param>
		public int ExecuteQuery(string sql)
		{
			if (db != null)
			{
				q.CommandText = sql;
				return q.ExecuteNonQuery();
			}
			return -1;
		}
		/// <summary>
		/// Execute query previously prepared and return the number of columns affected. Returns (-1) if the database is not connected.
		/// </summary>
		public int ExecuteQuery()
		{
			if (db != null)
				return q.ExecuteNonQuery();
			return -1;
		}
		/// <summary>
		/// Prepares SQL query.
		/// </summary>
		/// <param name="sql">SQLite query</param>
		/// <returns>Sucess</returns>
		public bool Prepare(string sql)
		{
			if (db != null)
			{
				q.Parameters.Clear();
				q.CommandText = sql;
				return true;
			}
			return false;
		}
		/// <summary>
		/// Bind column value to the query previously prepared.
		/// </summary>
		/// <param name="column">Column name</param>
		/// <param name="value">Value to bind</param>
		public void Bind(string column,object value)
		{
			q.Parameters.Add(new SQLiteParameter(column, value));
		}
		public List<NameValueCollection> GetResult()
		{
			List<NameValueCollection> result = new List<NameValueCollection>();
			SQLiteDataReader reader = q.ExecuteReader();
      while (reader.Read())
				result.Add(reader.GetValues());
			reader.Close();
      return result;
		}
		public List<NameValueCollection> GetResultFromQuery(string sql)
		{
			List<NameValueCollection> result = new List<NameValueCollection>();
			if (db != null)
			{
				SQLiteCommand q = new SQLiteCommand(sql,db);
				q.CommandTimeout = 1000;
				q.ExecuteNonQuery();
				SQLiteDataReader reader = q.ExecuteReader();
				while (reader.Read())
					result.Add(reader.GetValues());
				reader.Close();
        q.Dispose();
			}
			return result;
		}
		public void Begin()
		{
			ExecuteQuery("BEGIN");
		}
		public void End()
		{
			ExecuteQuery("END");
		}
		public void Close()
		{
			if (db != null && db.State != System.Data.ConnectionState.Closed)
			{
				q.Dispose();
				db.Close();
				db = null;
			}
		}
	}
}