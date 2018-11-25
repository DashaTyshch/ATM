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
    /// Interaction logic for TransferPage.xaml
    /// </summary>
    public partial class TransferPage : Page
    {
        public TransferPage()
        {
            InitializeComponent();
        }
       
        private void SendTransferButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://tktbanking.azurewebsites.net/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var mobTransfer = new TransferCreateDto
            {
                Amount = double.Parse(AmountTextBox.Text), // Check for valid number
                FromId = CardComboBox.SelectedValue.ToString(), // convert selected item value to string
                ToId = CardTextBox.Text
            };

            var response = client.PostAsJsonAsync("api/transfers/perform", mobTransfer).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transfer is done");
                AmountTextBox.Text = "";
                //FromId = ;
                CardTextBox.Text = "";

                // TODO:: and where futher?
                // To previous page
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void Back_To_Main_Click(object sender, RoutedEventArgs e)
        {
            MainPage MP = new MainPage();
            this.NavigationService.Navigate(MP);
        }
    }
}
