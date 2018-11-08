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
        private int SelectedId;

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

            List<object> CurrentItems = Output.Items.ToList();
            List<string> FilteredValues = new List<string>();

            Output.ItemsSource = ExecuteFiltering(FilterText, FilteredValues, CurrentItems);
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

        private List<string> ExecuteFiltering(string FilterValue, List<String> FilteredValues, List<object> items)
        {
            foreach (String value in items)
            {
                if (value.ToLower().Contains(FilterValue) && !FilteredValues.Contains(value))
                {
                    FilteredValues.Add(value);
                }
            }

            return FilteredValues;
        }

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    Page DestinationPage = e.Content as Page;
        //    if (DestinationPage.GetType() != typeof(DetailPage))
        //    {
        //        return;
        //    }

        //    DetailPage DetailPage = e.Content as DetailPage;
        //    DetailPage.TodoId = this.SelectedId;
        //}
    }
}
