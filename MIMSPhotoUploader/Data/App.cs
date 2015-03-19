using System;
using Android.Graphics;
using Java.IO;
using SQLite;


namespace MIMSPhotoUploader
{
	public static class App{
		public static string _dbFileName;
		public static string _fullDbPath;
		public static string _username;
		public static DateTime _date;
		public static string _desc;
		public static Bitmap bitmap;
		public static File _file;
		public static File _dir;     
		public static string _lat;
		public static string _long;
		public static string _borrowPitID;
		public static string _borrowpitName;
		public static string _roadNo;
		public static string _photoID;
		public static string _photoFileName;
		public static string _photoCategory;
		public static string _photoCategoryID;
	}
}
