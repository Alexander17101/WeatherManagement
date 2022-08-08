using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum WeatherType
    {
        Clear,
        Cloudy,
        Rain,
        Thunderstorm,
        Snow
    }

    public class WeatherRecord
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public WeatherType Condition { get; private set; }
        public double TemperatureInCelcius { get; private set; }
        public String Comment { get; private set; }
   
        private static int nextId = 0;

        public WeatherRecord(DateTime DateTime, WeatherType Type, double Temperature, string Comment)
        {
            this.Id = getId();
            this.DateTime = DateTime;
            this.Condition = Type;
            this.TemperatureInCelcius = Temperature;
            this.Comment = Comment;
        }

        public WeatherRecord(DateTime DateTime, WeatherType Type, double Temperature)
        {
            this.Id = getId();
            this.DateTime = DateTime;
            this.Condition = Type;
            this.TemperatureInCelcius = Temperature;
        }

        private int getId()
        {
            return nextId++;
        }
    }
}
