﻿using System;
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
using System.Collections.Generic;
using System.Linq;

namespace MIMSPhotoUploader
{
	[Activity (Label = "MIMSPhotoUploader", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true)]
	public class MainActivity : Activity
	{
		string tag = "MainActivity";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			App._username = "DEVELOPER01"; //TODO: Build Login Screen
			App._date = DateTime.Now;
			App._borrowpitName = "No Borrowpit Selected";

			var i = CheckPendingUploads ();
			//i = 0; //remove
			TextView textNoOfUploads = FindViewById<TextView> (Resource.Id.textNoOfUploads);
			textNoOfUploads.Text = i.ToString ();

			View ls = FindViewById<View> (Resource.Id.layoutSync);

			if (i > 0) {
				ls.Visibility = ViewStates.Visible;
			} else {
				
				ls.Visibility = ViewStates.Gone;
			}

			/*
			//copies the inital SQLite db out of Assets into the final folder 
			var db = dbBorrowPit.ConnectToDB ();
			if (db == null) {
				Java.IO.File DB_file = new Java.IO.File (Android.OS.Environment.GetExternalStoragePublicDirectory (Android.OS.Environment.DirectoryDocuments), "MIMSBorrowpitPhotos_DATA");
				CopyInitialDBtoFinal (dbBorrowPit.dbFileName, DB_file.Path);
			}
			*/
			TextView headBorrowpitName = FindViewById<TextView> (Resource.Id.headBorrowpitName);
			headBorrowpitName.Text = App._borrowpitName;
			//TextView textMainTitle = FindViewById<TextView> (Resource.Id.textMainTitle);
			TextView textGPSNote =  FindViewById<TextView> (Resource.Id.textGPSNote);

			TextView labelGPSPos = FindViewById<TextView> (Resource.Id.labelGPSPos);
			TextView textGPSLat = FindViewById<TextView> (Resource.Id.textGPSLat);
			TextView textGPSLong = FindViewById<TextView> (Resource.Id.textGPSLong);


			/*
			TextView syncStatus = FindViewById<TextView> (Resource.Id.textSyncStatus);

			if (CheckPendingUploads () > 0) {
				syncStatus.Text = GetText (Resource.String.syncStatus_PhotosToSync);
			} else {
				syncStatus.Text = GetText (Resource.String.syncStatus_NothingToSync);
			}



			EditText currDate = FindViewById<EditText> (Resource.Id.editCurentDate);
			currDate.Text = string.Format("{0:yyyy/MM/dd}", App._date);
*/

			//currDate.EditText = DateTime.Now;


			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Click += delegate {
				Log.Info(tag, "button.Click");
				//StartActivity(typeof(MIMSPhotoUploader.BorrowpitListActivity));

				Intent intent = new Intent(this,typeof(BorrowpitListActivity));
				StartActivity(intent);
			};



			Button btnPhotos = FindViewById<Button> (Resource.Id.myPhotos);
			btnPhotos.Click += delegate {
				Log.Info(tag, "btnPhotos.Click");

				Intent intent = new Intent(this,typeof(BorrowpitListActivity));
				StartActivity(typeof(PhotoListActivity));
			};

			Button btnQuickAdd = FindViewById<Button> (Resource.Id.btnQuickAdd);
			btnQuickAdd.Click += delegate {
				Log.Info(tag, "btnQuickAdd.Click");

				Intent intent = new Intent(this,typeof(BorrowpitListActivity));
				StartActivity(typeof(PhotoDetailActivity));
			};
		}

		public int CheckPendingUploads ()
		{
			Log.Info (tag, "Check Pending Uploads");

			var db = dbBorrowPit.ConnectToDB ();
			if (db == null) {
				string toastMsg = string.Format ("No Database connected on {0}", App._dbFileName);
				Toast.MakeText (this, toastMsg, ToastLength.Long);
			}
			List<MIMS_UPLOADED_PHOTOS> lst = (from i in db.Table<MIMS_UPLOADED_PHOTOS> () select i).ToList();
			return lst.Count;
		}


		public  void CopyInitialDBtoFinal(string DatabaseFileNAme, string DatabaseFinalDirectory)
		{
			string dbName = DatabaseFileNAme;
			string dbPath = System.IO.Path.Combine (DatabaseFinalDirectory, dbName);
			// Check if your DB has already been extracted.
			if (!System.IO.File.Exists(dbPath))
			{
	
				string cpath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				string src = System.IO.Path.Combine (cpath, dbName);
				string src1 = System.IO.Path.Combine (cpath, "Assets", dbName);
				string src2 = System.IO.Path.Combine (cpath, "Resources", dbName);
				string src3 = System.IO.Path.Combine (dbName);
				string src4 = System.IO.Path.Combine (cpath, dbName);
				//(Stream)Assets.Open (dbName);

				//var appdir = ApplicationContext.GetDir ("DATA");

				using (BinaryReader br = new BinaryReader(Application.ApplicationContext.Resources.Assets.Open (dbName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write (buffer, 0, len);
						}
					}
				};
			}
		}

	}
}


