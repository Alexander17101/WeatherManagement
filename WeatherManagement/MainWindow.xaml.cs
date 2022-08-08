using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherDatabase;

namespace WeatherManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Database.GenerateMockData();

            DrawStatistics(); 
        }

        private void menuItemDataCaptureWeather(object sender, RoutedEventArgs e)
        {
            CaptureWeather window = new CaptureWeather();

            window.Owner = this;
            window.ShowDialog();
        }

        private void DrawStatistics()
        {
            const int daysToUse = 7;
            DateTime from = DateTime.Now.Subtract(new TimeSpan(daysToUse, 0, 0, 0));

            List<WeatherRecord> records = Database.GetWeatherRecordsBetween(from, DateTime.Now);

            int max = records.Max((record) => { return (int) record.TemperatureInCelcius; });

            double widthStep = canvas.Width / records.Count;
            double heightStep = canvas.Height / max;

            for (int i = 0; i < records.Count - 1; i++)
            {
                double x1 = i * widthStep, y1 = canvas.Height - records[i].TemperatureInCelcius * heightStep;
                double x2 = (i + 1) * widthStep, y2 = canvas.Height - records[i + 1].TemperatureInCelcius * heightStep;
                double lineThickness = 2.5;

                DrawLineWithAnimation(x1, y1, x2, y2, lineThickness);
                DisplayText(x1, y1 + 20, records[i].TemperatureInCelcius + " °C");

                if(i == records.Count - 2)
                {
                    DisplayText(x2, y2 + 20, records[i + 1].TemperatureInCelcius + " °C");
                }
            }
        }

        private void DrawLine(double x1, double y1, double x2, double y2, double thickness)
        {
            Line line = new Line();

            line.Stroke = Brushes.Black;
            line.StrokeThickness = thickness;

            line.X1 = x1;
            line.Y1 = y1;

            line.X2 = x2;
            line.Y2 = y2;

            canvas.Children.Add(line);
        }

        private void DrawLineWithAnimation(double x1, double y1, double x2, double y2, double thickness)
        {
            const int durationInSec = 1;
            Line line = new Line();
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimationY;
            DoubleAnimation doulbeAnimationX;

            line.Stroke = Brushes.Black;
            line.StrokeThickness = thickness;

            line.X1 = x1;
            line.Y1 = y1;

            line.X2 = x2;
            line.Y2 = y2;

            doulbeAnimationX = new DoubleAnimation(line.X1, line.X2, new Duration(new TimeSpan(0, 0, durationInSec)));
            doubleAnimationY = new DoubleAnimation(line.Y1, line.Y2, new Duration(new TimeSpan(0, 0, durationInSec)));

            Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("(Line.Y2)"));
            Storyboard.SetTargetProperty(doulbeAnimationX, new PropertyPath("(Line.X2)"));

            storyboard.Children.Add(doubleAnimationY);
            storyboard.Children.Add(doulbeAnimationX);

            canvas.Children.Add(line);
            line.BeginStoryboard(storyboard);
        }

        private void DisplayText(double x, double y, string text)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);

            canvas.Children.Add(textBlock);
        }
    }
}
