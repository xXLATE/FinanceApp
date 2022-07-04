using System;
using System.IO;
using User06.DataBase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using User06.Pages;

namespace User06
{
    public partial class App : Application
    {
        private static DataAccess db;
        private const string DATABASE_NAME = "User06.db";

        public static DataAccess Db
        {
            get
            {
                if (db == null)
                {
                    db = new DataAccess(Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AuthPage())
            {
                BarBackgroundColor = Color.SaddleBrown
            };
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
