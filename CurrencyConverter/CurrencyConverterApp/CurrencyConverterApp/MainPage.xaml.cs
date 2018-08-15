using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace CurrencyConverterApp
{
    public partial class MainPage : ContentPage
    {
        private ExchangeRates exchangeRates;

        public MainPage()
        {
            InitializeComponent();

            sourcePicker.Items.Add("Swedish crown");
            sourcePicker.Items.Add("Euro");
            sourcePicker.Items.Add("Dollar");
            sourcePicker.Items.Add("Romanian Leu");

            targetPicker.Items.Add("Swedish crown");
            targetPicker.Items.Add("Euro");
            targetPicker.Items.Add("Dollar");
            targetPicker.Items.Add("Romanian Leu");

            this.LoadRates();

            convertButton.Clicked += ConvertButton_Clicked;
        }

        private async void ConvertButton_Clicked(object sender, EventArgs e)
        {
            //validates if there is an amount entered
            if (string.IsNullOrEmpty(amountEntry.Text))
            {
                await DisplayAlert("Error", "You must enter an amount greater than zero (0)", "Ok");
                amountEntry.Focus();
                return;
            }

            //validates if the amount is bigger than 0
            var amount = decimal.Parse(amountEntry.Text);
            if (amount <= 0)
            {
                await DisplayAlert("Error", "You must enter an amount greater than zero (0)", "Ok");
                amountEntry.Focus();
                return;
            }

            //validates if the first currency is entered
            if (sourcePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please select a currency", "Ok");
                amountEntry.Focus();
                return;
            }

            //validates if the second currency is entered
            if (targetPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please select a currency", "Ok");
                amountEntry.Focus();
                return;
            }

            var amountConverted = this.Convert(amount, sourcePicker.SelectedIndex, targetPicker.SelectedIndex);

            convertedLabel.Text = string.Format(
                "{0:N2} {1} = {2:N2} {3}",
                amount,
                sourcePicker.Items[sourcePicker.SelectedIndex],
                amountConverted,
                targetPicker.Items[targetPicker.SelectedIndex]);
        }

        private decimal Convert(decimal amount, int source, int target)
        {
            double rateSource = this.GetRate(source);
            double rateTarget = this.GetRate(target);
            return amount / (decimal)rateSource * (decimal)rateTarget;
        }

        private double GetRate(int index)
        {
            switch (index)
            {
                case 0: return exchangeRates.Rates.SEK;
                case 1: return exchangeRates.Rates.EUR;
                case 2: return exchangeRates.Rates.USD;
                case 3: return exchangeRates.Rates.RON;
                default: return 1;
            }
        }

        //this is where the magic happens
        private async void LoadRates()
        {
            activityIndicator.IsRunning = true;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://openexchangerates.org");
            string url = string.Format("/api/latest.json?app_id=3c5c81f9c8ec49c3baba4f3df9a62fc1");
            var response = await client.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;
            activityIndicator.IsRunning = false;
            convertButton.IsEnabled = true;

            if (string.IsNullOrEmpty(result))
            {
                await DisplayAlert("Error", "No response service, please try again later", "Ok");
                convertButton.IsEnabled = false;
                return;
            }

            this.exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(result);
        }
    }
}
