
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
	[Activity (Label = "PhotoDetailActivity")]			
	public class PhotoDetailActivity : Activity
	{

		string userName;
		string roadNo;
		string borrowpitID;
		string borrowpitName;

		string tag;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//SetContentView (Resource.Layout.ImageListLayout);
			if ( dbBorrowPit.IsNull(Intent.GetStringExtra ("BorrowpitName"), "") != "" )
			{
				//string userName = Intent.GetStringExtra ("UserName") ?? "No User Details";

				userName =  Intent.GetStringExtra ("UserName")?? "No User Details";
				roadNo =  Intent.GetStringExtra ("RoadNo")?? "No Road Details";
				borrowpitID = Intent.GetStringExtra ("BorrowpitId") ?? "No Borrow Pit ID";;
				borrowpitName =  Intent.GetStringExtra ("BorrowpitName") ?? "No BorrowPit Name";
				//photoId = Intent.GetStringExtra ("PhotoID") ?? "NO PHOTO REQUESTED";
			}


			tag = "PhotoDetailActivity";

			List<string> from;

			var db = dbBorrowPit.ConnectToDB ();
			var tab = db.Table<MIMS_UPL_PHOTO_CATEGORIES> ();

			from = new List<string> ();
			foreach (var i in tab) {
				//from.Add (i.PHOTO_CATEGORY_DESC.ToString());
				Log.Info (tag, "Desc = " + i.PHOTO_CATEGORY_DESCR);
			}

	
			Spinner spinnerFrom = FindViewById<Spinner> (Resource.Id.spinner1);

			ArrayAdapter _adapterFrom = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, from);
			_adapterFrom.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			//spinnerFrom.Adapter = _adapterFrom; 

			spinnerFrom.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				string toast = string.Format ("The planet is {0}",e.Position);
				//Toast.MakeText (this, toast, ToastLength.Long).Show (); 
			};




			// Create your application here
		}
	}
}

