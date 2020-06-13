using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTextAppeal : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public EditTextAppeal()
        {
            InitializeComponent();            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TextAppealListView.ItemsSource = await App.Database.GetAllTextAppealsAsync();
            TextAppealListView.SelectedItem = null;
        }        

        private async void AddPhotoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPhoto
            {
                BindingContext = this.BindingContext
            });
        }
    }
}
