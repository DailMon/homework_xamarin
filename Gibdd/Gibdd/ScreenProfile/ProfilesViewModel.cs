
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


namespace Gibdd.ScreenProfile
{
    public class ProfilesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotyfyProrertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string NameProfile { get; set; } = "Профиль не выбран";

        bool isChoosed = false;
        public bool IsChoosed
        {
            get { return isChoosed; }
            set
            {
                isChoosed = value;
                NotyfyProrertyChanged(nameof(IsChoosed));
            }
        }

        string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                ValidateToSave();
                NotyfyProrertyChanged(nameof(Name));
            }
        }

        string firstName = "";
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                ValidateToSave();
                NotyfyProrertyChanged(nameof(FirstName));
            }
        }

        string secondName = "";
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                ValidateToSave();
                NotyfyProrertyChanged(nameof(SecondName));
            }
        }

        string email = "";
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                ValidateToSave();
                NotyfyProrertyChanged(nameof(Email));
            }
        }

        string region = "";
        public string Region
        {
            get { return region; }
            set
            {
                region = value;
                ValidateToSave();
                NotyfyProrertyChanged(nameof(Region));
            }
        }

        string subdivision = "";
        public string Subdivision
        {
            get { return subdivision; }
            set
            {
                subdivision = value;
                ValidateToSave();
                NotyfyProrertyChanged(nameof(Subdivision));
            }
        }

        bool canSave = false;
        public bool CanSave
        {
            get { return canSave; }
            set
            {
                canSave = value;
                (SaveProfile_Command as Command).ChangeCanExecute();
                NotyfyProrertyChanged(nameof(CanSave));
            }
        }
        private void ValidateToSave()
        {
            if (Name.Length > 0
                && FirstName.Length > 0
                && SecondName.Length > 0
                && Email.Length > 0
                && Region.Length > 0
                && Subdivision.Length > 0)
            {
                CanSave = true;
                NotyfyProrertyChanged(nameof(CanSave));
            }
            else
            {
                CanSave = false;
                NotyfyProrertyChanged(nameof(CanSave));
            }
        }

        Profile selectedProfile = new Profile();
        public Profile SelectedProfile
        {
            get { return selectedProfile; }
            set
            {
                selectedProfile = value;
                if (selectedProfile != null)
                {
                    if (value.Name != null)
                    {
                        _name = value.Name;
                        firstName = value.FirstName;
                        secondName = value.SecondName;
                        email = value.Email;
                        region = value.Region;
                        subdivision = value.Subdivision;
                    }
                    else
                    {
                        _name = "";
                        firstName = "";
                        secondName = "";
                        email = "";
                        region = "";
                        subdivision = "";
                    }
                }
            }
        }

        bool isCompany = false;
        public bool ProfileIsCompany
        {
            get { return isCompany; }
            set { isCompany = SelectedProfile.IsCompany; }
        }

        private IProfilesModel profilesModel;
        public ICommand ProfileChoosed_Command { get; set; }
        public ICommand SaveProfile_Command { get; set; }        
        public ICommand TakePhoto_Command { get; set; }
        public ICommand PickPhoto_Command { get; set; }

        public ProfilesViewModel()
        {
            ProfileChoosed_Command = new Command(OnChooseProfileClick);
            SaveProfile_Command = new Command(OnSaveProfileClick, () => CanSave);
            TakePhoto_Command = new Command(OnTakePhoto);
            PickPhoto_Command = new Command(OnPickPhoto);
            profilesModel = new ProfilesModel();
        }

        private void OnChooseProfileClick()
        {
            if (SelectedProfile != null)
            {
                NameProfile = "Текущий профиль: " + SelectedProfile.Name;
                IsChoosed = true;
                NotyfyProrertyChanged(nameof(NameProfile));
            }
        }

        private async void OnSaveProfileClick()
        {
            SelectedProfile.Name = Name;
            SelectedProfile.FirstName = FirstName;
            SelectedProfile.SecondName = SecondName;
            SelectedProfile.Email = Email;
            SelectedProfile.Region = Region;
            SelectedProfile.Subdivision = Subdivision;
            await App.Database.SaveProfileAsync(SelectedProfile);
            NotyfyProrertyChanged(nameof(SelectedProfile));
        }

        string currentTextAppeal = "";
        public string CurrentTextAppeal
        {
            get { return currentTextAppeal; }
            set
            {
                currentTextAppeal = value;
                if (value.Length > 0)
                {
                    IsEditText = true;                   
                }
                else
                {
                    IsEditText = false;
                }                
                NotyfyProrertyChanged(nameof(CurrentTextAppeal));
            }
        }

        TextAppeal selectedTextAppeal = new TextAppeal();
        public TextAppeal SelectedTextAppeal
        {
            get { return selectedTextAppeal; }
            set
            {
                if (value != null)
                {
                    selectedTextAppeal = value;
                    CurrentTextAppeal = value.Text;
                    NotyfyProrertyChanged(nameof(SelectedTextAppeal));
                    NotyfyProrertyChanged(nameof(CurrentTextAppeal));
                }
            }
        }

        bool isEditText = false;
        public bool IsEditText
        {
            get { return isEditText; }
            set
            {
                isEditText = value;
                NotyfyProrertyChanged(nameof(IsEditText));
            }
        }

        ObservableCollection<MyImage> imageItems = new ObservableCollection<MyImage>();
        public ObservableCollection<MyImage> ImageItems
        {
            get { return imageItems; }
            set {
                NotyfyProrertyChanged(nameof(ImageItems));
            }           
        }

        private async void OnTakePhoto()
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
                CustomPhotoSize = 80,
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
            NotyfyProrertyChanged(nameof(ImageItems));            
        }

        private async void OnPickPhoto()
        {
            var curImage = new MyImage();
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 80,
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
                    NotyfyProrertyChanged(nameof(ImageItems));
                }
            }
        }

        public MyImage SelectedPhoto { get; set; }

    }
}
