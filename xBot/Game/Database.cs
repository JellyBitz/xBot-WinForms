using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
namespace xBot.Game
{
	public class Database
	{
		/// <summary>
		/// Unique name to identify the silkroad database.
		/// </summary>
		public string Name { get; }
		public string FileName { get; }
		private SQLiteConnection db;
		private SQLiteCommand q;
		public Database(string Name)
		{
			this.Name = Name;
			this.FileName = "Data\\" + Name + ".db";
		}
		/// <summary>
		/// Creates a zero-byte database file to be used correctly by SQLite. Return success.
		/// </summary>
		public bool Create()
		{
			try
			{
				SQLiteConnection.CreateFile(FileName);
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
				db = new SQLiteConnection("Data Source=" + FileName + ";Version=3;");
				q = new SQLiteCommand(db);
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
			using (SQLiteDataReader reader = q.ExecuteReader())
			{
				while (reader.Read())
				{
					result.Add(reader.GetValues());
				}
			}
			return result;
		}
		public void Begin()
		{
			ExecuteQuery("begin");
		}
		public void End()
		{
			ExecuteQuery("end");
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
		public static bool Exists(string Name)
		{
			return File.Exists("Data\\" + Name + ".db");
		}
		public static bool Delete(string Name)
		{
			try
			{
				if (Exists(Name))
				{
					File.Delete("Data\\" + Name + ".db");
				}
				return true;
			}
			catch { return false; }
		}
	}
}