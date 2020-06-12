using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

            EntryNameCompany.Placeholder = "";
            EntryAddInfomation.Placeholder = "";
            EntryOutgoingNumber.Placeholder = "";
            LabelDate.Text = "";
            EntryNumberLetter.Placeholder = "";

        }

        async void SaveProfile_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PopModalAsync();        
           
        }       

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
           if (SwitchTypeProfile.IsToggled)
            {                
                TypeProfile.Text = "Организация";                
                EntryNameCompany.Placeholder = "Наименование организации";
                EntryAddInfomation.Placeholder = "Дополнительная информация";
                EntryOutgoingNumber.Placeholder = "Исходящий номер";
                LabelDate.Text = "Дата регистрации документа в организации";
                EntryNumberLetter.Placeholder = "Номер заказного письма";

            } else
            {
                TypeProfile.Text = "Гражданин";
                EntryNameCompany.Placeholder = "";
                EntryAddInfomation.Placeholder = "";
                EntryOutgoingNumber.Placeholder = "";
                LabelDate.Text = "";
                EntryNumberLetter.Placeholder = "";
            }
        }
    }    
}