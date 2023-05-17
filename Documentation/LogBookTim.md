# Bugs
- [x] 2nd MainWindow 

# ToDo
- [X] [GUI] Design ListView
- [X] [Database] DatabaseUpdateManger -> Update on 10:10, 11:10, 12:10, ... 
- [X] Onpropertychanged

# Log
## 13-04-2022 

- [MVVM] MainWindow
  - Setup Bindings (CurrentViewModel <-> CurrentView)
- [MVVM / GUI] Views
  - DashboardView: Display weather information
  - SettingsView: Edit location
- [MVVM] ViewModels
  - DashboardViewModel: Manage binding between View and API/Database
  - SettingsViewModel: Manage location setting for API
  - BaseViewModel: Base class to inherit from -> update on changed


## 16-04-2022

- [MVVM] Navigationstore
  - Implement current viewmodel
  - Method for changing viewmodel
- [MVVM] Commands
  - CommandBase: Base class to inherit from -> implements ICommand Interface
  - ExitApplicationCommand : Terminate application and calls any cancellationtokens
  - NavigateCommand : Navigate to choosen viewmodel


## 22-04-2022

- [MVVM] Starup (App.xaml.cs)+
  - Add NavigationStore
  - DashBoardViewModel on startup
  - Set up MainWindow
  - Methods for creating ViewModels
  
## 29-04-2022

- [GUI] DashboardView
  - Fix alligment (current weather)
  - implement listview for weather-forecast
  

## 30-04-2022

- [GUI] Allign lociation text (settings)
- [MVVM] Adding comments


## 05-05-2022

- [General] fixed duplicate window bug
- [GUI] replaced ListView with ListBox

## 06-05-2022

- [GUI] UI Overhaul
  - Removed Info, Wind, etc.
  - Added headers
  - Realligned items
  - Removed button background color on mouse hover

## 13-05-2022

- [API/Database] create update manager
  - update on startup
  - update every 3h (timer)
- [GUI / MVVM] update manager binding
  - pass updatemanager into ViewModels
  - bind settingsviewmodel
  - create command to call update method on confirm-button press

## 14-05-2022

- [API / Database] check connection to api before request

## 18-05-2022

- [Database] Database Update manager update every 3hours
- [GUI] fix image binding
- [API] imported api key
- [UI] Show "X" Value if List is empty

## 20-05-2022

- [API/Database] Update on HH:10 via Timer
- [MVVM] Automate binding update via event on weather collection changed

## 21-05-2022

- [UI] UI Overhaul
  - Backgroud color
  - Rearrange elements
  - Show icons in forecast
- [BUG] Bugfixing
  - fix showing icon
  - fix crash on HH:10
