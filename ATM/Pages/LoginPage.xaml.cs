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

namespace ATM
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:56851/");
            client.BaseAddress = new Uri("https://tktbanking.azurewebsites.net/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/auth/signin").Result; // auth/signin ?????/

            if (response.IsSuccessStatusCode)
            {
               var user = response.Content.ReadAsAsync<IEnumerable<DataModels.Credentials>>().Result;
                // TODO:: and where futher?

               // usergrid.ItemsSource = users;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }


        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("http://localhost:56851/");

            client.BaseAddress = new Uri("https://tktbanking.azurewebsites.net/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var userCredentials = new DataModels.Credentials();

            userCredentials.Login = LoginTextBox.Text;
            userCredentials.Password = PasswordTextBox.Password;

            var response = client.PostAsJsonAsync("api/auth/signin", userCredentials).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User Logged in");
                LoginTextBox.Text = "";
                PasswordTextBox.Password = ""; // ??????????
                GetData();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
