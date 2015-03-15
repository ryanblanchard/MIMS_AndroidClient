using System;
using System.Collections.Generic;
using SQLite;
using MIMSPhotoUploader;
using Android.Util;
using Java.IO;

namespace MIMSPhotoUploader
{
	public class dbBorrowPit
	{

		static string dbFileName = "GMSMobileDBv0p2.s3db";
		static File DB_file;

		public dbBorrowPit ()
		{
			//
		}

		private void CreateDirectoryForDatabase()
		{

		}

		public static SQLiteConnection ConnectToDB ()
		{
			try {

				DB_file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments), "MIMSBorrowpitPhotos_DATA");
				if (!DB_file.Exists())
				{
					DB_file.Mkdirs();
					Log.Info("CREATE CreateDirectoryForDatabase","Data Directory:{0}", DB_file);
				}
				Log.Info("CreateDirectoryForDatabase","Data Directory:{0}", DB_file);
				
				string folder = DB_file.Path;

				Log.Info("SQLiteConnection", string.Format("Special Folder . Personal = {0}, " ,folder));

				//folder = "/storage/emulated/0/";
				App._dbFileName = System.IO.Path.Combine (folder, dbFileName);
				Log.Info("ConnectToDB", App._dbFileName);

				if (!System.IO.File.Exists(App._dbFileName))
				{
					Log.Info("**** ConnectToDB (NOT exist) ****", App._dbFileName);
					string cpath =System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
					//string initdb = System.IO.Path.Combine(cpath,"Assets",  "BaseLineDB.s3db");
					//System.IO.File.Copy(initdb,App._dbFileName);

					//run create table scripts here
				}

	
				var conn = new SQLiteConnection (App._dbFileName);
				//conn.CreateTable<Stock> ();
				System.Console.WriteLine (App._dbFileName);
				return conn;
			} 
			catch (Exception ex) 
			{
				Log.Info("**** ConnectToDB (Exception) **** {0}", ex.Message);
				return null;
			}

		}

	}
}

