# Bugs

- [x] Binding problems (pictures)
- [x] Location changed bug --> List didn't update
- [ ] 1h update doesn't work properly
  - [ ] confirm button doesn't load data, if data is not available in db and needs to downloaded


# ToDo

- [x] Start with `Database-Manager`
  - [x] Create database for weather info
    - [x] Try to Create
  - [x] Save weather info into database
    - [x] change the format of datetime to `yyyy-MM-dd HH:mm:ss`
    - [x] change all `,` with `.` while saving, because DB is working with `.` !
  - [x] Helper Class, `WeatherToDisplay`
  - [x] try out SELECT, INSERT, UPDATE, ... commands
    - [x] get MAXIMUM from maxtemperature
    - [x] get MINIMUM temperature from mintemperature
    - [x] get AVERAGE temperature of a day
    - [x] get MOST FREQUENT weathertype `weatherdescription`
    - [x] delete duplicate cities (just leave the first one) `citylist`  
    - [x] Foreign Keys (citylist) (how to CREATE TABLE  ... FOREIGN KEY ... as ...`)
    - [x] how to read SELECTed content in C# `DataType` (see previous WFFST-Project)
    - [x] UPDATE if exists in DataBase
- [ ] Finish `API-Manager` (should be static, but for now its not a static class)
  - [x] Exceptions for different errors


# Log

## 08-04-2022 - Friday

- [Organizational] organising project folder/files/ ... on gitlab
- [Organizational] https://openweathermap.org/weather-conditions#Weather-Condition-Codes-2 ... Icons for different weather

## 13-04-2022 - Wednesday (Easter Holidays)
- [Python Script] Installed Pycharm -> Convert Cities (JSON-File) to SQLite-File -> Try to req.
- [Python Script] Converting into SQLite-File --> \2h

## 14-04-2022 - Thursday (Easter Holidays)
- [WeatherInfoModel] Model for weather information `weatherInfoModel `--> \1/2h
- [API-Manager] Start with `API-Manager` --> \1/2h

## 15-04-2022 - Friday (Easter Holidays --> nothing on this day)

## 22-04-2022 - Friday
- [JSONModel] JSON Converter --> https://github.com/kerminator-dev/WeatherWidget/blob/main/src/WeatherWidget/WeatherWidget/Models/JSON/OpenWeatherJSON.cs change code to my needs \2h
- [WeatherInfoListModel] Model for weather information `weatherInfoListModel`
- TO DO: Pflichtenheft, UI-Entwürfe, Aufteilung Arbeitspakete, Klassendiagramme, ER-Diagramme


till 02.05 --> per E-Mail
## 23-04-2022 - Saturday
- [API-Manager, JSONModel] Working on API-Manager, JSON-Converter

## 29-04-2022 - Friday
- [Organizational] finishing `Pflichtenheft.md`, documentation `LogBookMikail.md` --> \2h
- [WeatherInfoModel] update on `WeatherInfoModel` --> \1/4h (String for wind direction)

## 30-04-2022 - Saturday

- [Organisation] documentation: ER-Diagram, Class-Diagram --> \2h
- [API-Manager] work on exceptions for different errors --> \2h
  - Try and Catch
- [DataBaseManager] new Branch, new Class -->\1/4h
  - [DataBaseManager] Save into DataBase (try, not final) --> \1,5h

## 06-05-2022 - Friday

- [DataBaseManager]
  - first steps for CRUD (Remove, Insert, Update, ...) --> \2h

## 08-05-2022 - Saturday

- Merged DataBase (citylist and weatherinfo) together --> \ 1/2h

## 12-05-2022 - Thursday

- [DataBaseManager]
  - got MIN, MAX, AVERAGE, MOST FREQUENT weather--> \ 2 h

## 13-05-2022 - Friday

- ill (visiting the doctor) --> 1h
- [Organizational] asked a few questions about the project --> \1/2h
- [DataBase] deleted duplicate cities --> \1/2h
- [DataBaseManagerModel] changed FOREIGN KEYS --> \1/2h

##  14-05-2022 - Saturday

- new Classes [WeatherToDisplayListModel, WeatherToDisplayModel] --> 1/2h
  - is going to be binded by UI

- [DataBaseManagerModel] UPDATE and INSERT INTO --> \2 1/2h
  - Add new values and update present values
    - check if exits -> update
    - if not --> insert

  - Added new feature for receiving a list of cities, which starts with specific letters


## 20-05-2022 - Friday

- normal lesson --> \0h

## 21-05-2022 - Saturday

- [DataBaseManagerModel] Load,  Code improvements
  - Load data from DB --> \4h
  - prettied some code --> \1/2h
- [WeatherToDisplayList] save loaded data into this list --> \1/2h
- [WeatherToDisplay] added windspeed (forgotten to add it)
- [main] merged
- [Organizational] documentation --> \1/2h
- [Bugfix] Icons on UI, Location problems --> \1/2h

## 22-05-2022 - Sunday

- [Organizational] documentation -->\1/2h
  - [UPDATE] Class diagrams
  -  [UPDATE] ER diagram
