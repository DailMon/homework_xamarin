using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Provider;
using Android.Support.V4.App;
using System.IO;
using Android.Graphics;
using System.Threading.Tasks;

namespace Gibdd.Droid
{
    [Activity(Label = "Gibdd", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        // private const int CameraRequest = 2;
        // public Action<byte[]> PhotoCallback { get; set; }
        // private byte[] imageData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App()); // this
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            /*
            for (int i = 0; i < permissions.Length; i++)
            {
                var p = permissions[i];
                var g = i < grantResults.Length ? grantResults[i] : Permission.Denied;
                if (p == Android.Manifest.Permission.Camera && g == Permission.Granted)
                {
                    TakePhotoInternal();
                }
                if (p == Android.Manifest.Permission.WriteExternalStorage && g == Permission.Granted)
                {
                    SaveImageInternal();
                }
            }
            */
        }
        /*
        public void TakePhoto()
        {
            var permission = Android.Manifest.Permission.Camera;
            if (Application.Context.CheckSelfPermission(permission) == Permission.Granted)
            {
                TakePhotoInternal();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new[] { permission }, 0);
            }
        }

        private void TakePhotoInternal()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, CameraRequest);
        }

        public bool IsCameraAvailable()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            var availableActivities = Application.Context.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == CameraRequest && resultCode == Result.Ok)
            {
                var bitmap = (Bitmap)data.Extras.Get("data");
                if (bitmap != null)
                {
                    byte[] bitmapData;
                    using (var stream = new System.IO.MemoryStream())
                    {
                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 80, stream);
                        bitmapData = stream.ToArray();
                    }
                    PhotoCallback(bitmapData);
                }
            }
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
                ActivityCompat.RequestPermissions(this, new[] { permission }, 0);
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
            SendBroadcast(intent);
        }

        public Task<Stream> GetImageStreamAsync()
        {
            throw new NotImplementedException();
        }
        */
    }
}