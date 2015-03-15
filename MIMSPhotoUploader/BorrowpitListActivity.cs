
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
	[Activity (Label = "Borrowpit List")]			
	public class BorrowpitListActivity : ListActivity
	{

		string tag = "Borrowpit List Activity";


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Log.Info (tag, "On Create()");
			ListAdapter = new BorrowpitAdapter (this);
		}
	}
}

