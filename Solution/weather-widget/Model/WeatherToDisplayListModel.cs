using System;
using System.Collections.ObjectModel;

namespace weather_widget.Model
{
    public class WeatherToDisplayListModel : ObservableCollection<WeatherToDisplayModel>
    {
        public void FillTest()
        {
            for (int i = 0; i < 5; i++)
            {
                var weatherNew = new WeatherToDisplayModel("Cloudy", "02n.png", Convert.ToString(i + 10), Convert.ToString(i), Convert.ToString(22), Convert.ToString(i + 5),"NEWW", Convert.ToString(50), "NEWWW");
                this.Add(weatherNew);
            }
        }
    }
}
