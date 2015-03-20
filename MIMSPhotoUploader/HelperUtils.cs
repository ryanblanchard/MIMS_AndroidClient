using System;
using Android.Graphics;
using System.IO;
using Android.Util;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Widget;

namespace MIMSPhotoUploader
{
	public static class HelperUtils
	{


		public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			// Raw height and width of image
			float height = options.OutHeight;
			float width = options.OutWidth;
			double inSampleSize = 1D;

			if (height > reqHeight || width > reqWidth)
			{
				int halfHeight = (int)(height / 2);
				int halfWidth = (int)(width / 2);

				// Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
				while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
				{
					inSampleSize *= 2;
				}
			}

			return (int)inSampleSize;
		}


		/// <summary>
		/// Loads the and resize bitmap.
		/// </summary>
		/// <returns>The and resize bitmap.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
		{
			// First we get the the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);


			// Next we calculate the ratio that we need to resize the image by
			// in order to fit the requested dimensions.
			int MaxWidth = 200;
			int MaxHeight = 200;
			int inSampleSize = CalculateInSampleSize (options, MaxWidth, MaxHeight);


			
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			//int inSampleSize = 1;
			
			
			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight
					? outHeight / height
					: outWidth / width;
			}

			Log.Info("inSampleCalc", string.Format ("inSampleSize : {0}", inSampleSize));


			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

			return resizedBitmap;
		}

	
		/// <summary>
		/// Resizes the photo.
		/// </summary>
		/// <returns>The photo.</returns>
		/// <param name="filePath">File path.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
	public static Bitmap ResizePhoto(string filePath, int width, int height)
	{

		//reducing the size of the images
		BitmapFactory.Options options=new BitmapFactory.Options();// Create object of bitmapfactory's option method for further option use
		options.InPurgeable = true; // inPurgeable is used to free up memory while required
			options.OutWidth = width;
			options.OutHeight = height;

			Bitmap songImage1 = BitmapFactory.DecodeFile (filePath, options);

		Bitmap songImage = Bitmap.CreateScaledBitmap(songImage1, width , height , true);// convert decoded bitmap into well scalled Bitmap format.
			songImage1.Recycle();
		return songImage;
	}

	}
}


