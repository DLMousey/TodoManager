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
    public partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();
        }

        public void GetTodo(int id)
        {
            TodoModel Todo = DataAccess.GetDetail(id);
            this.DataContext = Todo;
        }

        private void TodoList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ListPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.GetTodo(int.Parse(e.Parameter.ToString()));
        }
    }
}
