using System;

namespace weather_widget.Model
{

    /// <Zusammenfassung>
    /// Weather status (each object should have weather data in 3 hour intervals)
    /// </summary>
    public class WeatherInfoModel
    {
        #region properties
        /// <summary>
        /// Description of current weather
        /// </summary>
        public string WeatherDescription { get; set; }

        /// <summary>
        /// Weathericon --> Weathericon e.g. 04d.png
        /// </summary>
        public string WeatherIcon { get; set; }


        /// <summary>
        /// Weatherday: Datetime
        /// </summary>
        public DateTime WeatherDayTime { get; set; }


        /// <summary>
        /// Max. Temperature in Celsius
        /// </summary>
        public double MaxTemperature { get; set; }


        /// <summary>
        /// Min. Temperature in Celsius
        /// </summary>
        public double MinTemperature { get; set; }


        /// <summary>
        /// Wind direction as a value
        /// </summary>
        public double WindDirection { get; set; }

        /// <summary>
        /// Wind direction as string (NN, ...)
        /// </summary>
        public string WindDirectionAsString { get; set; }


        /// <summary>
        /// Wind speed in m/s
        /// </summary>
        public double WindSpeed { get; set; }


        /// <summary>
        /// Humidity in %
        /// </summary>
        public double Humidity { get; set; }

        #endregion

        public WeatherInfoModel(string weatherdesc, string weathericon, DateTime weatherdaytime, double maxtemp, double mintemp, double winddir, string winddirasstring, double windspeed, double humidity)
        {
            WeatherDescription = weatherdesc;
            WeatherIcon = weathericon;
            WeatherDayTime = weatherdaytime;
            MaxTemperature = maxtemp;
            MinTemperature = mintemp;
            WindDirection = winddir;
            WindDirectionAsString = winddirasstring;
            WindSpeed = windspeed;
            Humidity = humidity;
        }
    }
}
