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

namespace ATM.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        private void EditPasswordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_To_Main_Click(object sender, RoutedEventArgs e)
        {
            MainPage MP = new MainPage();
            this.NavigationService.Navigate(MP);
        }
    }


}
