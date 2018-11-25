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
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private UserGetDto currUser;
        public AccountPage()
        {
            InitializeComponent();
            currUser = BankingApiClient.GetInstance().CurrentUser();
            this.NameTextBlock.Text = $"{currUser.Name}";
            this.SurnameTextBlock.Text = $"{currUser.Surname}";
            this.PhoneTextBlock.Text = $"{currUser.PhoneNumber}";
        }

        private void EditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var userUpdInfo = new UserUpdDto
            {
                Name = null,
                Surname = null,
                PhoneNumber = null,
                Password = NewPasswordTextBox.Password
            };

            if (userUpdInfo.Password.Length < 8)
            {
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
                MessageBox.Show("Пароль має складатися з 8 чи більше символів!", "Помилка оновлення паролю");
            }
            else if(NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
            {
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
                MessageBox.Show("Неоднакові паролі", "Помилка оновлення паролю");
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tktbanking.azurewebsites.net/");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));


                var response = client.PutAsJsonAsync("api/auth/updinfo/"+ currUser.PhoneNumber.Substring(1), userUpdInfo).Result;

                if (response.IsSuccessStatusCode)
                {
                    Xceed.Wpf.Toolkit.MessageBox msg = new Xceed.Wpf.Toolkit.MessageBox
                    {
                        WindowBackground = Brushes.Snow
                    };
                    msg.Caption = "Оновлено";
                    msg.Text = "Пароль успішно оновлено. Не забувайте його!";
                    msg.ShowDialog();
                    NewPasswordTextBox.Password = "";
                    ConfirmNewPasswordTextBox.Password = "";
                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }

        private void Back_To_Main_Click(object sender, RoutedEventArgs e)
        {
            MainPage MP = new MainPage();
            this.NavigationService.Navigate(MP);
        }
    }


}
