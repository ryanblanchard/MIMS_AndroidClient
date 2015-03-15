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
using MIMSPhotoUploader;
using Android.Util;

namespace MIMSPhotoUploader
{


	[Activity (Label = "MIMSPhotoUploader", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		string tag = "MainActivity";


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			if (bundle == null) {
				//App app = new App ();
			}

			TextView textMainTitle = FindViewById<TextView> (Resource.Id.textMainTitle);
			TextView textGPSNote =  FindViewById<TextView> (Resource.Id.textGPSNote);

			TextView labelGPSPos = FindViewById<TextView> (Resource.Id.labelGPSPos);
			TextView textGPSLat = FindViewById<TextView> (Resource.Id.textGPSLat);
			TextView textGPSLong = FindViewById<TextView> (Resource.Id.textGPSLong);



			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Click += delegate {
				Log.Info(tag, "button.Click");
				StartActivity(typeof(BorrowpitListActivity));

			};

			Button btnPhotos = FindViewById<Button> (Resource.Id.myPhotos);
			btnPhotos.Click += delegate {
				Log.Info(tag, "btnPhotos.Click");
				StartActivity(typeof(PhotoListActivity));
			};

			Button btnQuickAdd = FindViewById<Button> (Resource.Id.btnQuickAdd);
			btnQuickAdd.Click += delegate {
				Log.Info(tag, "btnQuickAdd.Click");
				StartActivity(typeof(AddPhotoActivity));
			};

		}

	}
}


