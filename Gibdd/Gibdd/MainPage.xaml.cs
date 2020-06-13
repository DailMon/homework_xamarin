using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Gibdd
{   
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new GibddViewModel();            
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
