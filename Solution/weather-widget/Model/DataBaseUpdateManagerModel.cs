using System;
using System.Diagnostics;
using System.Timers;

namespace weather_widget.Model
{
    public class DataBaseUpdateManagerModel
    {
        #region fields
        private string _currentCity;
        private DataBaseManagerModel _manager;
        private System.Timers.Timer _threeHourTimer;
        // private SynchronizationContext _uiContext;
        #endregion

        #region ctor
        public DataBaseUpdateManagerModel()
        {

            _manager = new DataBaseManagerModel();
            CurrentCity = "Rankweil";   //current default value
            _manager.CityName = CurrentCity; // NEW: inform default city to databasemanager
            //_uiContext = SynchronizationContext.Current;
            UpdateWeather();      //uncommented unless api key is in repo
            SetTimerInitial();
            _manager.LoadFromDatabase(CurrentCity);

            //TESTING PURPOSE! : Fill list while api not working
            //FillListTest(); 
        }
        #endregion

        #region methods
        // call the update method from the Database Manager + pass current city
        public void UpdateWeather()
        {
            if (IsConnectionAvailable())
            {
                _manager.GetDataFromOpenWeather(CurrentCity);
                _manager.LoadFromDatabase(CurrentCity);
            }
            else
            {
                Debug.WriteLine("[ERROR]: No internet connection");
            }
        }

        // set timer on HH:10 updating weatherlist
        private void SetTimerInitial()
        {
            int minutesLeft;    //minutest left till (HH + 1) : 10
            if (DateTime.Now.Minute > 10)   // is HH:10 already in the past for current hour?
            {
                minutesLeft = 10 + 60 - DateTime.Now.Minute;    //... minutes left for NEXT hours :10
            }
            else
            {
                minutesLeft = 10 - DateTime.Now.Minute; //... minutes left for THIS hours :10
            }

            int millisecondsLeft = minutesLeft * 60 * 1000 + 1; // + 1 ms if current time is exactly HH:10 

            _threeHourTimer = new System.Timers.Timer(millisecondsLeft);    // Create a timer 
            _threeHourTimer.Elapsed += OnTimedEvent;    // Hook up the Elapsed event for the timer. 
            _threeHourTimer.AutoReset = true;
            _threeHourTimer.Enabled = true;
        }

        // set timer for 1h updating weatherlist
        private void SetTimerContinious()
        {
            _threeHourTimer = new System.Timers.Timer(3600000);    // Create a timer with a 1h interval. 
            _threeHourTimer.Elapsed += OnTimedEvent;    // Hook up the Elapsed event for the timer. 
            _threeHourTimer.AutoReset = true;
            _threeHourTimer.Enabled = true;
        }

        //check if internet connection is available
        private bool IsConnectionAvailable()
        {
            try
            {   //check if domain name is resolvable
                System.Net.IPHostEntry ipHe = System.Net.Dns.GetHostEntry("www.openweathermap.org");
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region testing purpose
        private void FillListTest()
        {
            for (int i = 0; i < 5; i++)
            {
                var weatherNew = new WeatherToDisplayModel("Cloudy", "02n.png", Convert.ToString(i + 10), Convert.ToString(i), Convert.ToString(22), Convert.ToString(i + 5), "NEWW", Convert.ToString(50), "NEWWW");
                WeatherList.Add(weatherNew);
            }
        }
        #endregion

        #endregion

        #region events
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //_uiContext.Send(x => UpdateWeather(),null);
            //_uiContext.Send(x => SetTimerContinious(),null);
            SetTimerContinious();
            UpdateWeather();
        }
        #endregion

        #region properties
        public string CurrentCity
        {
            get => _currentCity;
            set => _currentCity = value;
        }
        public WeatherToDisplayListModel WeatherList { get => _manager.WeatherToDisplays; }
        #endregion 
    }
}
