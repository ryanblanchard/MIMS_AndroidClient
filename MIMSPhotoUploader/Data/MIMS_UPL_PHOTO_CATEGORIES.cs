using System;
using SQLite;

namespace MIMSPhotoUploader
{
	[Table("MIMS_UPL_PHOTO_CATEGORIES")]
	public class MIMS_UPL_PHOTO_CATEGORIES
	{	
		[PrimaryKey]
		public int ID  { get; set;}
		public string PHOTO_CATEGORY_DESC  { get; set;}
		public string CREATED_BY { get; set;}
		public DateTime CREATED_DATE { get; set;}
		public string MODIFIED_BY { get; set;}
		public DateTime MODIFIED_DATE { get; set;}
		public int SORT_ORDER { get; set;}

	}
}

