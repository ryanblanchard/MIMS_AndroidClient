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
	public class BorrowpitListActivity : Activity
	{
		List<BorrowPit> data;
		List<string> roads;
		string tag = "BorrowpitListActivbity";
		BorrowpitAdapter mBorrowPitAdapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);
			Log.Info (tag, "On Create()");

			SetContentView (Resource.Layout.layoutBorrowpitSelect);
			//EventLogTags = "Borrowpit List Activity";

			data = FillBorrowPits ();
			ListView lv = FindViewById<ListView> (Resource.Id.List);
			lv.ItemClick += delegate (object IntentSender, Android.Widget.AdapterView.ItemClickEventArgs e)
			{
				Log.Info (tag, "OnListItemClick Position {0}, ID {1}",  e.Position, e.Id);

				App._borrowPitID = data [e.Position].Id.ToString ();
				App._borrowpitName = data [e.Position].BorrowpitName;
				App._roadNo = data [e.Position].RoadNo;

				Intent intent = new Intent (this, typeof(PhotoListActivity));
				StartActivity(intent);
			};

			roads = FillRoadList ();
			var RoadAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, roads);
			AutoCompleteTextView autoText = FindViewById<AutoCompleteTextView> (Resource.Id.autoCompleteTextView1);
			autoText.Adapter = RoadAdapter;

			mBorrowPitAdapter = new BorrowpitAdapter (this,data);
			lv.Adapter = mBorrowPitAdapter;

			Button btnFilter = FindViewById<Button> (Resource.Id.btnFilter);
			btnFilter.Click += delegate(object sender, EventArgs e) {

				data = FilterBorrowPits (autoText.Text);
				mBorrowPitAdapter = new BorrowpitAdapter (this,data);
				lv.Adapter = mBorrowPitAdapter;

			};


		}

		public List<string> FillRoadList()
		{
			var conn = dbBorrowPit.ConnectToDB ();
			var table = conn.Table<VW_ROAD_LIST> ();
			List<string> road_List = new List<string> ();

			foreach (var r in table) {
				road_List.Add (r.ROAD_NO);
			
			}

			return road_List;

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

			public List<BorrowPit> FilterBorrowPits(string road_no)
			{
				var conn = dbBorrowPit.ConnectToDB();
				var table = conn.Table<MIMS_MATERIAL_SRC> ().Where (v => v.ROAD_NO.StartsWith(road_no));

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
		
		private void ListView_ItemClick (Object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
		{
			//base.OnListItemClick (l, v, position, id);
			Log.Info (tag, "OnlistItemClick");

			//int itemID = this.mBorrowPitAdapter.GetItemId

			var item = this.mBorrowPitAdapter.GetItem (e.Position);
/*
			var bp_ID = data [position].Id;
	
			Log.Info (tag, "OnListItemClick Position {0}, ID {1}",  position, bp_ID);

			App._borrowPitID = data [position].Id.ToString ();
			App._borrowpitName = data [position].BorrowpitName;
			App._roadNo = data [position].RoadNo;

			Intent intent = new Intent (this, typeof(PhotoListActivity));
			intent.PutExtra ("BorrowpitID", bp_ID);
			intent.PutExtra ("BorrowpitName", data [position].BorrowpitName);

			StartActivity(intent);

			//Toast.MakeText (this, data [position], ToastLength.Short).Show ();
*/		}
		

	}
}

