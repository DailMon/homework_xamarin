using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Gibdd
{
    class GibddModel : IGibddModel
    {

        ObservableCollection<MyImage> imageItems = new ObservableCollection<MyImage>();
        public ObservableCollection<MyImage> ImageItems { 
            get { return imageItems; }
            set { } 
        }

        bool isPhotoAdd = false;
        public bool IsPhotoAdd { get { return isPhotoAdd; } set { isPhotoAdd = value; } }

        public async Task TakePhoto()
        {
            await CrossMedia.Current.Initialize();
            Image img = new Image();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg",
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 75,
                CompressionQuality = 92
            });

            if (file == null)
                return;

            img.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            var curImage = new MyImage();
            curImage.Photo = img;
            curImage.PhotoSource = img.Source;
            if (img.Source is FileImageSource source)
            {
                curImage.NamePhoto = (string)source.File;
            }
            else
            {
                curImage.NamePhoto = "название отсутствует";
            }
            imageItems.Add(curImage);
            isPhotoAdd = true;            
        }

        public async Task PickPhoto()
        {
            var curImage = new MyImage();
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 75,
                    CompressionQuality = 92
                });

                curImage.PhotoSource = ImageSource.FromFile(photo.Path);

                if (photo != null)
                {
                    if (curImage.PhotoSource is FileImageSource source)
                    {
                        curImage.NamePhoto = (string)source.File;

                    }
                    else
                    {
                        curImage.NamePhoto = "название отсутствует";
                    }
                    imageItems.Add(curImage);                    
                    isPhotoAdd = true;                 

                }
            }
        }       
        public void DeletePhoto(MyImage SelectedPhoto)
        {
            imageItems.Remove(SelectedPhoto);
            if (imageItems.Count > 0)
            {
                isPhotoAdd = true;
            }
            else
            {
                isPhotoAdd = false;
            }
        }
    }
}
