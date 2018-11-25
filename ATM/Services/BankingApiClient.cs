using ATM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Services
{
    class BankingApiClient
    {
        private string SessionId;
        private HttpClient Client;
        private static BankingApiClient ApiClient;

        private BankingApiClient()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://tktbanking.azurewebsites.net/")
            };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static BankingApiClient GetInstance()
        {
            if (ApiClient == null)
                ApiClient = new BankingApiClient();
            return ApiClient;
        }

        public bool Login(string login, string password)
        {
            var userCredentials = new Credentials
            {
                Login = login,
                Password = password
            };

            var resp = Client.PostAsJsonAsync("api/auth/signin", userCredentials).Result;
            if (!resp.IsSuccessStatusCode)
                return false;
            SessionId = resp.Content.ReadAsAsync<string>().Result;
            return true;
        }

        public bool Register(string name, string surname, string login, string password)
        {
            var user = new UserUpdDto
            {
                Name = name,
                PhoneNumber = login,
                Password = password,
                Surname = surname
            };

            var resp = Client.PostAsJsonAsync("api/auth/signup", user).Result;
            if (!resp.IsSuccessStatusCode)
                return false;
            SessionId = resp.Content.ReadAsAsync<string>().Result;
            return true;
        }

        public UserGetDto CurrentUser()
        {
            if (SessionId == null)
                return null;
            var resp = Client.GetAsync($"api/auth/identify?sessId={SessionId}").Result;
            if (!resp.IsSuccessStatusCode)
                return null;
            return resp.Content.ReadAsAsync<UserGetDto>().Result;
        }
        public void Reset()
        {
            SessionId = null;
        }

        // must call CurrentUser after doing it to refresh data
        public bool OpenAccount(string phNum)
        {
            var resp = Client.PostAsJsonAsync("api/auth/createacc", new UserGetDto { PhoneNumber = phNum }).Result;
            return resp.IsSuccessStatusCode;
        }

        public AnyTransferDto[] History(string accId, DateTime from, DateTime to)
        {
            var path = $"/api/transfers/history?accnum={accId}&from={ToQueryArgument(from)}&to={ToQueryArgument(to.AddDays(1))}";
            var resp = Client.GetAsync(path).Result;
            if (!resp.IsSuccessStatusCode)
                return null;
            var res = resp.Content.ReadAsAsync<IEnumerable<AnyTransferDto>>().Result.ToArray();

            return res;
        }

        private string ToQueryArgument(DateTime dateTime)
        {
            return $"{dateTime.Month}%2F{dateTime.Day}%2F{dateTime.Year}";
        }

    }
}
