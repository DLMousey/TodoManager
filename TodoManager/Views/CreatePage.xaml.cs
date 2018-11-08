using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DataAccessLibrary;

namespace TodoManager.Views
{
    public partial class CreatePage : Page
    {
        public CreatePage()
        {
            this.InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ListPage));
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            TodoModel Todo = new TodoModel
            {
                Title = Title.Text,
                Description = Detail.Text
            };

            DataAccess.AddData(Todo);
            this.Frame.Navigate(typeof(ListPage));
        }
    }
}
