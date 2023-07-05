using DemoAPI.Request;
using DemoAPI.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoAPI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async Task<EmployeeResponse> RefreshDataAsync()
        {
            HttpClient client = new HttpClient();
            EmployeeResponse employeeResponse= new EmployeeResponse();

            Uri uri = new Uri("https://dummy.restapiexample.com/api/v1/employees");
         
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                employeeResponse = JsonConvert.DeserializeObject<EmployeeResponse>(content);
                
            }
            return employeeResponse;


        }


        public async Task<bool> InsertDataAsync(EmployeeRequest request)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            var uri = new Uri("https://dummy.restapiexample.com/api/v1/create");
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                DisplayAlert("Exitoso", "Message", "Ok");
                return true;
            }
            DisplayAlert("Error", "Message", "Ok");
            return false;

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var response = await RefreshDataAsync();
            if (response.Status== "success")            
            {
                lvEmployee.ItemsSource = response.Data.ToList();
                DisplayAlert("Exitoso", "Message", "Ok");
            }
            else
            {
                DisplayAlert("Error", "Message", "Ok");
            }
            
            DisplayAlert("Sincrono", "Message","Ok");
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await InsertDataAsync(new EmployeeRequest { Name = "Hugo", Age = 45, Salary = 8000 });
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    lvEmployee.ItemsSource = a RefreshDataAsync.ToList();
        //}
    }
}
