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
    public partial class AddCategoryPage : ContentPage
    {
        private User _currentUser;

        public AddCategoryPage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Удаление", $"Вы точно хотите создать категорию?", "Создать", "Отмена"))
            {
                var newType = new Category()
                {
                    Name = TypeEntry.Text,
                    User_Id = _currentUser.Id,
                };

                App.Db.SaveCategory(newType);

                await Navigation.PopAsync();
            }
        }
    }
}