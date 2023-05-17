using System;
using System.Windows.Media.Imaging;

namespace weather_widget.Model
{
    public class WeatherToDisplayModel
    {
        #region properties
        /// <summary>
        /// Description of current weather
        /// </summary>
        public string WeatherDescription { get; set; }

        /// <summary>
        /// Weekday (abbreviation) e.g. Mon
        /// </summary>
        public string Weekday { get; set; }

        /// <summary>
        /// Weathericon --> Weathericon e.g. 04d.png
        /// </summary>
        public string WeatherIcon { get; set; }

        /// <summary>
        /// Max. Temperature in Celsius of a day
        /// </summary>
        public string MaxTemperature { get; set; }


        /// <summary>
        /// Min. Temperature in Celsius of a day
        /// </summary>
        public string MinTemperature { get; set; }


        /// <summary>
        /// Avg. Temperature in Celsius of a day
        /// </summary>
        public string AvgTemperature { get; set; }

        /// <summary>
        /// Max wind speed as string in m/s
        /// </summary>
        public string WindSpeed { get; set; }

        /// <summary>
        /// Wind direction as string (NN, ...), this value depends on max windspeed of a day
        /// </summary>
        public string Winddirection { get; set; }

        /// <summary>
        /// Humidity in %
        /// </summary>
        public string Humidity { get; set; }

        public BitmapImage WeatherImageSource
        {
            get
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();

                bi3.UriSource = new Uri(@"..\Resources\Icons\" + this.WeatherIcon, UriKind.RelativeOrAbsolute);
                bi3.EndInit();

                return bi3;
            }
        }

        #endregion properties

        public WeatherToDisplayModel(string weatherdesc, string weathericon, string maxtemperature, string mintemperature,
                string avgtemperature, string winddirection, string windspeed, string humidity, string weekday)
        {
            WeatherDescription = weatherdesc;
            WeatherIcon = weathericon;
            MaxTemperature = maxtemperature;
            MinTemperature = mintemperature;
            AvgTemperature = avgtemperature;
            Winddirection = winddirection;
            WindSpeed = windspeed;
            Humidity = humidity;
            Weekday = weekday;
        }

    }
}
