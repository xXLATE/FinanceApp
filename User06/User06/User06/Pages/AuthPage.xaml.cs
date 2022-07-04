using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace User06.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private async void RegistrationButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void Eye1_Clicked(object sender, EventArgs e)
        {
            PasswordEntry1.IsPassword = false;

            await Task.Delay(1000);

            PasswordEntry1.IsPassword = true;
        }

        private async void AuthButton_Clicked(object sender, EventArgs e)
        {
            var allUsers = App.Db.GetUsers();

            if (NameEntry.Text == null && PasswordEntry1.Text == null)
                await DisplayAlert("Alert!", "Не правилный логин или пароль", "OK");
            else
            {
                bool haveUser = false;

                foreach (var user in allUsers)
                {
                    if (user.Login == NameEntry.Text && user.Password == PasswordEntry1.Text)
                    {
                        await Navigation.PushAsync(new MainPage(user));
                        haveUser = true;
                    }
                }

                if (!haveUser)
                    await DisplayAlert("Alert!", "Не правилный логин или пароль", "OK");
            }
        }
    }
}