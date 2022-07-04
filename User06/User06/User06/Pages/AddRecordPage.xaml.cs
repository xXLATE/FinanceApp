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
    public partial class AddRecordPage : ContentPage
    {
        private User _currentUser;
        private Record _currentRecord;
        private IEnumerable<Category> _categories;
        private IEnumerable<RecordType> _recordTypes;
        private bool _isChange;
        private RecordType _selectedType;
        private Category _selectedCategory;

        /// <summary>
        /// для создания
        /// </summary>
        /// <param name="currentUser"></param>
        public AddRecordPage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            TrashButton.IsVisible = false;
            TitleLabel.Text = "Создать запись";
        }

        /// <summary>
        /// для изменения
        /// </summary>
        /// <param name="currensUser"></param>
        /// <param name="record"></param>
        public AddRecordPage(User currensUser, Record record)
        {
            InitializeComponent();
            _currentUser = currensUser;
            _currentRecord = record;

            TrashButton.IsVisible = true;
            TitleLabel.Text = "Изменить запись";
            TitleLabel.Margin = new Thickness(-15, 0, 0, 0);

            _isChange = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _categories = App.Db.GetCategories();
            _recordTypes = App.Db.GetRecordTypes();

            CategoryPicker.ItemsSource = _categories.Select(category => category.Name).ToList();
            TypePicker.ItemsSource = _recordTypes.Select(recordType => recordType.Name).ToList();

            if (_isChange == true)
            {
                TypePicker.SelectedItem = _recordTypes.First(recordType => recordType.Id == _currentRecord.Type_Id).Name;
                CategoryPicker.SelectedItem = _categories.First(category => category.Id == _currentRecord.Category_Id).Name;
                SumEntry.Text = _currentRecord.Sum;
                Date.Date = _currentRecord.Date;
                Description.Text = _currentRecord.Description;
            }
        }

        private async void TrashButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Удаление", "Вы действительно хотите удалить?", "Да", "Нет"))
            {
                App.Db.DeleteRecord(_currentRecord);
                await Navigation.PopAsync();
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// сохранение при создании и при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (_isChange)
            {
                _currentRecord.Type_Id = _selectedType.Id;
                _currentRecord.Category_Id = _selectedCategory.Id;
                _currentRecord.Sum = SumEntry.Text;
                _currentRecord.Date = Date.Date;
                _currentRecord.Description = Description.Text;

                App.Db.SaveRecord(_currentRecord);
            }
            else
            {
                Record record = new Record
                {
                    Type_Id = _selectedType.Id,
                    Category_Id = _selectedCategory.Id,
                    Sum = SumEntry.Text,
                    Date = Date.Date,
                    Description = Description.Text,
                    User_Id = _currentUser.Id,
                };

                App.Db.SaveRecord(record);
            }

            await Navigation.PopAsync();
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedType = _recordTypes.First(type => type.Name == TypePicker.Items[TypePicker.SelectedIndex]);
            }
            catch { }
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedCategory = _categories.First(category => category.Name == CategoryPicker.Items[CategoryPicker.SelectedIndex]);
            }
            catch { }
        }
    }
}