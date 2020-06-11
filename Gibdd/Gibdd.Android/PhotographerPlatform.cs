using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Gibdd.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;
/*
[assembly: Dependency(typeof(PhotographerPlatform))]
namespace Gibdd.Droid
{
    
    public class PhotographerPlatform : IPhotographerPlatform
    {
        private const int CameraRequest = 2;
        public Action<byte[]> PhotoCallback { get; set; }

        private byte[] imageData;      

        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }

        public bool IsCameraAvailable()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            var availableActivities = Application.Context.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }        

        public void TakePhoto()
        {
            var permission = Android.Manifest.Permission.Camera;
            if (Application.Context.CheckSelfPermission(permission) == Permission.Granted)
            {
                TakePhotoInternal();
            }
            else
            {
                ActivityCompat.RequestPermissions(MainActivity.Instance, new[] { permission }, 0);
            }
        }

        private void TakePhotoInternal()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            MainActivity.Instance.StartActivityForResult(intent, CameraRequest);
        }

        public void SaveImage(byte[] data)
        {
            this.imageData = data;
            var permission = Android.Manifest.Permission.WriteExternalStorage;
            if (Application.Context.CheckSelfPermission(permission) == Permission.Granted)
            {
                SaveImageInternal();
            }
            else
            {
                ActivityCompat.RequestPermissions(MainActivity.Instance, new[] { permission }, 0);
            }
        }

        private void SaveImageInternal()
        {
            var pictures = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            if (!pictures.Exists())
            {
                pictures.Mkdirs();
            }
            var file = new Java.IO.File(pictures, $"Photo{Guid.NewGuid()}.jpg");
            File.WriteAllBytes(file.Path, imageData);
            var intent = new Intent(Intent.ActionMediaScannerScanFile);
            intent.SetData(Android.Net.Uri.FromFile(file));
            MainActivity.Instance.SendBroadcast(intent);
        }
    }
}
*/