using System;
using System.Windows.Input;
using weather_widget.Command;
using weather_widget.Model;
using weather_widget.Store;

namespace weather_widget.ViewModel
{
    internal class SettingsViewModel : ViewModelBase
    {
        #region fields
        private DataBaseUpdateManagerModel _updateMan;
        #endregion

        #region ctor
        public SettingsViewModel(NavigationStore navigationStore, Func<DashboardViewModel> createDashboardViewModel, DataBaseUpdateManagerModel updateManager)
        {
            _updateMan = updateManager;
            BackToDashboardButtonCommand = new NavigateCommand(navigationStore, createDashboardViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
            ConfirmButtonCommand = new UpdateCityCommand(_updateMan);
        }
        #endregion

        #region commands
        public ICommand BackToDashboardButtonCommand { get; }    //Command in order to switch to Dashboard-View
        public ICommand CloseButtonCommand { get; } //command in order to close window
        public ICommand ConfirmButtonCommand { get; } //confirm choosen city -> update 
        #endregion

        #region properties
        public string CurrentLocation
        {
            get => _updateMan.CurrentCity;
            set => _updateMan.CurrentCity = value;
        }    //Binding for View -> gets/sets location for weather api
        #endregion
    }
}
