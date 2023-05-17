using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace weather_widget.Model
{
    internal class APIManagerModel
    {
        private static string API_KEY = File.ReadAllText(@"..\\..\\..\\..\\..\\API.key"); //=> ".\weather-widget\API.key"
        //private static string API_KEY = "11"; //=> ".\weather-widget\API.key"

        public async Task<WeatherInfoListModel> GetWeather(string location)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={location}&mode=json&units=metric&appid={API_KEY}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    // No error occured (internet connection available & max request is not exceeded)
                    if (!(response.ToUpper().Contains(@"""MESSAGE"": ""YOUR ACCOUNT IS TEMPORARY""") && response.ToUpper().Contains(@"""COD"": 429")))
                    {
                        return GetWeatherInfos(response.ToString());
                    }
                }
                catch (HttpRequestException ex)
                {
                    if (ex.Message.ToUpper().Contains("429"))
                    {
                        throw new Exception("1: Max request reached!"); // Maximum reached
                    }
                    else if (ex.Message.ToUpper().Contains("401"))
                    {
                        throw new Exception("2: Invalid API-Key!"); // API-Key is wrong
                    }
                    else if (ex.Message.ToUpper().Contains("404"))
                    {
                        throw new Exception("4: Wrong city name / Invalid city name!"); // wrong city
                    }
                    else if (ex.Message.ToUpper().Contains("500") || ex.Message.ToUpper().Contains("502") || ex.Message.ToUpper().Contains("503") || ex.Message.ToUpper().Contains("504"))
                    {
                        throw new Exception("5: Unknown error code! Please contact openweather: https://home.openweathermap.org/questions."); // undefinied error code --> contact openweather 
                    }
                    else
                    {
                        throw new Exception("6: No internet connection! Please be sure that you have access to the internet"); // No Internet connection
                    }
                }
                catch (Exception)
                {
                    throw new Exception("7: Something went worng! Please try again!");
                }
                return null;
            }
        }

        private static WeatherInfoListModel GetWeatherInfos(string JSONContent)
        {
            JSONResponce jSONResponce = JsonConvert.DeserializeObject<JSONResponce>(JSONContent);
            Debug.WriteLine(JsonConvert.DeserializeObject<JSONResponce>(JSONContent).Items[0].WeatherWindInfo.WindDirection);

            // Weather states (each item 3 hours apart)
            WeatherInfoListModel weatherInfos = new WeatherInfoListModel();

            // Add WeatherInfoModel to WeatherInfoListModel
            foreach (var item in jSONResponce.Items)
            {
                weatherInfos.Add(ToWeatherInfoModel(item));
            }
            return weatherInfos;
        }

        /// <summary>
        /// Convert ListItem object to WeatherInfoModel object
        /// </summary>
        /// <param name="item">Weather data (for every 3 hours)</param>
        /// <returns>WeatherInfoModel</returns>
        private static WeatherInfoModel ToWeatherInfoModel(JSONListItem item)
        {
            return new WeatherInfoModel
            (
                weatherdesc: item.WeatherTypes[0].Description,
                weathericon: item.WeatherTypes[0].Icon + ".png",
                weatherdaytime: DateTime.Parse(item.DateTime), // content: string DateTime --> Parse to DateTime
                maxtemp: double.Parse(item.MainInfo.Temp_max.ToString()),
                mintemp: double.Parse(item.MainInfo.Temp_min.ToString()),
                winddir: double.Parse(item.WeatherWindInfo.WindDirection.ToString()),
                winddirasstring: WindDirConverter(double.Parse(item.WeatherWindInfo.WindDirection.ToString())),
                windspeed: double.Parse(item.WeatherWindInfo.WindSpeed.ToString()),
                humidity: double.Parse(item.MainInfo.Humidity.ToString())
            );
        }

        /// <summary>
        /// Converts the received value into a direction, which is understandable (as String)
        /// </summary>
        /// <param name="winddir"></param>
        /// <returns>String wind direction</returns>
        private static string WindDirConverter(double winddir)
        {
            double degree = winddir;

            int fixeddegree = (int)((degree / 22.5) + .5);
            string[] arr = new string[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            return arr[(fixeddegree % 16)];
        }
    }
}
