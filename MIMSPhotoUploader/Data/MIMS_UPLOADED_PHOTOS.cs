using System;
using SQLite;

namespace MIMSPhotoUploader
{
	[Table("MIMS_UPLOADED_PHOTOS")]
	public class MIMS_UPLOADED_PHOTOS
	{	
		[PrimaryKey, AutoIncrement]
		public int ID { get; set;}
		public int BORROW_PIT_ID { get; set;}
		public string USERNAME { get; set;}
		public DateTime TRANSACTION_DATE { get; set;}
		public byte[] PHOTO_DATA { get; set;}
		public string PHOTO_FILENAME { get; set;}
		public bool UPLOADED { get; set;}
		public int DEGREES_DECIMAL_X { get; set;}
		public int DEGREES_DECIMAL_Y { get; set;}
		public int CATEGORY_ID { get; set;}
		public string CATEGORY_DESC { get; set;}

	}
}

