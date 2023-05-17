# MVVM class diagram



````mermaid
classDiagram
class ViewModelBase{
	Event PropertyChanged
	OnPropertyChanged()
}
class SettingsViewModel{
	Command BackToDashboardButton
	Command CloseButtonCommand
	string CurrentLocation
}
class MainViewModel{
	Navigationstore _navigationStore
	OnCurrentViewModelChanged()
	ViewModelBase CurrentViewModel
}
class DashBoardViewModel{
	string CurrentDate
	string CurrentDay
	string CurrentLocation
	string CurrentType
	WeatherInfoListModel ForeCastList
}
class DashBoardView
class SettingsView
class CommandBase{
	EventHandler CanExecuteChanged
	CanExecute(object)
	OnCanExecutedChanged()
	Execute(object)
}
class ExitApplicationCommand{
	Execute()
}
class UpdateCityCommand
class NavigateCommand{
	NavigationStore _navigationStore
	Func<ViewModelBase> _createViewModel
	NavigateCommand(NavigationStore Func<ViewModelBase> )
}
class MainWindowXaml{
	
}
class NavigationStore{
	ViewModelBase CurrentViewModel
	Action CurrentViewModelChanged
	OnCurrentViewModelChanged()
}
class AppXamlCs{
	NavigationStore _navStore;
	OnStartup(StartupEventArgs)
	CreateDashboardViewModel()
	CreateSettingsViewModel()
}

ViewModelBase ..> SettingsViewModel : inherit
ViewModelBase ..> DashBoardViewModel : inherit
ViewModelBase ..> MainViewModel : inherit
CommandBase ..> ExitApplicationCommand : inherit
CommandBase ..> NavigateCommand : inherit
NavigateCommand <|-- DashBoardViewModel
ExitApplicationCommand <|-- DashBoardViewModel
NavigateCommand <|-- SettingsViewModel
ExitApplicationCommand <|-- SettingsViewModel
UpdateCityCommand <|-- SettingsViewModel
NavigationStore <|-- AppXamlCs
NavigationStore <|-- MainWindowXaml
SettingsView <|-- MainWindowXaml
DashBoardView <|-- MainWindowXaml
MainViewModel <|-- MainWindowXaml
DashBoardViewModel *-- DashBoardView : binding
SettingsViewModel *-- SettingsView : binding
MainWindowXaml <|-- AppXamlCs
NavigationStore <|-- NavigateCommand

````

