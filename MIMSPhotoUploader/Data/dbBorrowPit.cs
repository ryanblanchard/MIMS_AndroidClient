using System;
using System.Collections.Generic;
using SQLite;
using MIMSPhotoUploader.Data;


namespace MIMSPhotoUploader
{
	public class dbBorrowPit
	{
		public dbBorrowPit ()
		{
			//
		}

		public static SQLiteConnection ConnectToDB ()
		{
			try {
				
				string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				folder = "/storage/emulated/0/";
				App._dbFileName = System.IO.Path.Combine (folder, "GMSMobileDB.s3db");
				var conn = new SQLiteConnection (App._dbFileName);
				//conn.CreateTable<Stock> ();
				System.Console.WriteLine (App._dbFileName);
				return conn;
			} 
			catch (Exception ex) 
			{
				return null;
			}

		}

	}
}

