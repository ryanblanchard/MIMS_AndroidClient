
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
using Java.IO;

namespace MIMSPhotoUploader
{
	[Activity (Label = "SinglePhotoActivity")]			
	public class SinglePhotoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.layoutSinglePhoto);

			TextView txtBorrowpit = FindViewById<TextView> (Resource.Id.textBorrowpit);
			TextView txtCategory = FindViewById<TextView> (Resource.Id.textCategory);
			ImageView imgPhoto = FindViewById<ImageView> (Resource.Id.imagePhoto);


			txtBorrowpit.Text = App._borrowpitName;
			txtCategory.Text = App._photoCategory;

			File pic = new File (App._photoFileName);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(pic);
			imgPhoto.SetImageURI (contentUri);

		}
	}
}

