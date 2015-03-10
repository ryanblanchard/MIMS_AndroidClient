using System;
using SQLite;
using System.Threading.Tasks;
using Android.Util;
using Log = Android.Util.Log;


namespace MIMSPhotoUploader
{
	public class GSMMobileDB
	{
		string tag = "class GSMMobileDB";
		string folder = "/data/data/";
		string fileName = "GMSMobileDB";
		string result;

		public GSMMobileDB () 
		{
			Log.Info (tag, "GSMMobileDB constructor");

			//string pathToDatabase = System.IO.Path.Combine(folder, fileName);
			string pathToDatabase = System.IO.Path.Combine(fileName);
			result = createDatabase(pathToDatabase);
			result = result + "\n";
		}

		public string GetDatabasePath()
		{
			//string pathToDatabase = System.IO.Path.Combine(folder, fileName);
			string pathToDatabase = System.IO.Path.Combine(fileName);
			return pathToDatabase;
		}

		private string createDatabase(string path)
		{
			try
			{
				var connection = new SQLiteAsyncConnection(path);
				connection.CreateTableAsync<MaterialSrc>();
				return "Database created";
			}
			catch (SQLiteException ex)
			{
				return ex.Message;
			}
		}


	}


}

