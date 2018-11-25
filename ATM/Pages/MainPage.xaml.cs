using ATM.DataModels;
using ATM.Services;
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
        private UserGetDto currUser;
        public MainPage()
        {
            InitializeComponent();
            currUser = BankingApiClient.GetInstance().CurrentUser();
            this.GreetingText.Text = $"Вітаємо,\n{currUser.Surname} {currUser.Name}!";

            var cards = currUser.Accounts;
            foreach (var card in cards)
            {
                Button butt = new Button();
                butt.Content = $"# {card.AccountNumber}\nБаланс - {card.Balance} грн.";
                butt.Name = "Button" + card.AccountNumber;
                butt.FontSize = 19;
                butt.Tag = card.AccountNumber;
                butt.Click += Button_Click;
                CardPanel.Children.Add(butt);

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            MessageBox.Show(b.Tag as string);
        }

        private void MobileTransferButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:: go to mobile transfer page
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("Pages/MobileTransferPage.xaml", UriKind.RelativeOrAbsolute));

        }
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri("Pages/TransferPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CharityButton_Click(object sender, RoutedEventArgs e)
        {
            Xceed.Wpf.Toolkit.MessageBox msg = new Xceed.Wpf.Toolkit.MessageBox
            {
                WindowBackground = Brushes.Snow
            };
            msg.Caption = "Благодійсність";
            msg.Text = "Ви маєте добре серце, але дана функція наразі недоступна.";
            msg.ShowDialog();

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            BankingApiClient.GetInstance().Reset();
            LoginPage LogP = new LoginPage();
            this.NavigationService.Navigate(LogP);
        }

        private void Button_Click_Add_Card(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви дійсно бажаєте додати нову картку?",
                                          "Підтвердження",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (BankingApiClient.GetInstance().OpenAccount(currUser.PhoneNumber))
                {
                    MainPage MP = new MainPage();
                    this.NavigationService.Navigate(MP);
                } else
                {
                    MessageBox.Show("Відбулася якась помилка.\nСпробуйте ще раз.", "Помилка");
                }
                
            }
            
        }
    }
}
