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

namespace ATM.Pages
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://tktbanking.azurewebsites.net/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var userInfo = new UserUpdDto
            {
                Name = NameTextBox.Text,
                Surname = SurnameTextBox.Text,
                PhoneNumber = LoginTextBox.Text,
                Password = PasswordTextBox.Password
            };

            var response = client.PostAsJsonAsync("api/auth/signup", userInfo).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User Signed up");
                NameTextBox.Text = "";
                SurnameTextBox.Text = "";
                LoginTextBox.Text = "";
                PasswordTextBox.Password = "";
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
