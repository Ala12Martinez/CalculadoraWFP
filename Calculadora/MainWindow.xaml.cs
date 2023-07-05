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

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string value = (string)button.Content;

                if (IsNumber(value))
                {
                    HandleNumbers(value);
                }
                else if (IsOperator(value))
                {
                    HandleOperator(value);
                }

                else if (value == "=")
                {
                    HandleEquals(Screen.Text);
                }

                else if (value == "CE")
                {

                    Screen.Clear();

                }
                else if (value == "C")
                {
                    Screen.Text = Screen.Text.Remove(Screen.Text.Length - 1);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }

        }

        private bool IsNumber(string num)
        {
            return double.TryParse(num, out _);
        }

        private void HandleNumbers(string value)
        {
            if (string.IsNullOrEmpty(Screen.Text))
            {
                Screen.Text = value;
            }
            else
            {
                Screen.Text += value;
            }
        }

        private bool IsOperator(string possibleOperator)
        {
            return possibleOperator == "+" || possibleOperator == "-" || possibleOperator == "*" || possibleOperator == "/";
        }

        private bool isOperatorEntered = false;
        private void HandleOperator(string value)
        {
            if (!string.IsNullOrEmpty(Screen.Text) && !ContainsOtherOperators(Screen.Text))
            {
                Screen.Text += value;
                isOperatorEntered = true;
            }
        }

        private bool ContainsOtherOperators(string screenContent)
        {
            return screenContent.Contains("+") || screenContent.Contains("-") || screenContent.Contains("*") || screenContent.Contains("/");
        }

        private void HandleEquals(string screenContent)
        {
            string op = FindOperator(screenContent);
            if (!string.IsNullOrEmpty(op))
            {
                string[] numbers = screenContent.Split(new[] { op }, StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length == 2 && double.TryParse(numbers[0], out double n1) && double.TryParse(numbers[1], out double n2))
                {
                    switch (op)
                    {
                        case "+":
                            Screen.Text = (n1 + n2).ToString();
                            break;
                        case "-":
                            Screen.Text = (n1 - n2).ToString();
                            break;
                        case "*":
                            Screen.Text = (n1 * n2).ToString();
                            break;
                        case "/":
                            if (n2 != 0)
                                Screen.Text = (n1 / n2).ToString();
                            else
                                Screen.Text = "Error: División entre cero";
                            break;
                    }
                }
            }
        }

        private string Sum()
        {
            string[] number = Screen.Text.Split('+');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 + n2, 12).ToString();
        }

        private string Res()
        {
            string[] number = Screen.Text.Split('-');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 - n2, 12).ToString();
        }

        private string Div()
        {
            string[] number = Screen.Text.Split('/');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 / n2, 12).ToString();
        }

        private string Mul()
        {
            string[] number = Screen.Text.Split('*');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 * n2, 12).ToString();
        }

        private string FindOperator(string screenContent)
        {
            foreach (var c in screenContent)
            {
                if (IsOperator(c.ToString()))
                {
                    return c.ToString();
                }
            }
            return "";
        }
    }
}