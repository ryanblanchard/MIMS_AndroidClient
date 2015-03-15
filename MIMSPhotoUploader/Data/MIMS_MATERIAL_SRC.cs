using System;
using SQLite;
using System.Collections.Generic;

namespace MIMSPhotoUploader
{
	[Table("MIMS_MATERIAL_SRC")]
	public class MIMS_MATERIAL_SRC
	{
		[PrimaryKey]
		public int ID { get; set;}
		public string MATERIAL_SRC_NO { get; set;}
		public string BORROWPIT_NO { get; set;}
		public string ROAD_NO { get; set;}
		public double KILOMETER_POSITION { get; set;}
		public string OFFSET_LEFT_RIGHT { get; set;}
		public double OFFSET_IN_METERS { get; set;}
		public double DEGREES_DECIMAL_X { get; set;}
		public double DEGREES_DECIMAL_Y { get; set;}

	}



}
	/*
		public void CreateMaterialSrcTable()
		{
			//var db = 
		}

		public List<MIMS_MATERIAL_SRC> GetMaterialSourceList()
		{
			with (lock locker)
			{

			}
			var table = 
		}

		public static void DoSomeDataAccess () 
		{
			Console.WriteLine ("Creating database, if it doesn't already exist");
			string dbPath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"ormdemo.db3");
			var db = new SQLiteConnection (dbPath);
			db.CreateTable<Stock> ();
			if (db.Table<Stock> ().Count() == 0) {
				// only insert the data if it doesn't already exist
				var newStock = new Stock ();
				newStock.Symbol = "AAPL";
				db.Insert (newStock);
				newStock = new Stock ();
				newStock.Symbol = "GOOG";
				db.Insert (newStock);
				newStock = new Stock ();
				newStock.Symbol = "MSFT";
				db.Insert (newStock);
			}
			Console.WriteLine("Reading data");
			var table = db.Table<Stock> ();
			foreach (var s in table) {
				Console.WriteLine (s.Id + " " + s.Symbol);
			}
	}}

*/