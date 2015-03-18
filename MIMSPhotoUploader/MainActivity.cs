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
using System.Collections.Generic;
using System.Linq;

namespace MIMSPhotoUploader
{


	[Activity (Label = "MIMSPhotoUploader", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		string tag = "MainActivity";


		string userName;
		string borrowpitID;
		string borrowpitName;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			if (bundle == null) {
				//App app = new App ();
			}

			userName = "Ryan";//TODO: BUild Login Screen


			//copies the inital SQLite db out of Assets into the final folder 
			var db = dbBorrowPit.ConnectToDB ();
			if (db == null) {
				Java.IO.File DB_file = new Java.IO.File (Android.OS.Environment.GetExternalStoragePublicDirectory (Android.OS.Environment.DirectoryDocuments), "MIMSBorrowpitPhotos_DATA");
				CopyInitialDBtoFinal (dbBorrowPit.dbFileName, DB_file.Path);
			}

			TextView textMainTitle = FindViewById<TextView> (Resource.Id.textMainTitle);
			TextView textGPSNote =  FindViewById<TextView> (Resource.Id.textGPSNote);

			TextView labelGPSPos = FindViewById<TextView> (Resource.Id.labelGPSPos);
			TextView textGPSLat = FindViewById<TextView> (Resource.Id.textGPSLat);
			TextView textGPSLong = FindViewById<TextView> (Resource.Id.textGPSLong);



			TextView syncStatus = FindViewById<TextView> (Resource.Id.textSyncStatus);

			if (CheckPendingUploads () > 0) {
				syncStatus.Text = GetText (Resource.String.syncStatus_PhotosToSync);
			} else {
				syncStatus.Text = GetText (Resource.String.syncStatus_NothingToSync);
			}



			EditText currDate = FindViewById<EditText> (Resource.Id.editCurentDate);
			currDate.Text = string.Format("{0:yyyy/MM/dd}", DateTime.Today);


			//currDate.EditText = DateTime.Now;


			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Click += delegate {
				Log.Info(tag, "button.Click");
				//StartActivity(typeof(MIMSPhotoUploader.BorrowpitListActivity));

				StartActivity(typeof(PhotoDetailActivity));
			};

			Button btnPhotos = FindViewById<Button> (Resource.Id.myPhotos);
			btnPhotos.Click += delegate {
				Log.Info(tag, "btnPhotos.Click");
				StartActivity(typeof(PhotoListActivity));
			};

			Button btnQuickAdd = FindViewById<Button> (Resource.Id.btnQuickAdd);
			btnQuickAdd.Click += delegate {
				Log.Info(tag, "btnQuickAdd.Click");
				//StartActivity(typeof(AddPhotoActivity));
				StartActivity(typeof(PhotoDetailActivity));
			};


			//CheckPendingUploads ();

		}

		public int CheckPendingUploads ()
		{
			var db = dbBorrowPit.ConnectToDB ();
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


