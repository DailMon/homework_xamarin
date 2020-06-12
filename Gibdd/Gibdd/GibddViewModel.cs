
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


namespace Gibdd
{
    public class GibddViewModel : INotifyPropertyChanged
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
                if (value != null)
                {
                    selectedProfile = value;
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
                    if (value.IsCompany)
                    {
                        selectedProfile.TypeProfile = "Организация";
                    }
                    else
                    {
                        selectedProfile.TypeProfile = "Гражданин";
                    }
                }
            }
        }     


        public ICommand ProfileChoosed_Command { get; set; }
        public ICommand SaveProfile_Command { get; set; }
        public ICommand TakePhoto_Command { get; set; }
        public ICommand PickPhoto_Command { get; set; }
        public ICommand DeletePhoto_Command { get; set; }        
        public ICommand ImageTapped_Command { get; set; }
        public ICommand FileButton_Command { get; set; }

        private IGibddModel gibddModel;

        public GibddViewModel()
        {
            gibddModel = new GibddModel();

            ProfileChoosed_Command = new Command(OnChooseProfileClick);
            SaveProfile_Command = new Command(OnSaveProfileClick, () => CanSave);
            TakePhoto_Command = new Command(OnTakePhoto);
            PickPhoto_Command = new Command(OnPickPhoto);
            DeletePhoto_Command = new Command(OnDeletePhoto);            
            ImageTapped_Command = new Command(OnImageTapped);
            FileButton_Command = new Command(OnFileButton);
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
        
        public List<Profile> ProfileItems { get; set; }
        private async void OnSaveProfileClick()
        {
            SelectedProfile.Name = Name;
            SelectedProfile.FirstName = FirstName;
            SelectedProfile.SecondName = SecondName;
            SelectedProfile.Email = Email;
            SelectedProfile.Region = Region;
            SelectedProfile.Subdivision = Subdivision;
            if (selectedProfile.IsCompany)
            {
                SelectedProfile.TypeProfile = "Организация";
            }
            else
            {
                SelectedProfile.TypeProfile = "Гражданин";
            }
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

        public ObservableCollection<MyImage> ImageItems
        {
            get { return gibddModel.ImageItems; }
        }
        
        private void OnTakePhoto()
        {
            var task = gibddModel.TakePhoto();
            task.ContinueWith((_) => {
                NotyfyProrertyChanged(nameof(ImageItems));
                NotyfyProrertyChanged(nameof(IsPhotoAdd));
            });
        }       

        private void OnPickPhoto()
        {
            var task = gibddModel.PickPhoto();
            task.ContinueWith((_) => {
                NotyfyProrertyChanged(nameof(ImageItems));
                NotyfyProrertyChanged(nameof(IsPhotoAdd));
            });           
        }

        public MyImage SelectedPhoto { get; set; }
        private void OnDeletePhoto()
        {
            gibddModel.DeletePhoto(SelectedPhoto);
            NotyfyProrertyChanged(nameof(ImageItems));            
            NotyfyProrertyChanged(nameof(IsPhotoAdd));
        }

        
        public bool IsPhotoAdd { 
            get { return gibddModel.IsPhotoAdd; }
            set { gibddModel.IsPhotoAdd = value;  } 
        }  
        
        private void OnImageTapped()
        {
            NotyfyProrertyChanged(nameof(SelectedPhoto));
        }

        private async void OnFileButton()
        {
            var curText = new TextAppeal();
            curText.Text = CurrentTextAppeal;
            await App.Database.SaveTextAppealAsync(curText);            
            CurrentTextAppeal = "";
            ImageItems.Clear();
            IsPhotoAdd = false;
        }
    }
}
