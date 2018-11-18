using ATM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MobileTransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:: go to mobile transfer page
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("Pages/MobileTransferPage.xaml", UriKind.RelativeOrAbsolute));

        }
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:: go to transfer page
        }
        private void CharityButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
