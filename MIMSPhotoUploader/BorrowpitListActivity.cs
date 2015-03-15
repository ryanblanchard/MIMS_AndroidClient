
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
	[Activity (Label = "Select Borrowpit", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class BorrowpitListActivity : ListActivity
	{
		string tag = "Borrowpit List Activity";
		List<BorrowPit> data;

		//string[] items;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Log.Info (tag, "On Create()");

			data = FillBorrowPits ();

			ListAdapter = new BorrowpitAdapter (this,data);
		}

		public List<BorrowPit> FillBorrowPits()
		{
			var conn = dbBorrowPit.ConnectToDB();
			var table = conn.Table<MIMS_MATERIAL_SRC> ();

			List<BorrowPit> _borrowpitList = new List<BorrowPit> ();

			foreach (var t in table) {
				BorrowPit bp = new BorrowPit ();
				bp.Id = t.ID;
				bp.BorrowpitName = t.MATERIAL_SRC_NO;
				bp.RoadNo = t.ROAD_NO;

				_borrowpitList.Add (bp);
			}

			return _borrowpitList;
		}


		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			Log.Info (tag, "OnlistItemClick");

			var bp_ID = data [position].Id;

			Intent intent = new Intent (this, typeof(PhotoListActivity));
			intent.PutExtra ("BorrowpitID", bp_ID.ToString ());
			intent.PutExtra ("BorrowpitName", data [position].BorrowpitName);

			StartActivity(intent);

			//Toast.MakeText (this, data [position], ToastLength.Short).Show ();
		}

	}
}

