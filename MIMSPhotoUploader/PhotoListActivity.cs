
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

		List<MIMS_UPLOADED_PHOTOS> tabItems;
		Activity context;

		int _borrowpitID = 27;
		string tag = "PhotoListActivity";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//SetContentView (Resource.Layout.ImageListLayout);

			SetContentView(Resource.Layout.layoutImageList); // loads the HomeScreen.axml as this activity's view
			listView = FindViewById<ListView>(Resource.Id.List); // get reference to the ListView in the layout
			// populate the listview with data
			int Counter = 0;

			tabItems = new List<MIMS_UPLOADED_PHOTOS> ();

			var db = dbBorrowPit.ConnectToDB ();
			//var tab = db.Table<MIMS_UPLOADED_PHOTOS> ();

			var iQuery = db.Table<MIMS_UPLOADED_PHOTOS> ().Where (v => v.BORROW_PIT_ID == _borrowpitID);
			foreach (var pg in iQuery) {
				MIMS_UPLOADED_PHOTOS p = new MIMS_UPLOADED_PHOTOS ();
				p.ID = pg.ID;
				p.BORROW_PIT_ID = _borrowpitID;
				p.CATEGORY_DESC = pg.CATEGORY_DESC;
				p.CATEGORY_ID = pg.CATEGORY_ID;
				p.PHOTO_FILENAME = pg.PHOTO_FILENAME;

				tabItems.Add (p);
				Counter++;
			}
			//tabItems = itemsQuery.ToList();

			listView.Adapter = new ImageListAdapter(this, tabItems);
			listView.ItemClick += OnListItemClick; 


			Button btnPhoto = FindViewById<Button> (Resource.Id.btnAddPhoto);
			btnPhoto.Click += delegate {
			
				Log.Info (tag, "btnPhoto.Click");
				Intent intent = new Intent (this, typeof(AddPhotoActivity));
				intent.PutExtra ("BorrowpitID", _borrowpitID);
				StartActivity (intent);

			};

		}



		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Log.Info(tag, "button.Click");

			var listView = sender as ListView;
			var t = tabItems[e.Position];


			//Android.Widget.Toast.MakeText(this, t.Heading, Android.Widget.ToastLength.Short).Show();

			//Log.Info(tag,  
			
		}
	}
}

