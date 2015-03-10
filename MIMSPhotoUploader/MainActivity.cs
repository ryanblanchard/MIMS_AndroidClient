using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using System.IO;
using Android.Graphics;
using Java.IO;
using MIMSPhotoUploader.Data;

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

			if (bundle == null) {
				//App app = new App ();
			}

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {

				button.Text = string.Format ("{0} clicks!", count++);

				try {

					ConnectToDB();

					var conn = new SQLiteConnection (App._dbFileName);

					//conn.Table<MIMS_MATERIAL_SRC>.

					//var s = from e in conn.Table<MIMS_MATERIAL_SRC>()
//					//		where (1 == 1)
					//		select e;
					//
					//foreach (var src in s.
				//	{
				//		System.Console.WriteLine(src.MATERIAL_SRC_NO.ToString());
				//	}
						



					button.Text = string.Format ("Connected");

				} catch (Exception ex) {
					button.Text = string.Format ("Ex: {0}", ex.Message);
				}

			};

		}

		static void ConnectToDB ()
		{
			string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			folder = "/storage/emulated/0/";
			App._dbFileName = System.IO.Path.Combine (folder, "GMSMobileDB.s3db");
			var conn = new SQLiteConnection (App._dbFileName);
			//conn.CreateTable<Stock> ();
			System.Console.WriteLine (App._dbFileName);
		}

	}
}


