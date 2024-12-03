using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fahrenheit_Celsius
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged 
    {
        private string _celsius;
        private string _fanhrenheit;
        private double _celsiusResult;
        private double _fanhrenheitResult;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Celsius { get=>_celsius ; 
        set { 
              _celsius = value;
              OnPropertyChanged(nameof(Celsius));
            } }
        public string Fanhrenheit { get => _fanhrenheit;
            set { 
              _fanhrenheit = value;
              OnPropertyChanged(nameof(Fanhrenheit));
            } }
        public double CResult { get=> _celsiusResult; set { 
            _celsiusResult= value;
            OnPropertyChanged(nameof(CResult));
            }
        }
        public double FResult { get=> _fanhrenheitResult; set { 
              _fanhrenheitResult= value;
                OnPropertyChanged(nameof(FResult));
            } 
        }
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private async void _FButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await ConvertToDouble("F", Fanhrenheit);
                FResult = Math.Round(result, 2);
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Были введены не корректный данные. Попробуйте снова!");
            }
        }

        private async void _CButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await ConvertToDouble("C", Celsius);
                CResult= Math.Round(result,2);
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Были введены не корректный данные. Попробуйте снова!");
            } 
        }


        private async Task<double> ConvertToDouble(string unitType,string data)
        {
            if (unitType == "C") 
            { 
                bool isParsed = double.TryParse(data, out double temper);
                if(isParsed)
                {
                    return temper * 9 / 5 + 32;
                }
                else 
                {
                    throw new InvalidCastException("Введенное значение не верного формата");
                }
            }
            else
            {
                bool isParsed = double.TryParse(data, out double temper);
                if (isParsed)
                {
                    return (temper - 32) * 5 / 9;
                }
                else
                {
                    throw new InvalidCastException("Введенное значение не верного формата");
                }
            }
        }

        

        private void _FTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_FTextBox.Text))
            {
                _FTextBlock.Background = Brushes.Transparent;
                _FTextBox.Background = Brushes.White;
            }
            else
            {
                _FTextBlock.Background = Brushes.White;
                _FTextBox.Background = Brushes.Transparent;
            }

        }

        private void _CTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_CTextBox.Text))
            {
                _CTextBlock.Background = Brushes.Transparent;
                _CTextBox.Background = Brushes.White;
            }
            else
            {
                _CTextBlock.Background = Brushes.White;
                _CTextBox.Background = Brushes.Transparent;
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}