using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileAppeal : ContentPage
    {
        public FileAppeal()
        {
            InitializeComponent();
        }

        private async void FileButton_Clicked(object sender, EventArgs e)
        {
            var existingPages = Navigation.NavigationStack.ToList();
            var mainPage = existingPages[0];
            for (int i = 1; i < existingPages.Count; i++)
            {
                Navigation.RemovePage(existingPages[i]);
            }
            await Navigation.PopAsync();
        }

        private async void Image_Tapped(object sender, EventArgs e)
        {
            if (CarouselView.CurrentItem != null)
            {
                await Navigation.PushAsync(new PhotoFullScreen
                {
                    BindingContext = this.BindingContext
                });
            }
        }
    }
}