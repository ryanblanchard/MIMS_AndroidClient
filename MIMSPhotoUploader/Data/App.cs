using System;
using Android.Graphics;
using Java.IO;
using SQLite;


namespace MIMSPhotoUploader
{
	public static class App{
		public static string _dbFileName;
		public static string _username;
		public static DateTime _date;
		public static string _desc;
		public static Bitmap bitmap;
		public static File _file;
		public static File _dir;     
		public static string _lat;
		public static string _long;
	}
}
