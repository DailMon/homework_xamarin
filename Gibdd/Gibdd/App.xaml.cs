using System;
using System.IO;
using Xamarin.Forms;


namespace Gibdd
{
   
    public partial class App : Application
    {
        static ProfileDatabase database;

        public static ProfileDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ProfileDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Profiles.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
