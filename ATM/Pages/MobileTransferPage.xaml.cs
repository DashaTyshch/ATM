using ATM.DataModels;
using ATM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MobileTransferPage.xaml
    /// </summary>
    public partial class MobileTransferPage : Page
    {
        Regex phoneValid = new Regex(@"^\+380([0-9]){9}$");
        Regex currencyValid = new Regex(@"^[0-9]+(,[0-9]{1,2})?$");
        private UserGetDto currUser;
        public MobileTransferPage()
        {
            InitializeComponent();

            currUser = BankingApiClient.GetInstance().CurrentUser();
            var cards = currUser.Accounts;
            for (var i = 0; i < cards.Length; i++)
            {
                ComboBoxItem boxItem = new ComboBoxItem();
                boxItem.Content = cards[i].AccountNumber;
                if (i == 0)
                    boxItem.IsSelected = true;
                CardComboBox.Items.Add(boxItem);
            }
        }

        private void SendMobileTramsferButton_Click(object sender, RoutedEventArgs e)
        {
            var mobTransfer = new CreateMobileTransferDto();
            var amount = AmountTextBox.Text.Replace('.', ',');

            if (currencyValid.IsMatch(amount))
            {

                mobTransfer.Amount = double.Parse(amount) + 1;
                mobTransfer.FromId = CardComboBox.Text;
                mobTransfer.PhNum = PhoneTextBox.Text;

                if (mobTransfer.Amount > currUser.Accounts.Where(a => a.AccountNumber == mobTransfer.FromId).First().Balance)
                {
                    Xceed.Wpf.Toolkit.MessageBox msg = new Xceed.Wpf.Toolkit.MessageBox
                    {
                        WindowBackground = Brushes.Snow
                    };
                    msg.Caption = "Помилка";
                    msg.Text = "На рахунку недостатньо коштів!";
                    msg.ShowDialog();

                }
                else if (!phoneValid.IsMatch(mobTransfer.PhNum))
                {
                    Xceed.Wpf.Toolkit.MessageBox msg = new Xceed.Wpf.Toolkit.MessageBox
                    {
                        WindowBackground = Brushes.Snow
                    };
                    msg.Caption = "Помилка";
                    msg.Text = "Неправильний формат телефону.\nПеревірте і спробуйте ще раз.";
                    msg.ShowDialog();
                }
                else
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://tktbanking.azurewebsites.net/");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsJsonAsync("api/transfers/performmobile", mobTransfer).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Xceed.Wpf.Toolkit.MessageBox msg = new Xceed.Wpf.Toolkit.MessageBox
                        {
                            WindowBackground = Brushes.Snow
                        };
                        msg.Caption = "Завершено";
                        msg.Text = "Операція пройшла успішно!";
                        msg.ShowDialog();
                        AmountTextBox.Text = "100";
                        PhoneTextBox.Text = "+380";
                    }
                    else
                    {
                        MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                    }
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox msg = new Xceed.Wpf.Toolkit.MessageBox
                {
                    WindowBackground = Brushes.Snow
                };
                msg.Caption = "Помилка";
                msg.Text = "Неправильний формат суми!";
                msg.ShowDialog();
            }
                
        }

        private void Back_To_Main_Click(object sender, RoutedEventArgs e)
        {
            MainPage MP = new MainPage();
            this.NavigationService.Navigate(MP);
        }
    }
}
