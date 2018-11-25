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
using ATM.DataModels;
using ATM.Services;
using System.Text.RegularExpressions;

namespace ATM.Pages
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        Regex phoneValid = new Regex(@"^\+380([0-9]){9}$");

        public SignUp()
        {
            InitializeComponent();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            var userInfo = new UserUpdDto
            {
                Name = NameTextBox.Text,
                Surname = SurnameTextBox.Text,
                PhoneNumber = LoginTextBox.Text,
                Password = PasswordTextBox.Password
            };

            if (userInfo.Name == "" || userInfo.Surname == "" 
                || userInfo.PhoneNumber == "" || userInfo.Password == "")
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка реєстрації");
            }
            else if (!phoneValid.IsMatch(userInfo.PhoneNumber))
            {
                MessageBox.Show("Неправильний формат телефону.\nПеревірте і спробуйте ще раз.", "Помилка реєстрації");
            }
            else if (userInfo.Password.Length < 8 )
            {
                PasswordTextBox.Password = "";
                MessageBox.Show("Пароль має складатися з 8 чи більше символів!", "Помилка реєстрації");
            }
            else
            {
                if (BankingApiClient.GetInstance().Register(userInfo.Name, userInfo.Surname, userInfo.PhoneNumber, userInfo.Password))
                {
                    NameTextBox.Text = "";
                    SurnameTextBox.Text = "";
                    LoginTextBox.Text = "+380";
                    PasswordTextBox.Password = "";

                    // To next page
                    MainPage MainP = new MainPage();
                    this.NavigationService.Navigate(MainP);
                }
                else
                {
                    PasswordTextBox.Password = "";
                    MessageBox.Show("Вказаний номер телефону вже зареєстрований!", "Помилка реєстрації");
                }
            }
        }

        private void ButtonToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPage LogP = new LoginPage();
            this.NavigationService.Navigate(LogP);
        }
    }
}
