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
    /// Логика взаимодействия для RecordingServicePage.xaml
    /// </summary>
    public partial class RecordingServicePage : Page
    {
      
        public RecordingServicePage()
        {
            InitializeComponent();
            UpdateData();
        }

        public void UpdateData()
        {
            IEnumerable<ClientService> clientServices = EFModel.Init().ClientServices.Where(w => w.Client.FirstName.Contains(tbSearch.Text));
            LvVizit.ItemsSource = clientServices.ToList();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void btDelet_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить?", "внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ClientService clientService = (sender as Button).DataContext as ClientService;
                    EFModel.Init().ClientServices.Remove(clientService);
                    EFModel.Init().SaveChanges();
                    MessageBox.Show("Удалено");
                    UpdateData();
                }
                catch(Exception)
                {
                    MessageBox.Show("ошибка!");
                }
            }
        }
    }
}
