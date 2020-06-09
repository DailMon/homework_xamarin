using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gibdd.ScreenProfile
{
    public class ProfilesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotyfyProrertyChanged (string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
               
        public string NameProfile { get; set; } = "Профиль не выбран";


        string _name = "";
        public string Name
        {
            get { return _name; }
            set { 
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

        bool isCompany = false;
        public bool ProfileIsCompany
        {
            get { return isCompany; }
            set { isCompany = SelectedProfile.IsCompany; }
        }

        Profile selectedProfile = new Profile();
        public Profile SelectedProfile {
            get {return selectedProfile; }
            set { selectedProfile = value;
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
                    } else
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

        bool canSave = false;
        public bool CanSave { 
            get { return canSave; } 
            set { 
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

        private IProfilesModel profilesModel;
        public ICommand ProfileChoosed_Command { get; set; }
        public ICommand SaveProfile_Command { get; set; }
        public ICommand Switch_Toggled { get; set; }       
        
        public ProfilesViewModel()
        {
            ProfileChoosed_Command = new Command(OnChooseProfileClick);            
            SaveProfile_Command = new Command(OnSaveProfileClick, () => CanSave);            
            Switch_Toggled = new Command(OnSwitchToggled);            
            profilesModel = new ProfilesModel();
        }        

       
        private void OnChooseProfileClick()
        {
            if (SelectedProfile != null)
            {
                NameProfile = SelectedProfile.Name;                
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
        }

        private void OnSwitchToggled()
        {
            if (SelectedProfile.IsCompany)
            {
                SelectedProfile.TypeProfile = "Организация";
            }
            else
            {
                SelectedProfile.TypeProfile = "Гражданин";
            }
        }     

    }
}
