﻿
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
	[Activity (Label = "Borrowpit List")]			
	public class BorrowpitListActivity : ListActivity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			ListAdapter = new BorrowpitAdapter (this);
		}
	}
}

