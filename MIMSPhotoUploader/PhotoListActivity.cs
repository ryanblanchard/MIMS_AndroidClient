
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
	[Activity (Label = "PhotoListActivity")]			
	public class PhotoListActivity : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//SetContentView (Resource.Layout.ImageListLayout);

			int borrowpitId = 27; //TODO: GET THE BORROWPIT ID FROM INTENT
			ListAdapter = new PhotoAdapter (this);

			// Create your application here
		}
	}
}

