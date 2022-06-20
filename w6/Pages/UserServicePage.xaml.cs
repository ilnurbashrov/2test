using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using w6.DateBase;

namespace w6.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserServicePage.xaml
    /// </summary>
    public partial class UserServicePage : Page
    {
        public UserServicePage()
        {
            InitializeComponent();
            cbSort.Items.Add("по возрастанию");
            cbSort.Items.Add("по убываню");
            cbSort.SelectedIndex = 0;
            UpdateData();
        }

        public void UpdateData()
        {
            IEnumerable<Service> services = EFModel.Init().Services.Where(s => s.Title.Contains(cbSearch.Text) || s.Description.Contains(cbSearch.Text));

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    services = services.OrderBy(w => w.Cost);
                    break;
                case 1:
                    services = services.OrderByDescending(w => w.Cost);
                    break;
            }
            LvService.ItemsSource = services.ToList();
        }

        private void cbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            Service service = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new OrderServicePage(service));
        }
    }
}
