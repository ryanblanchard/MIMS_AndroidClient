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

		/*
		public static void ShowToastMessage(Android.Content.Context context, string Message)
		{
			//context = getApplicationContext();
			CharSequence text = "Hello toast!";
			int duration = Toast.LENGTH_SHORT;

			Toast toast = Toast.MakeText(context, text, duration);
			toast.Show();
		}
		*/

		/*
		/// <summary>
		/// Checks the db exists.
		/// </summary>
		/// <returns>The db exists.</returns>
		public static string CheckDbExists()
		{
			File DB_file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments), "MIMSBorrowpitPhotos_DATA");
			if (!DB_file.Exists ()) {
				return string.Format ("Database not found at at : {0}", DB_file);
			}
			else {
				return string.Format ("Database found");
			}
		}
		*/



		/// <summary>
		/// Calculates the size of the in sample.
		/// </summary>
		/// <returns>The in sample size.</returns>
		/// <param name="options">Options.</param>
		/// <param name="reqWidth">Req width.</param>
		/// <param name="reqHeight">Req height.</param>
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


		//DecodeByteArray(thumbnail,0, thumbnail.length,options);//Decode image, "thumbnail" is the object of image fil

		//Bitmap songImage1 = BitmapFactory.DecodeByteArray(thumbnail,0, thumbnail.length,options);//Decode image, "thumbnail" is the object of image file
		Bitmap songImage = Bitmap.CreateScaledBitmap(songImage1, width , height , true);// convert decoded bitmap into well scalled Bitmap format.
			songImage1.Recycle();
		return songImage;
	}

		/*
		public static void CopyInitialDBtoFinal(string DatabaseFileNAme, string DatabaseFinalDirectory)
		{
			string dbName = DatabaseFileNAme;
			string dbPath = System.IO.Path.Combine (DatabaseFinalDirectory, dbName);
			// Check if your DB has already been extracted.
			if (!File.Exists(dbPath))
			{
				string src = System.IO.Path.Combine (cpath, dbName);
				string cpath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write (buffer, 0, len);
						}
					}
				}
			}
			}
		*/
	}
}


