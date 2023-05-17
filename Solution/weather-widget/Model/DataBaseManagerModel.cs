using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace weather_widget.Model
{
    public class DataBaseManagerModel : INotifyPropertyChanged
    {
        #region fields
        public WeatherToDisplayListModel WeatherToDisplays { get; private set; }
        public string CityName; // TODO: get default value from 
        #endregion

        #region privateValues
        private WeatherInfoListModel weatherInfos;


        private string CountryZip;
        private int CityId;

        private const string MS = "m/s";
        private const string CELSIUS = "°C";
        private const string HUM = "%";
        private const string FilePath = @"..\\..\\..\\..\\..\\weatherwidget.db";
        #endregion privateValues
        public DataBaseManagerModel()
        {
            WeatherToDisplays = new WeatherToDisplayListModel();
            weatherInfos = new WeatherInfoListModel();

        }

        #region DownloadSaveData
        /// <summary>
        /// This method gets forecasts from openweather
        /// </summary>
        public void GetDataFromOpenWeather(string CityName)
        {
            try
            {
                GetWeather(CityName);
            }
            catch (Exception ex)
            {
                // Inform user that it failed
                Debug.WriteLine("[ERROR]: Getting weather failed: " + ex.Message);
            }
        }

        private async void GetWeather(string CityName)
        {
            APIManagerModel apimanagerModel = new APIManagerModel();
            Task<WeatherInfoListModel> TaskweatherInfos = apimanagerModel.GetWeather(CityName);

            weatherInfos = await TaskweatherInfos;

            this.CityName = CityName;

            // TODO: get id, countryzip

            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            SQLiteCommand command = connection.CreateCommand();

            command.CommandText = $"SELECT id, countryzip FROM citylist WHERE upper(cityname) = upper('{CityName}')";

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CityId = reader.GetInt32("id");
                CountryZip = reader.GetString("countryzip");
                break; // only one city
            }

            reader.Close();
            connection.Close();

            SaveIntoDatabase();
        }
        public void SaveIntoDatabase() // TO DO: CityId, CountryZip --> get them from DataBase and private safe
        {
            // .NET Framework
            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            // create command, which will communicate with DB
            SQLiteCommand cmd = new SQLiteCommand(connection);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS weatherinfo(
                              id INTEGER PRIMARY KEY, 
                              cityid INTEGER, cityname TEXT, countryzip TEXT, 
                              weatherdescription TEXT, weathericon TEXT, weatherdaytime TEXT, maxtemperature DOUBLE, mintemperature DOUBLE, winddirection DOUBLE, winddirectionasstring TEXT, windspeed DOUBLE, humidity DOUBLE,
                              FOREIGN KEY(cityid) REFERENCES citylist(id), FOREIGN KEY(cityname) REFERENCES citylist(cityname), FOREIGN KEY(countryzip) REFERENCES citylist(countryzip)
                              )";
            cmd.ExecuteNonQuery();

            foreach (WeatherInfoModel item in weatherInfos)
            {
                // CRUD ... Create Read Update

                // TO DO: Use DateTime from DataBaseUpdateManager!!
                // TO DO: CityName, CityId & co. should be prettier coded
                string cmdtext = "";
                if (CheckIfCurrentDataExist(item) == false)
                {
                    cmdtext = InsertIntoDataBase(item); // ->INSERT
                }
                else
                {
                    cmdtext = UpdateDataBase(item); // -> UPDATE 
                }

                cmd.CommandText = cmdtext;

                /*cmd.commandtext = $"insert into " +
                     $"weatherinfo(cityid, cityname, countryzip, weatherdescription, weathericon, weatherdaytime, maxtemperature, mintemperature, winddirection, winddirectionasstring, windspeed, humidity)" +
                     $"values({(cityid)},'{cityname}','{countryzip}','{item.weatherdescription}','{item.weathericon}','{item.weatherdaytime.tostring("yyyy-mm-dd hh:mm:ss")}','{item.maxtemperature.tostring(new cultureinfo("en-us"))}','{item.mintemperature.tostring(new cultureinfo("en-us"))}','{item.winddirection.tostring(new cultureinfo("en-us"))}','{item.winddirectionasstring}', '{item.windspeed.tostring(new cultureinfo("en-us"))}', '{item.humidity.tostring(new cultureinfo("en-us"))}')";
                */
                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }
        #endregion DownloadSaveData


        public void LoadFromDatabase(string CityName)
        {
            this.CityName = CityName;

            if (CityName == null || CityName == String.Empty)
            {
                return;
            }
            else
            {
                SQLiteConnection connectionf = CreateSQLiteConnection(FilePath);

                SQLiteCommand commandf = connectionf.CreateCommand();

                commandf.CommandText = $"SELECT id, countryzip FROM citylist WHERE upper(cityname) = upper('{CityName}')";

                SQLiteDataReader readerf = commandf.ExecuteReader();

                while (readerf.Read())
                {
                    CityId = readerf.GetInt32("id");
                    CountryZip = readerf.GetString("countryzip");
                    break; // only one city
                }

                readerf.Close();
                connectionf.Close();
            }

            WeatherToDisplays.Clear(); 

            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            SQLiteCommand command = connection.CreateCommand();

            List<DateTime> dtlist = new List<DateTime>();
            dtlist.Add(DateTime.Now);
            dtlist.Add(DateTime.Now.AddDays(1));
            dtlist.Add(DateTime.Now.AddDays(2));
            dtlist.Add(DateTime.Now.AddDays(3));
            dtlist.Add(DateTime.Now.AddDays(4));

            // CityName valid ? --> try/catch
            try
            {



                for (int i = 0; i < dtlist.Count; i++)
                {
                    // get Min, Max, AVG, winddir, humidity, weathericon, weatherdesc for each day
                    command.CommandText = $"SELECT t1.maxtemp, t1.mintemp, t1.averagetemp, t1.maxwind, t1.winddir, t1.humidity, t2.description, t2.icon, t2.frequency" +
                                          $" FROM (" +
                                                  $" SELECT" +
                                                  $" MAX(maxtemperature) as 'maxtemp'," +
                                                  $" MIN(mintemperature) as 'mintemp'," +
                                                  $" round((SUM(maxtemperature)+SUM(mintemperature))/(COUNT(maxtemperature)+COUNT(mintemperature)),2) as 'averagetemp'," +
                                                  $" MAX(windspeed) as 'maxwind'," +
                                                  $" winddirectionasstring as 'winddir'," +
                                                  $" MAX(humidity) as 'humidity'" +
                                                  $" FROM weatherinfo" +
                                                  $" WHERE weatherdaytime BETWEEN '{DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")} 00:00:00' AND '{DateTime.Now.AddDays(i + 1).ToString("yyyy-MM-dd")} 00:00:00' AND upper(cityname) LIKE '{CityName}'" +
                                                  $" ) as t1," +
                                                 $" (" +
                                                  $" SELECT weatherdescription as description, weathericon as icon, COUNT(weatherdescription) as frequency" +
                                                  $" FROM weatherinfo" +
                                                  $" WHERE weatherdaytime BETWEEN '{DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")} 00:00:00' AND '{DateTime.Now.AddDays(i + 1).ToString("yyyy-MM-dd")} 00:00:00' AND upper(cityname) LIKE '{CityName}'" +
                                                  $" GROUP BY weatherdescription" +
                                                  $" ORDER BY frequency DESC" +
                                                  $" LIMIT 1" +
                                                  $" ) as t2";


                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // save into the list
                        string icon = reader.GetString("icon");
                        if (DateTime.Now.TimeOfDay > Convert.ToDateTime("19:00:00").TimeOfDay)
                        {
                            try
                            {
                                icon = icon.Split('.')[0].Replace('d', 'n') + "." + icon.Split('.')[1]; // replace icon e.g. 10d.png 
                            }
                            catch (Exception)
                            {
                                continue; // continue if already contains e.g. 10n.png
                            }

                        }
                        else
                        {
                            try
                            {
                                icon = icon.Split('.')[0].Replace('n', 'd') + "." + icon.Split('.')[1]; // replace icon e.g. 10n.png 
                            }
                            catch (Exception)
                            {
                                continue; // continue if already contains e.g. 10d.png
                            }
                        }
                        string desc = reader.GetString("description");
                        string maxtemp = reader.GetDouble("maxtemp").ToString();
                        string mintemp = reader.GetDouble("mintemp").ToString();
                        string averagetemp = reader.GetDouble("averagetemp").ToString();
                        string winddir = reader.GetString("winddir");
                        string maxwind = reader.GetDouble("maxwind").ToString();
                        string humidity = reader.GetDouble("humidity").ToString();

                        WeatherToDisplayModel item = new WeatherToDisplayModel(desc, icon, maxtemp + CELSIUS, mintemp + CELSIUS,
                            averagetemp + CELSIUS, winddir, maxwind + MS, humidity + HUM, DateTime.Now.AddDays(i).ToString("ddd", new CultureInfo("en-EN")));
                        WeatherToDisplays.Add(item);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            catch (Exception)
            {
                // TODO: if table not exist, inform user no data available
                connection.Close();
                return;
            }
        }


        /// <summary>
        /// Gives you a list of string, depending on the letters of a city
        /// </summary>
        /// <param name="LettersForCityname">Letters of a city, that needs to be searched. Capital or small letters doesn't matter.</param>
        /// <returns>List of string</returns>
        public List<string> GetCitiesByLetters(string LettersForCityname)
        {
            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            SQLiteCommand cmd = connection.CreateCommand();

            cmd.CommandText = $"SELECT cityname FROM citylist " +
                                $"WHERE upper(cityname) LIKE '{LettersForCityname.ToUpper()}%'";

            SQLiteDataReader reader = cmd.ExecuteReader();
            List<string> cities = new List<string>();

            while (reader.Read())
            {
                cities.Add(reader.GetString("cityname"));
            }
            connection.Close();

            if (cities.Count > 0)
            {
                return cities;
            }
            else
            {
                cities.Add("Not Available! City doesn't exist in citylist!"); // NA... not available
                return cities;
            }
        }

        #region private HelperMethods
        private string InsertIntoDataBase(WeatherInfoModel weatherInfo)
        {
            string sqlitecmd = $"INSERT INTO " +
                    $"weatherinfo(cityid, cityname, countryzip, weatherdescription, weathericon, weatherdaytime, maxtemperature, mintemperature, winddirection, winddirectionasstring, windspeed, humidity)" +
                    $" VALUES({(CityId)},'{CityName}','{CountryZip}','{weatherInfo.WeatherDescription}','{weatherInfo.WeatherIcon}','{weatherInfo.WeatherDayTime.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    $" '{weatherInfo.MaxTemperature.ToString(new CultureInfo("en-US"))}','{weatherInfo.MinTemperature.ToString(new CultureInfo("en-US"))}','{weatherInfo.WindDirection.ToString(new CultureInfo("en-US"))}'," +
                    $" '{weatherInfo.WindDirectionAsString}', '{weatherInfo.WindSpeed.ToString(new CultureInfo("en-US"))}', '{weatherInfo.Humidity.ToString(new CultureInfo("en-US"))}')";

            return sqlitecmd;
        }

        private bool CheckIfCurrentDataExist(WeatherInfoModel weatherinfo)
        {
            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT weatherdaytime FROM weatherinfo " +
                                        $"WHERE weatherdaytime == '{weatherinfo.WeatherDayTime.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                        $"AND cityname == '{CityName}'";

            SQLiteDataReader reader = command.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                i++;
            }
            connection.Close();

            if (i > 0)
            {
                return true; // it exists
            }
            return false;
        }
        private string UpdateDataBase(WeatherInfoModel weatherInfo)
        {
            string sqlitecmd = sqlitecmd = $"UPDATE weatherinfo" +
                    $" SET weatherdescription = '{weatherInfo.WeatherDescription}'," +
                        $" weathericon = '{weatherInfo.WeatherIcon}'," +
                        $" maxtemperature = '{weatherInfo.MaxTemperature.ToString(new CultureInfo("en-US"))}'," +
                        $" mintemperature = '{weatherInfo.MinTemperature.ToString(new CultureInfo("en-US"))}'," +
                        $" winddirection = '{weatherInfo.WindDirection.ToString(new CultureInfo("en-US"))}'," +
                        $" winddirectionasstring = '{weatherInfo.WindDirectionAsString}', " +
                        $" windspeed = '{weatherInfo.WindSpeed.ToString(new CultureInfo("en-US"))}', " +
                        $" humidity = '{weatherInfo.Humidity.ToString(new CultureInfo("en-US"))}'" +
                    $" WHERE cityid = {(CityId)} AND cityname = '{CityName}' AND countryzip = '{CountryZip}' " +
                        $" AND weatherdaytime = '{weatherInfo.WeatherDayTime.ToString("yyyy-MM-dd HH:mm:ss")}'";

            return sqlitecmd;
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
        #endregion private HelperMethods

        #region propertychanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}