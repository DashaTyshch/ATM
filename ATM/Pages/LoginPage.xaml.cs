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
using ATM.Pages;
using ATM.Services;

namespace ATM
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == "" || PasswordTextBox.Password == "")
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка входу");
            }
            else
            {
                if (BankingApiClient.GetInstance().Login(LoginTextBox.Text, PasswordTextBox.Password))
                {
                    LoginTextBox.Text = "";
                    PasswordTextBox.Password = "";

                    // To next page
                    MainPage MainP = new MainPage();
                    this.NavigationService.Navigate(MainP);
                } else {
                    PasswordTextBox.Password = "";
                    MessageBox.Show("Ви ввели неправильний логін або пароль!", "Помилка входу");
                }
            }
        }

        private void ButtonToSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp SUP = new SignUp();
            this.NavigationService.Navigate(SUP);
        }
    }
}
