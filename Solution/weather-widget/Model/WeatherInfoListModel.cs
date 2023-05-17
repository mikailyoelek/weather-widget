using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace weather_widget.Model
{
    /// <summary>
    /// Weather data for each day, 3h forecasts
    /// </summary>
    public class WeatherInfoListModel : List<WeatherInfoModel>
    {
        /// <summary>
        /// WeatherInfoListModel constructor
        /// </summary>
        public WeatherInfoListModel()
        {

        }


        public void SaveToSqlite(string fileName)
        {
            /*
            // Info zu SQLite:
            // https://SQLite.org
           
            // Install-Package system.data.sqlite

            //.NET Core (using Microsoft.Data.SQLite)
            SqliteConnection conn = CreateSQLiteConnection(fileName);

            // Kommando erzeugen, das mit der DB kommuniziert
            SqliteCommand cmd = new SqliteCommand("DROP TABLE IF EXISTS adressdata", conn);
            cmd.ExecuteNonQuery();

            
            // .NET Framework
            SQLiteConnection connection = CreateSQLiteConnection(fileName);

            // create command, which will communicate with DB
            SQLiteCommand cmd = new SQLiteCommand(connection);

            cmd.CommandText = "DROP TABLE IF EXISTS weatherinfo";

            cmd.ExecuteNonQuery();

            // TODO: cityid, cityname, coutryzip --> FOREIGN KEYS
            cmd.CommandText = @"CREATE TABLE weatherinfo(id INTEGER PRIMARY KEY,
                              cityid INTEGER, cityname TEXT, countryzip TEXT,
                              weatherdescription TEXT, weathericon TEXT, weatherdaytime TEXT, maxtemperature DOUBLE, 
                              mintemperature DOUBLE, winddirection DOUBLE, winddirectionasstring TEXT, windspeed DOUBLE, humidity DOUBLE)";
            cmd.ExecuteNonQuery();

            foreach (WeatherInfoModel item in this)
            {
                // CRUD ... Create Read Update Delete

                // neue Datensätze -> INSERT ("create")
                cmd.CommandText = $"INSERT INTO " +
                    $"weatherinfo(cityid, cityname, countryzip, weatherdescription, weathericon, weatherdaytime, maxtemperature, mintemperature, winddirection, winddirectionasstring, windspeed, humidity)" +
                    $"VALUES({(cityid)},'{cityname}','{countryzip}','{item.WeatherDescription}','{item.WeatherIcon}','{item.WeatherDayTime.ToString()}','{item.MaxTemperature}','{item.MinTemperature}','{item.WindDirection}','{item.WindDirectionAsString}', '{item.WindSpeed}', '{item.Humidity}')";
                cmd.ExecuteNonQuery();

                // geänderte Datensätze -> UPDATE 

                // gelöschte Datensätze -> DELETE

            }

            connection.Close();
            */
        }
        private SQLiteConnection CreateSQLiteConnection(string fileName)
        {
            SQLiteConnection conn = new SQLiteConnection($"Data Source={fileName}");
            try
            {
                conn.Open();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return conn;
        }



        /// <summary>
        /// Get the average temperature for the day
        /// </summary>
        /// <returns>Average temperature for the day</returns>
        public double GetAVGTemp()
        {
            return (GetMinTemperature() + GetMaxTemperature()) / 2;
        }

        /// <summary>
        /// Get the minimum temperature for the day
        /// </summary>
        /// <returns>Minimum temperature for the day</returns>
        public double GetMinTemperature(bool round = true)
        {
            double result = double.Parse(this[0].MinTemperature.ToString());

            foreach (WeatherInfoModel weather in this)
            {
                if (result > weather.MinTemperature)
                {
                    result = weather.MinTemperature;
                }
            }

            return round ? Math.Round(result) : result;
        }

        /// <summary>
        /// Get the maximum temperature for the day
        /// </summary>
        /// <returns>Minimum temperature for the day</returns>
        public double GetMaxTemperature(bool round = true)
        {
            double result = double.Parse(this[0].MinTemperature.ToString());

            foreach (WeatherInfoModel weather in this)
            {
                if (result < weather.MinTemperature)
                {
                    result = weather.MinTemperature;
                }
            }

            return round ? Math.Round(result) : result;
        }

        /// <summary>
        /// Get the date
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDate()
        {
            return new DateTime(this[0].WeatherDayTime.Year, this[0].WeatherDayTime.Month, this[0].WeatherDayTime.Day);
        }


        /// <summary>
        /// Gives you the highest or lowest windspeed for the day
        /// </summary>
        /// <param name="highest">True for highest windspeed of that day</param>
        /// <returns>double WindSpeed (default: highest = true) in m/s</returns>
        public double GetWindSpeed(bool highest = true)
        {
            double result = double.Parse(this[0].WindSpeed.ToString());

            foreach (WeatherInfoModel weather in this)
            {
                if (highest)
                {
                    if (result < weather.WindSpeed)
                    {
                        result = weather.WindSpeed;
                    }
                }
                else
                {

                    if (result > weather.WindSpeed)
                    {
                        result = weather.WindSpeed;
                    }
                }

            }

            return result;
        }

        // TO DO:
        /*
        public string GetWindDirection()
        {
            
        }
        */



        /// <summary>
        /// Get the frequently recurring weather type for the day
        /// </summary>
        /// <returns></returns>
        /*
        public string GetFrequentDescription()
        {
            var uniqueWeatherStatess = EveryDayWeatherStates.OrderByDescending(i => i.WeatherDescription).Distinct(j => j.WeatherDescription).ToList();

            if (uniqueWeatherStatess.Count > 0)
            {
                return $"{uniqueWeatherStatess[0].Weather}";
            }
            else return "no weather data";

        }
        */

    }

}
