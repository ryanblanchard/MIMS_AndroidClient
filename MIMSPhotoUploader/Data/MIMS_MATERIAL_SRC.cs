using System;
using SQLite;

namespace MIMSPhotoUploader.Data
{
	[Table("Items")]
	public class MIMS_MATERIAL_SRC
	{
		[PrimaryKey]
		public int Id { get; set;}
		public string MATERIAL_SRC_NO{ get; set;}
		public string BORROWPIT_NO{ get; set;}
		public string ROAD_NO{ get; set;}
		public double KILOMETER_POSITION{ get; set;}
		public string OFFSET_LEFT_RIGHT{ get; set;}
		public double OFFSET_IN_METERS{ get; set;}
		public double DEGREES_DECIMAL_X { get; set;}
		public double DEGREES_DECIMAL_Y { get; set;}
	}
}

