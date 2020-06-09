using Gibdd.ScreenProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gibdd
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ProfilesViewModel();            
        }

        private void ChooseProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseProfile
                {
                BindingContext = this.BindingContext
            });
        }

        private async void FormAppeal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTextAppeal
            {
                BindingContext = this.BindingContext
            });
        }
    }
}
