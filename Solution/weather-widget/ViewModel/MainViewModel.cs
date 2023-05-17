using weather_widget.Store;

namespace weather_widget.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        #region fields
        private readonly NavigationStore _navigationStore;
        #endregion

        #region ctor
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        #endregion

        #region events
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        #endregion

        #region properties
        public ViewModelBase CurrentViewModel { get => _navigationStore.CurrentViewModel; } //currently used viewmodel -> Mainwindow chooses view depending on VM
        #endregion

    }
}
