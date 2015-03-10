using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;

namespace MIMSPhotoUploader
{
	[Activity (Label = "MIMSPhotoUploader", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);

				//var db = new GSMMobileDB();

				//var qry = from a in db.Material
				string fileName = "GMSMobileDB";
				string dbPath  = System.IO.Path.Combine(fileName);
				var db = new SQLiteConnection (dbPath);

				//var stock = db.Get<MaterialSrc>(5); // primary key id of 5
				var stockList = db.Table<MaterialSrc>();
				int recCnt = stockList.Count();

				button.Text = string.Format ("{0} pits!", recCnt);

			};
		}
	}
}


