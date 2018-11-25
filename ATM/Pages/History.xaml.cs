using ATM.DataModels;
using ATM.Services;
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
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        private UserGetDto currUser;
        private string card;
        public History(string card)
        {
            InitializeComponent();
            this.card = card;

            currUser = BankingApiClient.GetInstance().CurrentUser();
            CardNum.Text = card;

            datePickerFrom.SelectedDate = DateTime.Now.AddDays(-1);
            datePickerTo.SelectedDate = DateTime.Now.Date;

            Show_History(card, datePickerFrom.DisplayDate, datePickerTo.DisplayDate);
        }

        private void Back_To_Main_Click(object sender, RoutedEventArgs e)
        {
            MainPage MP = new MainPage();
            this.NavigationService.Navigate(MP);
        }

        private void Show_History(string id, DateTime from, DateTime to)
        {
            var his = BankingApiClient.GetInstance().History(id, from, to);
            foreach (var h in his)
            {
                Border border = new Border
                {
                    BorderThickness = new Thickness(0, 1, 0, 0),
                    BorderBrush = Brushes.CadetBlue,
                    
                };
                StackPanel sp = new StackPanel();
                TextBlock tx = new TextBlock
                {
                    FontSize = 17,
                    Width = 340,
                    TextWrapping = TextWrapping.Wrap
                };

                TextBlock currency = new TextBlock
                {
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = $"{h.Amount} грн.",
                    Foreground = Brushes.OrangeRed,
                    FontWeight = FontWeights.Bold
                };
                TextBlock fee = new TextBlock
                {
                    FontSize = 15,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = Brushes.Gray,
                };

                TextBlock dt = new TextBlock
                {
                    FontSize = 14,
                    Foreground = Brushes.LightSlateGray,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Text = h.DatePerformed.ToString()
                };

                if (h.IsIncome)
                {
                    tx.Text = $"Зарахування на карту. Відправник: {h.PartnerInfo}";
                    currency.Foreground = Brushes.ForestGreen;

                    fee.Text = $"Комісія - {h.Amount*0.01} грн.";
                } else if (h.Discriminator == "Mobile Replenishment")
                {
                    tx.Text = $"Поповнення мобільного. Отримувач: {h.PartnerInfo}";
                } else
                {
                    tx.Text = $"Переказ на карту. Отримувач: {h.PartnerInfo}";
                }

                sp.Children.Add(tx);
                sp.Children.Add(currency);
                sp.Children.Add(fee);
                sp.Children.Add(dt);
                border.Child = sp;
                InfoPanel.Children.Add(border);
            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            InfoPanel.Children.Clear();
            Show_History(card, datePickerFrom.DisplayDate, datePickerTo.DisplayDate);
        }
    }
}
