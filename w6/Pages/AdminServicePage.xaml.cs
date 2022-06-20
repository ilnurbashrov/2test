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
    /// Логика взаимодействия для AdminServicePage.xaml
    /// </summary>
    public partial class AdminServicePage : Page
    {
        public AdminServicePage()
        {
            InitializeComponent();
            cbSort.Items.Add("ПО ВОЗРАСТАНИЮ");
            cbSort.Items.Add("ПО УБЫВАНИЮ");
            cbSort.SelectedIndex = 0;
            UpdateData();
        }

        public void UpdateData()
        {
            IEnumerable<Service> services = EFModel.Init().Services.Where(s => s.Title.Contains(tbSearch.Text) || s.Description.Contains(tbSearch.Text));

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    services = services.OrderBy(s => s.Cost);
                    break;
                case 1:
                    services = services.OrderByDescending(s => s.Cost);
                    break;
            }
            LvService.ItemsSource = services.ToList();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
      
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            Service service = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new EditServicePage(service));
        }

        private void btDelet_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("вы действительно хотите удалить?", "внимание!", MessageBoxButton.YesNo)== MessageBoxResult.Yes)
            {
                try
                {
                    Service service = (sender as Button).DataContext as Service;
                    EFModel.Init().Services.Remove(service);
                    EFModel.Init().SaveChanges();
                    MessageBox.Show("Удалено!");
                    UpdateData();
                }
                catch(Exception)
                {
                    MessageBox.Show("ошибка!");
                }
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddServicePage());
        }

        private void btRec_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RecordingServicePage());
        }
    }
}
