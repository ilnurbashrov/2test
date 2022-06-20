using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditServicePage.xaml
    /// </summary>
    public partial class EditServicePage : Page
    {
        Service service = new Service();
        public EditServicePage(Service service)
        {
            this.service = service;
            DataContext = service;
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if(open.ShowDialog()== true)
            {
                service.MainImage = File.ReadAllBytes(open.FileName);
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(service.ID == 0)
                {
                    EFModel.Init().Services.Add(service);                
                  
                }
                EFModel.Init().SaveChanges();
                MessageBox.Show("Сохранено");
                NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
