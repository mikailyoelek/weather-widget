using System;
using weather_widget.Store;
using weather_widget.ViewModel;

namespace weather_widget.Command
{
    /// <summary>
    /// Command in order to create and switch viewmodels
    /// </summary>
    internal class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            this._navigationStore = navigationStore;
            this._createViewModel = createViewModel;
        }


        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(); //run createVM method (MainWindow switches automatically)
        }
    }
}
