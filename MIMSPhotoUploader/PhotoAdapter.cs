using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using SQLite;
using Android.Content.Res;
using Android.OS;


namespace MIMSPhotoUploader
{

	public class PhotoAdapter : BaseAdapter
	{

		LayoutInflater _inflator;

		private String[] _categories;
		private String[] _filenames;
		private TypedArray _images;
		private int[] _catId;

		private int viewResourceId;
	
		int _borrowPitID;
		Activity _activity;
		List<Photo> _photoList;


		public PhotoAdapter (Activity activity)
		{
			_activity = activity;
			//_borrowPitID = BorrowpitID;
			FillPhotoList ();

			//ArrayAdapter adapter = new ArrayAdapter (this, Resource.Layout.ImageListLayout, data);        
			//ListAdapter = adapter;
		}

/*
 * protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		}
 */



		public void FillPhotoList()
		{
			_photoList = new List<Photo> ();
			int sub = 0;
			var conn = dbBorrowPit.ConnectToDB();

			var table = conn.Table<MIMS_UPLOADED_PHOTOS> ();

			foreach (var p in table) {

				Photo ph = new Photo ();
				ph.id = p.ID;
				ph.category = p.CATEGORY_DESC;
				ph.categoryId = p.CATEGORY_ID;
				ph.uploaded = p.UPLOADED;
				ph.fileName = p.PHOTO_FILENAME;
				ph.image = p.PHOTO_DATA;
				_photoList.Add (ph);

				sub++;
				_categories [sub] = ph.category;
				//_images [sub] = ph.image;
				_filenames [sub] = ph.fileName;

			}

		}

		public override int Count {
			get { return _photoList.Count; }
		}

		public override Java.Lang.Object GetItem (int position) 
		{
			//Uri u = new Uri ();

			return _photoList [position].fileName;
			//return u;
			 //return _photoList [position].fileName;
		}

		public override long GetItemId (int position) {
			return _photoList [position].id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.ImageListItem, parent, false);

			var imageView = view.FindViewById<ImageView> (Resource.Id.imageView1);
			var category = view.FindViewById<TextView> (Resource.Id.textCategory);
			var filename = view.FindViewById<TextView> (Resource.Id.textFilename);
			var CheckBox = view.FindViewById<CheckBox> (Resource.Id.checkUploaded);

			//Android.Net.Uri contentUri = Android.Net.Uri.FromFile( GetItem(position));


			//imageView.SetImageBitmap(_images [position]);
			category.Text = _categories [position];
			filename.Text = _filenames [position];
			CheckBox.Checked = false;


			/*
			 * 
			var BorrowPitName = view.FindViewById<TextView> (Resource.Id.BorrowPitName);
			var ID = view.FindViewById<TextView> (Resource.Id.BorrowPitId);
			var uploadChk = view.FindViewById<CheckBox> (Resource.Id.checkUploaded);
			*/

			//BorrowPitName.Text = _borrowpitList [position].BorrowpitName;
			//RoadNo.Text  =_borrowpitList [position].RoadNo;
			//ID.Text = _borrowpitList [position].Id.ToString();

			return view;
		}


		/*

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
		}		 * 
		 * */


	}
}

