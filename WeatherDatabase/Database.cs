using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace WeatherDatabase
{
    public static class Database
    {
        private static List<WeatherRecord> weatherRecords = new List<WeatherRecord>();

        public static void addWeatherRecord(WeatherRecord record)
        {
            weatherRecords.Add(record);
        }

        public static List<WeatherRecord> GetWeatherRecordsBetween(DateTime fromTime, DateTime toTime)
        {
            IEnumerable<WeatherRecord> query;

            if (fromTime == null || toTime == null)
                throw new ArgumentNullException("from or to cannot be null");
            if (toTime < fromTime)
                throw new ArgumentOutOfRangeException("to cannot be before from");

            query = from record
                    in weatherRecords
                    where record.DateTime > fromTime && record.DateTime < toTime
                    orderby record.DateTime ascending
                    select record;

            return query.ToList();
        }

        public static void GenerateMockData()
        {
            WeatherRecord wr1 = new WeatherRecord(DateTime.Now, WeatherType.Clear, 22);
            WeatherRecord wr2 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)), WeatherType.Clear, 10);
            WeatherRecord wr3 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0)), WeatherType.Clear, 15);
            WeatherRecord wr4 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(3, 0, 0, 0)), WeatherType.Clear, 12);
            WeatherRecord wr5 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(4, 0, 0, 0)), WeatherType.Clear, 28);
            WeatherRecord wr6 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(4, 0, 0, 0)), WeatherType.Clear, 24.5);
            WeatherRecord wr7 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(4, 0, 0, 0)), WeatherType.Clear, -10);
            WeatherRecord wr8 = new WeatherRecord(DateTime.Now.Subtract(new TimeSpan(4, 0, 0, 0)), WeatherType.Clear, 0);

            weatherRecords.Add(wr1);
            weatherRecords.Add(wr2);
            weatherRecords.Add(wr3);
            weatherRecords.Add(wr4);
            weatherRecords.Add(wr5);
            weatherRecords.Add(wr6);
            weatherRecords.Add(wr7);
            weatherRecords.Add(wr8);
        }

        /*private static void Test()
        {
            WeatherRecord record1 = new WeatherRecord(DateTime.Now, WeatherType.Clear);
            WeatherRecord record2 = new WeatherRecord(DateTime.Parse("22.10.2022"), WeatherType.Clear);
            WeatherRecord record3 = new WeatherRecord(DateTime.Parse("22.10.1980"), WeatherType.Snow);

            weatherRecords.Add(record1);
            weatherRecords.Add(record2);
            weatherRecords.Add(record3);

            IEnumerable<WeatherRecord> query = from record in weatherRecords where record.dateTime.Year == 2022 select record;

            foreach(WeatherRecord record in query)
            {
                Console.WriteLine(record.dateTime.ToString());
            }
        }*/
    }
}
