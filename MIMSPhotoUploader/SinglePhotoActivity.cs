
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

namespace MIMSPhotoUploader
{
	[Activity (Label = "SinglePhotoActivity")]			
	public class SinglePhotoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.layoutSinglePhoto);

			TextView txtBorrowpit = FindViewById<TextView> (Resource.Id.textBorrowpitName);
			TextView txtCategory = FindViewById<TextView> (Resource.Id.textCategory);
			ImageView imgPhoto = FindViewById<ImageView> (Resource.Id.imageView1);


			txtBorrowpit.Text = App._borrowpitName;
			txtCategory.Text = App._photoCategory;



			// Create your application here
		}
	}
}

