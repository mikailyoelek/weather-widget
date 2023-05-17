using Newtonsoft.Json;
using System.Collections.Generic;

namespace weather_widget.Model
{
    /// <summary>
    /// Response from OpenWeatherMap server
    /// This class repeats the structure of the resulting JSON file
    /// JSON is deserialized into an object of this type
    /// </summary>
    public class JSONResponce
    {
        /// <summary>
        /// List of objects - data for every 3 hours
        /// </summary>
        [JsonProperty("list")]
        internal List<JSONListItem> Items { get; set; }

        /// <summary>
        /// City data
        /// </summary>
        [JsonProperty("city")]
        internal JSONCity City { get; set; }
    }

    /// <summary>
    /// Weather data every 3 hours
    /// </summary>
    public class JSONListItem
    {
        [JsonProperty("main")]
        public JSONMainInfo MainInfo { get; set; }

        /// <summary>
        /// List consisting of weather types (usually 1 weather type)
        /// </summary>
        [JsonProperty("weather")]
        public List<JSONWeatherType> WeatherTypes { get; set; }

        /// <summary>
        /// List with wind types
        /// </summary>
        [JsonProperty("wind")]
        public JSONWeatherWindInfo WeatherWindInfo { get; set; }

        /// <summary>
        /// Date in string format
        /// </summary>
        [JsonProperty("dt_txt")]
        public string DateTime { get; set; }
    }

    /// <summary>
    /// Temperature data
    /// </summary>
    public class JSONMainInfo
    {
        /// <summary>
        /// Minimum temperature
        /// </summary>
        [JsonProperty("temp_min")]
        public double Temp_min { get; set; }

        /// <summary>
        /// Maximum temperature
        /// </summary>
        [JsonProperty("temp_max")]
        public double Temp_max { get; set; }

        /// <summary>
        /// humidity
        /// </summary>
        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }

    /// <summary>
    /// Weather type data
    /// </summary>
    public class JSONWeatherType
    {
        /// <summary>
        /// weather type
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// icon type
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

    }

    /// <summary>
    /// Wind  data
    /// </summary>
    public class JSONWeatherWindInfo
    {
        /// <summary>
        /// windspeed 
        /// </summary>
        [JsonProperty("speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Direction as number
        /// </summary>
        [JsonProperty("deg")]
        public double WindDirection { get; set; }

    }


    /// <summary>
    /// Information about the specified city
    /// </summary>
    public class JSONCity
    {
        /// <summary>
        /// City name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// country name zipped
        /// </summary>
        [JsonProperty("country")]
        public string CountryZip { get; set; }
    }
}
