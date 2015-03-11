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

					var conn = MIMSPhotoUploader.dbBorrowPit.ConnectToDB();

					//var conn = new SQLiteConnection (App._dbFileName);

					button.Text = string.Format ("{0}", "Connected");

				} catch (Exception ex) {

					button.Text = string.Format ("Ex: {0}", ex.Message);
				}

			};

		}



	}
}


