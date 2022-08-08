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
using System.Windows.Shapes;
using Model;
using WeatherDatabase;

namespace WeatherManagement
{
    /// <summary>
    /// Interaction logic for CaptureWeather.xaml
    /// </summary>
    public partial class CaptureWeather : Window
    {
        private static readonly string timeFormat = "HH:mm";

        public CaptureWeather()
        {
            InitializeComponent();

            Initialize();
        }
        
        public void Initialize()
        {
            datePicker.SelectedDate = DateTime.Now;
            txtTime.Text = DateTime.Now.ToString(timeFormat);

            foreach(string enumValue in Enum.GetNames(typeof(WeatherType)))
            {
                comboBoxType.Items.Add(enumValue);
            }

            comboBoxType.SelectedIndex = 0;
        }

        private void bttnSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime;
            WeatherType type;
            int temperature;

            try
            {
                dateTime = DateTime.Parse($"{datePicker.DisplayDate}");
                dateTime = dateTime.Add(TimeSpan.Parse(txtTime.Text));

                type = (WeatherType) Enum.Parse(typeof(WeatherType), comboBoxType.SelectedItem.ToString());

                temperature = int.Parse(txtTemperature.Text);

                Database.addWeatherRecord(new WeatherRecord(dateTime, type, temperature, commentBox.Text));

                this.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                txtMessage.Text = "Eine Eingabe ist nicht korrekt.";

                bttnSave.BorderBrush = Brushes.Red;
            }
        }
    }
}
