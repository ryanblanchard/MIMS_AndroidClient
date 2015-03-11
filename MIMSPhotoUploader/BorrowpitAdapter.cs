using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using MIMSPhotoUploader.Data;
using SQLite;
using Android.Views;

namespace MIMSPhotoUploader
{
	public class BorrowpitAdapter : BaseAdapter
	{

		List<BorrowPit> _borrowpitList;
		Activity _activity;


		public BorrowpitAdapter (Activity activity)
		{
			_activity = activity;
			FillBorrowPits ();
		}



		public void FillBorrowPits()
		{
			var conn = dbBorrowPit.ConnectToDB();
			var table = conn.Table<Data.MIMS_MATERIAL_SRC> ();

			_borrowpitList = new List<BorrowPit> ();

			foreach (var t in table) {
				BorrowPit bp = new BorrowPit ();
				bp.Id = t.ID;
				bp.BorrowpitName = t.MATERIAL_SRC_NO;
				bp.RoadNo = t.ROAD_NO;

				_borrowpitList.Add (bp);
			}
		}

		public override int Count {
			get { return _borrowpitList.Count; }
		}

		public override Java.Lang.Object GetItem (int position) {
			// could wrap a Contact in a Java.Lang.Object
			// to return it here if needed
			return null;
		}

		public override long GetItemId (int position) {
			return _borrowpitList [position].Id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.BorrowPitListItem, parent, false);

			var BorrowPitName = view.FindViewById<TextView> (Resource.Id.BorrowPitName);
			var RoadNo = view.FindViewById<TextView> (Resource.Id.BorrowPitNo);
			var ID = view.FindViewById<TextView> (Resource.Id.BorrowPitId);

			BorrowPitName.Text = _borrowpitList [position].BorrowpitName;
			RoadNo.Text  =_borrowpitList [position].RoadNo;
			ID.Text = _borrowpitList [position].Id.ToString();

			return view;
		}
	}
		
}

