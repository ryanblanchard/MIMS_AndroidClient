
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace MIMSPhotoUploader
{
	[Activity (Label = "PhotoListActivity", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class PhotoListActivity : Activity
	{
		ListView listView;


		string userName;
		string roadNo;
		string borrowpitID; 
		string borrowpitName; 
		string photoId;



		List<MIMS_UPLOADED_PHOTOS> tabItems;
		//Activity context;

		string tag;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//context = Application.ApplicationContext.getApplicationContext ();
			tag = "PhotoListActivity";

			//SetContentView (Resource.Layout.ImageListLayout);
			if ( dbBorrowPit.IsNull(Intent.GetStringExtra ("BorrowpitName"), "") != "" )
			{
				/*
				//string userName = Intent.GetStringExtra ("UserName") ?? "No User Details";
				userName =  Intent.GetStringExtra ("UserName")?? "No User Details";
				roadNo =  Intent.GetStringExtra ("RoadNo")?? "No Road Details";
				borrowpitID = Intent.GetStringExtra ("BorrowpitId") ?? "No Borrow Pit ID";;
				borrowpitName =  Intent.GetStringExtra ("BorrowpitName") ?? "No BorrowPit Name";
				photoId = Intent.GetStringExtra ("PhotoID") ?? "NO PHOTO REQUESTED";
				*/
			}



			SetContentView(Resource.Layout.layoutImageList); // loads the HomeScreen.axml as this activity's view

			TextView textBorrowpitName = FindViewById<TextView> (Resource.Id.Borrowpit);
			textBorrowpitName.Text = App._borrowpitName;

			listView = FindViewById<ListView>(Resource.Id.List); // get reference to the ListView in the layout
			// populate the listview with data
			int Counter = 0;

			tabItems = new List<MIMS_UPLOADED_PHOTOS> ();

			var db = dbBorrowPit.ConnectToDB ();
			var tab = db.Table<MIMS_UPLOADED_PHOTOS> ();

			int bpID = int.Parse (App._borrowPitID);

			var iQuery = db.Table<MIMS_UPLOADED_PHOTOS> ().Where (v => v.BORROW_PIT_ID == bpID);
			foreach (var pg in iQuery) {
				MIMS_UPLOADED_PHOTOS p = new MIMS_UPLOADED_PHOTOS ();
				p.ID = pg.ID;
				p.BORROW_PIT_ID = pg.BORROW_PIT_ID;
				p.CATEGORY_DESC = pg.CATEGORY_DESC;
				p.CATEGORY_ID = pg.CATEGORY_ID;
				p.PHOTO_FILENAME = pg.PHOTO_FILENAME;

				tabItems.Add (p);
				Counter++;
			}
			//tabItems = itemsQuery.ToList();

			listView.Adapter = new ImageListAdapter(this, tabItems);
			listView.ItemClick += OnListItemClick; 

			//****************/

			Button btnPhoto = FindViewById<Button> (Resource.Id.btnAddPhoto);
			btnPhoto.Click += delegate {

				Log.Info (tag, "btnPhoto.Click");

				App._photoID = "0";
				App._photoCategory = "No Photo Selected";
				App._photoCategoryID = "0";

				Intent i = new Intent(this, typeof(AddPhotoActivity));
/*				i.PutExtra("UserName",App._username);
				i.PutExtra("RoadNo",App._roadNo);
				i.PutExtra("BorrowpitId",borrowpitID);
				i.PutExtra("BorrowpitName",App._borrowpitName);
				i.PutExtra("PhotoID","0");
*/
				StartActivity(i);

			};

		}



		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Log.Info(tag, "OnListItemClick");

			var lv = sender as ListView;
			var t = tabItems [e.Position].ID;

			App._photoID = tabItems [e.Position].ID.ToString ();
			App._photoFileName = tabItems [e.Position].PHOTO_FILENAME;
			App._photoCategory = tabItems [e.Position].CATEGORY_DESC;
			App._photoCategoryID = tabItems [e.Position].CATEGORY_ID.ToString();

			Intent intent = new Intent (Intent.ActionView);

			Java.IO.File f = new Java.IO.File (tabItems [e.Position].PHOTO_FILENAME);
			intent.SetData(Android.Net.Uri.FromFile (f));

			StartActivity (intent);
			Log.Info ("Actionview", f.AbsolutePath);

		}
	}
}

