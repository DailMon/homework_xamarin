using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProfile : ContentPage
    {        
        public AddProfile()
        {
            InitializeComponent();          
        }
        async void SaveProfile_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PopAsync();      
           
        }       

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
           if (SwitchTypeProfile.IsToggled)
            {                
                TypeProfile.Text = "Организация";               
            } else
            {
                TypeProfile.Text = "Гражданин";                
            }
        }
    }    
}