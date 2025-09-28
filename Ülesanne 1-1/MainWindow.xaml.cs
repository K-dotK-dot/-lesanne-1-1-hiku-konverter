using System;
using System.Globalization;
using System.Windows;

namespace Ülesanne_1_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            var fromIndex = FromCombo.SelectedIndex;
            var toIndex = ToCombo.SelectedIndex;
            FromCombo.SelectedIndex = toIndex;
            ToCombo.SelectedIndex = fromIndex;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(InputBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                MessageBox.Show("Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string fromUnit = ((System.Windows.Controls.ComboBoxItem)FromCombo.SelectedItem).Content.ToString();
            string toUnit = ((System.Windows.Controls.ComboBoxItem)ToCombo.SelectedItem).Content.ToString();

            try
            {
                double meters = ToMeters(value, fromUnit);
                double result = FromMeters(meters, toUnit);

                ResultText.Text = $"{value:N2} {fromUnit} = {result:N4} {toUnit}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Conversion Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double ToMeters(double value, string unit)
        {
            return unit switch
            {
                "Inches" => value * 0.0254,
                "Feet" => value * 0.3048,
                "Miles" => value * 1609.344,
                "Centimeters" => value / 100.0,
                "Meters" => value,
                "Kilometers" => value * 1000.0,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        private double FromMeters(double meters, string unit)
        {
            return unit switch
            {
                "Inches" => meters / 0.0254,
                "Feet" => meters / 0.3048,
                "Miles" => meters / 1609.344,
                "Centimeters" => meters * 100.0,
                "Meters" => meters,
                "Kilometers" => meters / 1000.0,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }
    }
}
