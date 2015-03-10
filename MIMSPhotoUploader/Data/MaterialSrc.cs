using System;
using SQLite;

namespace MIMSPhotoUploader
{
	public class MaterialSrc
	{
		[PrimaryKey]
		public int Id { get; set;}
		public string MaterialSrcNo{ get; set;}
		public string BorrowPitNo{ get; set;}
		public string RoadNo{ get; set;}
		public double KilometerPosition{ get; set;}
		public string OffsetLeftRight{ get; set;}
		public double OffsetInMeters{ get; set;}
		public double DegreesDecimalX { get; set;}
		public double DegreesDecimalY { get; set;}

		public MaterialSrc ()
		{
		}

	}
}

