using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TodoManager.Views
{
    public partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
            Output.ItemsSource = DataAccess.GetData();
        }

        private void CreateNav(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreatePage));
        }

        private void FilterData(object sender, RoutedEventArgs e)
        {
            String FilterText = Filter_Box.Text;
            if (FilterText.Length == 0)
            {
                Output.ItemsSource = DataAccess.GetData();
                return;
            }

            List<object> CurrentItems = Output.Items.ToList(); // TODO : Investigate why ToList<TodoModel>() isn't working
            List<TodoModel> CastValues = new List<TodoModel>();
            foreach (object Todo in CurrentItems)
            {
                CastValues.Add((TodoModel)Todo);
            }

            List<TodoModel> FilteredValues = new List<TodoModel>();
            Output.ItemsSource = ExecuteFiltering(FilterText, FilteredValues, CastValues);
        }

        private void RemoveData(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void ViewDetail(object sender, RoutedEventArgs e)
        {
            string IdTag = ((MenuFlyoutItem)sender).Tag.ToString();
            int Id = int.Parse(IdTag);

            this.Frame.Navigate(typeof(DetailPage), Id);
        }

        private List<TodoModel> ExecuteFiltering(string FilterValue, List<TodoModel> FilteredValues, List<TodoModel> items)
        {
            foreach (TodoModel Todo in items)
            {
                if (Todo.Title.ToLower().Contains(FilterValue) || Todo.Description.ToLower().Contains(FilterValue))
                {
                    FilteredValues.Add(Todo);
                }
            }

            return FilteredValues;
        }
    }
}
