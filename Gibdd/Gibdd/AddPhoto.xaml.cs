using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private async void Image_Tapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new PhotoFullScreen
            {
                BindingContext = this.BindingContext
            });
        }
    }
}