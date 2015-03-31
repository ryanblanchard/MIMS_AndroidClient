using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using Android.Util;
using Environment = Android.OS.Environment;
using Android.Content.PM;
using Android.Provider;
using System.Collections.Generic;
using Java.IO;
using Android.Graphics;
using Java.Net;
using Android.Media;
using MIMSPhotoUploader;
using System.IO;

namespace MIMSPhotoUploader
{
	[Activity (Label = "AddPhotoActivity", ScreenOrientation=ScreenOrientation.Portrait)]			
	public class AddPhotoActivity : Activity
	{

		string tag = "AddPhotoActivity";

		ImageView imageView;
		TextView borrowpitText;
		TextView textCategory;
		TextView textPhotoUrl;
		Spinner spinCategory;
		List<string> list;

		string PhotoCategoryDesc;

		/*
		int borrowpitId;
		string borrowpitName;
		string userName;
		*/

		string userName;
		string roadNo;
		string borrowpitID; 
		string borrowpitName;
		string photoId;

		Button btnSave;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);
			SetContentView (Resource.Layout.AddPhotoLayout);

			TextView headBorrowpitName = FindViewById<TextView> (Resource.Id.headBorrowpitName);
			headBorrowpitName.Text = App._borrowpitName;

			CreateDirectoryForPictures();

			Log.Info (tag, string.Format ("Borrowpit Name = {0}", borrowpitName));

			//TODO: Find the borrow pit description for display
			textCategory = FindViewById<TextView> (Resource.Id.textCategoryName);

			spinCategory = FindViewById<Spinner> (Resource.Id.spinCategory);
			spinCategory.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinCategory_ItemSelected);

			ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>
				(this, Android.Resource.Layout.SimpleSpinnerItem);

			dataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
			 
			spinCategory.Adapter = dataAdapter;


			//Load categories	
			int i = 1;
			dataAdapter.Insert ("<Select Category>", 0);
			var db = dbBorrowPit.ConnectToDB ();
			var qry = db.Table<MIMS_UPL_PHOTO_CATEGORIES> ();


			foreach (var cat in qry) {
				Log.Info ("Adding Category", cat.PHOTO_CATEGORY_DESCR);
				dataAdapter.Insert (cat.PHOTO_CATEGORY_DESCR, i);
				i++;
			}

			//TODO: populate with description categories

			textPhotoUrl = FindViewById<TextView> (Resource.Id.textPhotoUri);

			if (IsThereAnAppToTakePictures ()) 
			{
				Button btnTakePhoto = FindViewById<Button> (Resource.Id.btnTakePhoto);
				imageView = FindViewById<ImageView> (Resource.Id.imageView1);
				if (App.bitmap != null) 
				{
					imageView.SetImageBitmap (App.bitmap);
					App.bitmap = null;
				}
				btnTakePhoto.Click += TakeAPicture;

				btnSave = FindViewById<Button> (Resource.Id.btnSave);
				btnSave.Click += delegate {
					//click event here

					textPhotoUrl.Text = (App._file).AbsolutePath;

					if (imageView != null) {
						try {
								
							tag = "btnSave";
							var DB = dbBorrowPit.ConnectToDB();
							Log.Info(tag,"DB Connection : {0}", DB.DatabasePath);

							//var DB = new SQLiteConnection (App._dbFileName);

							int bpID =  int.Parse(App._borrowPitID);
							int geo_x = 0; //App._lat == null?0:App._lat;
							int geo_y = 0;  //App._long == null?0:App._Long;
							int catID = int.Parse(App._photoCategoryID);

							if (catID != 0)
							{

								var im = new MIMS_UPLOADED_PHOTOS ();
								im.BORROW_PIT_ID = bpID; //TODO: pass the real borrowpit id
								im.USERNAME = App._username; 
								im.TRANSACTION_DATE = DateTime.Now;
								im.PHOTO_FILENAME = App._file == null ? "No_Image" : App._file.Path;
								im.UPLOADED = false;
								im.DEGREES_DECIMAL_X = geo_x; // int.Parse(App._lat);
								im.DEGREES_DECIMAL_Y = geo_y; // int.Parse(App._long);
								im.CATEGORY_ID = catID; //int.Parse(App._photoCategoryID);
								im.CATEGORY_DESC = App._photoCategory;

								var s = DB.Insert (im);

								//textMessage.Text = String.Format("New Image ID: {0} - {1}", img.Id, img.ImageDesc);
								Log.Info (tag, String.Format ("New Image ID: {0} - {1}", im.ID, im.PHOTO_FILENAME));

								//clear form
								btnTakePhoto.Text = "Saved! Take another?";
								spinCategory.SetSelection(0);
								imageView.SetImageBitmap(null);
								textPhotoUrl.Text = "";
							}
							else
							{
								Toast.MakeText(this, "Please Select a category for this photo.", ToastLength.Short);
							}
						}
						catch (Exception ex)
						{
							Log.Error(tag,ex.Message);
							Android.Widget.Toast.MakeText(this, "Error Inserting Image to Database", Android.Widget.ToastLength.Short).Show();
						}
							
					} else {
						Log.Info (tag, String.Format ("No Image loaded"));
						//textMessage.Text = String.Format("No Photo to save.");		
						//Log.Info (tag, String.Format ("New Image ID: {0} - {1}", img.Id, img.ImageDesc));
					}

				};
			}
		}

		private void spinCategory_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{

			Spinner spinner = (Spinner)sender;
			if (spinner != null) {

				App._photoCategory = spinner.GetItemAtPosition (e.Position).ToString();
				App._photoCategoryID = spinner.GetItemIdAtPosition (e.Position).ToString();

				//string toast = string.Format ("The planet is {0}", spinner.GetItemAtPosition (e.Position));

				if (spinner.SelectedItem == null) {
					spinner.SetSelection (0);
				} else {
					PhotoCategoryDesc = spinner.GetItemAtPosition (e.Position).ToString();  //GetItemAtPosition (e.Position);
				}
				//Toast.MakeText (this, toast, ToastLength.Long).Show ();
			}
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "MIMSBorrowpitPhotos");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
				Log.Info(tag,"Image Directory:{0}", App._dir);
			}
			Log.Info(tag,"Image Directory:{0}", App._dir);
		}

		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Log.Info (tag, "TakeAPicture");

			Intent intent = new Intent(MediaStore.ActionImageCapture);

			App._file = new Java.IO.File(App._dir, String.Format("mimsPhoto_{0}.jpg", Guid.NewGuid()));

			Log.Info (tag, "Photo File Name - Absolute Path {0}", App._file.AbsolutePath);
			Log.Info (tag, "Photo File Name - Path {0}", App._file.Path);

			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));

			StartActivityForResult(intent, 0);

		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			Log.Info (tag, "On Activity Result");

			// make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);

			Log.Info (tag, contentUri.EncodedPath);

			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);


			/* For saving to disk */


			var fileName = App._file.AbsolutePath;
			int SaveSizeH = 700;
			int SaveSizeW = 700;

			Bitmap imageSave = HelperUtils.LoadAndResizeBitmap (App._file.Path, SaveSizeW, SaveSizeH);

			if (App._file.Exists()) {
				Log.Info("DeleteFile", String.Format("Image found at {0} will be removed", App._file.AbsolutePath));

				App._file.Delete ();
			}
			using (var os = new FileStream(fileName, FileMode.CreateNew))
			{
				imageSave.Compress(Bitmap.CompressFormat.Jpeg, 70, os);
				os.Flush ();
				os.Close ();
				imageSave.Recycle ();
			}


			/******/

			// display in ImageView. We will resize the bitmap to fit the display
			// Loading the full sized image will consume to much memory 
			// and cause the application to crash.
			int height = Resources.DisplayMetrics.HeightPixels;
			int width = imageView.Width ;

			//Bitmap imageView = HelperUtils.LoadAndResizeBitmap (App._file.Path, width, height);

			App.bitmap = HelperUtils.LoadAndResizeBitmap (App._file.Path, width, height);

			imageView = FindViewById<ImageView>(Resource.Id.imageView1);
			if (App.bitmap != null) {
				imageView.SetImageBitmap (App.bitmap);
				textPhotoUrl.Text = App._file.Path;
				App.bitmap = null;
			}
		}


		private List<String> GetPhotoCategories ()
		{
			List<String> list;
			list = new List<String> ();

			var db = dbBorrowPit.ConnectToDB ();
			var query = db.Table<MIMS_UPL_PHOTO_CATEGORIES> ();

				foreach (var it in db.Table<MIMS_UPL_PHOTO_CATEGORIES> ()) {
				Log.Info (tag,it.PHOTO_CATEGORY_DESCR);
					string PhotoCategory = it.PHOTO_CATEGORY_DESCR;
				list.Add (it.PHOTO_CATEGORY_DESCR);
			}

			return list;
		}




	}




}


