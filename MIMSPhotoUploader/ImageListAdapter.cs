using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Android.Graphics;

namespace MIMSPhotoUploader
{
	public class ImageListAdapter : BaseAdapter<MIMS_UPLOADED_PHOTOS> {

		List<MIMS_UPLOADED_PHOTOS> items;
		Activity context;

		public ImageListAdapter(Activity context, List<MIMS_UPLOADED_PHOTOS> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override MIMS_UPLOADED_PHOTOS this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.layoutCustomView, null);
			view.FindViewById<TextView>(Resource.Id.Text1).Text = item.CATEGORY_DESC;
			view.FindViewById<TextView>(Resource.Id.Text2).Text = item.ID.ToString();

			view.FindViewById<ImageView> (Resource.Id.Image).SetImageBitmap (HelperUtils.ResizePhoto(item.PHOTO_FILENAME,50,50));


			return view;

		}
	}
}

