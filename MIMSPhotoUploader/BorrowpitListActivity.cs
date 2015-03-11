
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
	[Activity (Label = "BorrowpitListActivity")]			
	public class BorrowpitListActivity : ListActivity 
	{

		string[] items;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			items = GetBorrowPitItems ();

			ArrayAdapter adapter = new ArrayAdapter (this, Resource.Layout.BorrowPitListItem, items);
			ListAdapter = adapter;

		}

		private string[] GetBorrowPitItems()
		{

			return new string[]{ "DR0100", "DR9763", "DR3456", "DR4567", "DR4443", "DR4432" };

			//Arra
		}

	}
}

