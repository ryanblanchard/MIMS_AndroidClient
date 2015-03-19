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

		public static string dbFileName = "GMSMobileDBv0p2.s3db";
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

				//folder = "/storage/emulated/0/";
				App._dbFileName = System.IO.Path.Combine (folder, dbFileName);
				Log.Info("ConnectToDB", App._dbFileName);

				if (!System.IO.File.Exists(App._dbFileName))
				{
					Log.Info("**** ConnectToDB (NOT exist) ****", App._dbFileName);

					//HelperUtils.CopyInitialDBtoFinal(dbFileName, DB_file.Path);
					Log.Info("**** Copy Initial Database File {0} to final target path {1}****", App._dbFileName,  DB_file.Path);
				}
	
				var conn = new SQLiteConnection (App._dbFileName);
				//conn.CreateTable<Stock> ();
				System.Console.WriteLine (App._dbFileName);
				return conn;
			} 
			catch (Exception ex) 
			{
				string tag = "Database Error";
				Log.Info(tag,"**** ConnectToDB (Exception) Path = {0} **** {1}", App._dbFileName, ex.Message);
				return null;
			}

		}



		public static string IsNull(object InputObject, string ReplaceString)
		{
			if (InputObject == null)
				return ReplaceString;
			else
				return InputObject.ToString();
		}

		public decimal IsNull(object InputObject, decimal ReplacementNumber)
		{
			decimal returnVal;
			if (InputObject == null)
			{
				return ReplacementNumber;
			}
			else
			{
				if (decimal.TryParse(InputObject.ToString(), out returnVal))
				{
					return returnVal;
				}
				else
				{
					return ReplacementNumber;
				}
			}
		}

	}
}

