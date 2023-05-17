using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using weather_widget.Command;
using weather_widget.Model;
using weather_widget.Store;

namespace weather_widget.ViewModel
{
    internal class DashboardViewModel : ViewModelBase
    {
        #region fields
        private DataBaseUpdateManagerModel _updateMan;
        private WeatherToDisplayListModel _weatherList;
        #endregion

        #region ctor
        public DashboardViewModel(NavigationStore navigationStore, Func<SettingsViewModel> createSettingsViewModel, DataBaseUpdateManagerModel updateManager)
        {
            _updateMan = updateManager;
            _weatherList = updateManager.WeatherList;
            _weatherList.CollectionChanged += _weatherList_CollectionChanged;   //subscribe to collectionchanged event
            SettingsButtonCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
        }
        #endregion

        #region commands
        public ICommand SettingsButtonCommand { get; } //Command in order to switch to Settings-View
        public ICommand CloseButtonCommand { get; } //Command in order to terminate entire application
        #endregion

        #region methods
        // update all bindings
        public void WeatherPropertyChanged()
        {
            OnPropertyChanged(nameof(CurrentLocation));
            OnPropertyChanged(nameof(CurrentDate));
            OnPropertyChanged(nameof(CurrentDay));
            OnPropertyChanged(nameof(Humidity));
            OnPropertyChanged(nameof(MinTemp));
            OnPropertyChanged(nameof(MaxTemp));
            OnPropertyChanged(nameof(AvTemp));
            OnPropertyChanged(nameof(ForecastList));
            OnPropertyChanged(nameof(WeatherImageSource));
        }
        #endregion

        #region events
        private void _weatherList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            WeatherPropertyChanged();   //update bindings
        }
        #endregion

        #region properties  
        //bindings for view <-> viewmodel
        public string CurrentDate
        {
            get
            {
                return
                    (
                     DateTime.Now.Day + "/" +
                     DateTime.Now.Month + "/" +
                     DateTime.Now.Year
                    );
            }

        }
        public string CurrentDay { get => DateTime.Now.DayOfWeek.ToString(); }
        public string CurrentLocation
        {
            get => _updateMan.CurrentCity;
            set
            {
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }
        public string Humidity
        {
            get
            {
                return ForecastList.Count == 0 ? "X" : Convert.ToString(ForecastList[0]?.Humidity);
            }
        }
        public string MinTemp
        {
            get
            {
                return ForecastList.Count == 0 ? "X" : (ForecastList[0]?.MinTemperature);
            }
        }
        public string MaxTemp
        {
            get
            {
                return ForecastList.Count == 0 ? "X" : (ForecastList[0]?.MaxTemperature);
            }
        }
        public string AvTemp
        {
            get
            {
                return ForecastList.Count == 0 ? "X" : (ForecastList[0]?.AvgTemperature);
            }
        }
        public WeatherToDisplayListModel ForecastList { get => _weatherList; }
        public BitmapImage WeatherImageSource
        {
            get
            {
                if (ForecastList[0] == null)    
                {
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.EndInit();
                    return bi3;
                }
                else
                return ForecastList[0].WeatherImageSource;
            }
        }

        #endregion
    }
}
