using System.ComponentModel;

namespace weather_widget.ViewModel
{
    /// <summary>
    /// Basis which other viewmodels inherit from -> Easier to implement INotifyPropertyChanged 
    /// </summary>

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
