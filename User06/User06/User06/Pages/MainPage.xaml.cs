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
    public partial class MainPage : ContentPage
    {
        private User _currentUser;
        private IEnumerable<Record> _records;
        private IEnumerable<Category> _categories;
        private IEnumerable<RecordType> _types;

        public MainPage(User currentUser)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            _currentUser = currentUser;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _categories = App.Db.GetCategoriesByUser(_currentUser.Id);
            _types = App.Db.GetRecordTypes();
            _records = App.Db.GetRecordsByUser(_currentUser.Id);

            List<string> filterList = new List<string> { "All Categories" };
            filterList.AddRange(_categories.Select(type => type.Name).ToList());

            FilterPicker.ItemsSource = filterList;

            List<ProjectForListView> projectForListViews = new List<ProjectForListView>();

            foreach (var record in _records)
            {
                var recordCategoryName = _categories.First(category => category.Id == record.Category_Id).Name;
                var recordTypeName = _types.First(type => type.Id == record.Type_Id).Name;
                if (recordTypeName == "Расход")
                {
                    var projectForListView = new ProjectForListView(record.Id, recordCategoryName, recordTypeName, "-" + record.Sum);
                    projectForListViews.Add(projectForListView);
                }
                else
                {
                    var projectForListView = new ProjectForListView(record.Id, recordCategoryName, recordTypeName, "+" + record.Sum);
                    projectForListViews.Add(projectForListView);
                }
            }

            ProjectsLV.ItemsSource = projectForListViews;
        }

        private async void NewRecordButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRecordPage(_currentUser));
        }

        private async void NewCategoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCategoryPage(_currentUser));
        }

        private async void ProjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedProjectForListView = (ProjectForListView)e.Item;
            var selectedProject = _records.First(project => project.Id == selectedProjectForListView.RecordID);

            await Navigation.PushAsync(new AddRecordPage(_currentUser, selectedProject));
        }

        private void FilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Record> recordListForFilter = new List<Record>();
                List<ProjectForListView> projectForListViews = new List<ProjectForListView>();

                var selectedCategoryString = FilterPicker.Items[FilterPicker.SelectedIndex];

                if (selectedCategoryString == "All Categories")
                    recordListForFilter = _records.ToList();
                else
                {
                    var selectedCategoryId = _categories.First(x => x.Name == selectedCategoryString).Id;
                    recordListForFilter = _records.Where(x => x.Category_Id == selectedCategoryId).ToList();
                }

                foreach (var record in recordListForFilter)
                {
                    var recordCategoryName = _categories.First(category => category.Id == record.Category_Id).Name;
                    var recordTypeName = _types.First(type => type.Id == record.Type_Id).Name;

                    if (recordTypeName == "Расход")
                    {
                        var projectForListView = new ProjectForListView(record.Id, recordCategoryName, recordTypeName, "-" + record.Sum);
                        projectForListViews.Add(projectForListView);
                    }
                    else
                    {
                        var projectForListView = new ProjectForListView(record.Id, recordCategoryName, recordTypeName, "+" + record.Sum);
                        projectForListViews.Add(projectForListView);
                    }
                }

                ProjectsLV.ItemsSource = projectForListViews;
            }
            catch { }
        }
    }

    public struct ProjectForListView
    {
        public ProjectForListView(int recordId, string category, string typeName, string sum)
        {
            RecordID = recordId;
            CategoryName = category;
            TypeName = typeName;
            Sum = sum;
        }

        public int RecordID { get; }
        public string CategoryName { get; }
        public string TypeName { get; }
        public string Sum { get; }
    }
}