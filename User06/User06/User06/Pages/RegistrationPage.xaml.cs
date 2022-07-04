using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User06.DataBase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace User06.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (LoginEntry.Text == null || PasswordEntry1.Text == null || PasswordEntry2.Text == null || LoginEntry.Text == null)
                {
                    await DisplayAlert("Внимание!", "Введите в поля данные!", "OK");
                    return;
                }

                if (PasswordEntry1.Text != PasswordEntry2.Text)
                {
                    await DisplayAlert("Внимание!", "Введеные пароли не совпадают!", "OK");
                    return;
                }

                var user = new User
                {
                    Login = LoginEntry.Text,
                    Password = PasswordEntry1.Text,
                    Mail = MailEntry.Text,
                };

                var recordType1 = new RecordType
                {
                    Name = "Расход",
                };

                var recordType2 = new RecordType
                {
                    Name = "Приход",
                };

                App.Db.SaveUser(user);
                App.Db.SaveRecordType(recordType1);
                App.Db.SaveRecordType(recordType2);

                await Navigation.PopAsync();
            }
            catch
            {
                await DisplayAlert("Внимание!", "Проверьте введённые данные", "OK");
            }
        }

        private async void Eye1_Clicked(object sender, EventArgs e)
        {
            PasswordEntry1.IsPassword = false;

            await Task.Delay(1000);

            PasswordEntry1.IsPassword = true;
        }

        private async void Eye2_Clicked(object sender, EventArgs e)
        {
            PasswordEntry2.IsPassword = false;

            await Task.Delay(1000);

            PasswordEntry2.IsPassword = true;
        }
    }
}