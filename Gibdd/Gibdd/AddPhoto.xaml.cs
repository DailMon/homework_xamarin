using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPhoto : ContentPage
    {        
        public AddPhoto()
        {
            InitializeComponent();            
        }      

        private async void FileAppealButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FileAppeal
            {
                BindingContext = this.BindingContext
            });
        }
    }
}