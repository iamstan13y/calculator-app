using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private decimal firstNumber;
        private string operatorName;
        private bool operatorClicked;

        private void BtnOne_Clicked(object sender, EventArgs e)
        {
            lblResult.Text = "1";
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;

            lblResult.Text = lblResult.Text == "0" || operatorClicked ?
                lblResult.Text = button.Text : 
                lblResult.Text += button.Text;

            operatorClicked = false;
        }

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            lblResult.Text = "0";
            firstNumber = 0;
        }

        private void BtnBackSpace_Clicked(object sender, EventArgs e)
        {
            string number = lblResult.Text;

            if(number != "0")
            {
                number = number.Remove(number.Length - 1, 1);

                lblResult.Text = string.IsNullOrEmpty(number) ?
                    lblResult.Text = "0" :
                    lblResult.Text = number;
            }
        }

        private void BtnOperation_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            operatorClicked = true;
            operatorName = button.Text;
            firstNumber = decimal.Parse(lblResult.Text);
        }

        private async void BtnPercent_Clicked(object sender, EventArgs e)
        {
            try
            {
                string number = lblResult.Text;

                if (number != "0")
                {
                    decimal percentValue = decimal.Parse(number);
                    string result = (percentValue / 100).ToString("0.##");
                    lblResult.Text = result;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void BtnEqual_Clicked(object sender, EventArgs e)
        {
            try
            {
                decimal secondNumber = decimal.Parse(lblResult.Text);
                string result = Calculate(firstNumber, secondNumber).ToString();
                lblResult.Text = result;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        public decimal Calculate(decimal firstNumber, decimal secondNumber)
        {
            decimal result = 0;

            switch (operatorName)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "÷":
                    result = firstNumber / secondNumber;
                    break;
                case "×":
                    result = firstNumber * secondNumber;
                    break;
            }

            return result;
        }
    }
}