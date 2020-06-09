using Gibdd.ScreenProfile;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseProfile : ContentPage
    {

        public ChooseProfile()
        {
            InitializeComponent();            
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ProfilesListView.ItemsSource = await App.Database.GetAllProfilesAsync();
        }       

        private async void ChooseProfile_Clicked(object sender, EventArgs e)
        {            
            AddProfile_Button.IsEnabled = false;
            RemoveProfile_Button.IsEnabled = false;
            EditProfile_Button.IsEnabled = false;
            Profile profile = ProfilesListView.SelectedItem as Profile;
            if (profile != null)
            {
                await Navigation.PopAsync();
                ProfilesListView.SelectedItem = null;
            }            
            AddProfile_Button.IsEnabled = true;
            RemoveProfile_Button.IsEnabled = true;
            EditProfile_Button.IsEnabled = true;
        }

        private async void AddProfile_Clicked(object sender, EventArgs e)
        {
            ChooseProfile_Button.IsEnabled = false;
            AddProfile_Button.IsEnabled = false;
            RemoveProfile_Button.IsEnabled = false;
            EditProfile_Button.IsEnabled = false;
            ProfilesListView.SelectedItem = new Profile();
            await Navigation.PushAsync(new AddProfile
            {
                BindingContext = this.BindingContext
            });
            ChooseProfile_Button.IsEnabled = true;
            AddProfile_Button.IsEnabled = true;
            RemoveProfile_Button.IsEnabled = true;
            EditProfile_Button.IsEnabled = true;
        }

        private async void RemoveProfile_Clicked(object sender, EventArgs e)
        {
            ChooseProfile_Button.IsEnabled = false;
            AddProfile_Button.IsEnabled = false;
            EditProfile_Button.IsEnabled = false;
            Profile profile = ProfilesListView.SelectedItem as Profile;
            if (profile != null)
            {
                await App.Database.DeleteNoteAsync(profile);
                ProfilesListView.ItemsSource = await App.Database.GetAllProfilesAsync();
                ProfilesListView.SelectedItem = null;
            }
            ChooseProfile_Button.IsEnabled = true;
            AddProfile_Button.IsEnabled = true;
            EditProfile_Button.IsEnabled = true;
        }

        private async void EditProfile_Clicked(object sender, EventArgs e)
        {
            RemoveProfile_Button.IsEnabled = false;            
            AddProfile_Button.IsEnabled = false;
            ChooseProfile_Button.IsEnabled = false;
            Profile profile = ProfilesListView.SelectedItem as Profile;
            if (profile != null)
            {
                await Navigation.PushAsync(new AddProfile
                {
                    BindingContext = this.BindingContext
                });                
            }
            ChooseProfile_Button.IsEnabled = true;
            AddProfile_Button.IsEnabled = true;
            RemoveProfile_Button.IsEnabled = true;
        }
    }
}
