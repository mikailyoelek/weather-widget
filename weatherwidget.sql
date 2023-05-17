-- following lines was used to create new table without duplicates
-- SELECT id, cityname, countryzip FROM citylist 
-- 	GROUP BY cityname 
-- 	ORDER BY cityname ASC;
-- 
-- -- create new citytable without duplicates
-- CREATE TABLE citylistNEW AS
-- 	SELECT id, cityname, countryzip FROM citylist 
-- 	GROUP BY cityname 
-- 	ORDER BY cityname ASC;
-- 
-- -- drop old citylist table	
-- DROP TABLE citylist;
-- 
-- -- create the new table as citylist
-- CREATE TABLE citylist AS
-- 	SELECT id, cityname, countryzip FROM citylistNEW 
-- 	GROUP BY cityname 
-- 	ORDER BY cityname ASC;
-- 	
-- --drop citylistNEW TABLE
-- DROP TABLE citylistNEW;


--LIKE "%" GROUP BY cityname;

SELECT id, countryzip FROM citylist	WHERE upper(cityname) = upper('Rankweil');



SELECT t1.maxtemp, t1. mintemp, t1.averagetemp, t1.maxwind, t1.winddir
FROM (
	  SELECT 
	  MAX(maxtemperature) as 'maxtemp', 
	  MIN(mintemperature) as 'mintemp', 
	  round((SUM(maxtemperature)+SUM(mintemperature))/(COUNT(maxtemperature)+COUNT(mintemperature)),2) as 'averagetemp', 
	  MAX(windspeed) as 'maxwind', 
	  winddirectionasstring as 'winddir'
	  FROM weatherinfo
	  WHERE weatherdaytime BETWEEN '2022-05-21 00:00:00' AND '2022-05-22 00:00:00' AND upper(cityname) LIKE 'RANKWEIL'
	  ) as t1;	



-- C# query
SELECT t1.maxtemp, t1. mintemp, t1.averagetemp, t1.maxwind, t1.winddir, t2.description, t2.icon, t2.frequency
FROM (
	  SELECT 
	  MAX(maxtemperature) as 'maxtemp', 
	  MIN(mintemperature) as 'mintemp', 
	  round((SUM(maxtemperature)+SUM(mintemperature))/(COUNT(maxtemperature)+COUNT(mintemperature)),2) as 'averagetemp', 
	  MAX(windspeed) as 'maxwind', 
	  winddirectionasstring as 'winddir'
	  FROM weatherinfo
	  WHERE weatherdaytime BETWEEN '2022-05-21 00:00:00' AND '2022-05-22 00:00:00' AND upper(cityname) LIKE 'RANKWEIL'
	  ) as t1,	  
	 (
	  SELECT weatherdescription as description, weathericon as icon, COUNT(weatherdescription) as frequency
	  FROM weatherinfo
	  WHERE weatherdaytime BETWEEN '2022-05-21 00:00:00' AND '2022-05-22 00:00:00' AND upper(cityname) LIKE 'RANKWEIL'
	  GROUP BY weatherdescription 
	  ORDER BY frequency DESC
	  LIMIT 1
	  ) as t2;  

-- Maximale Temperatur:
SELECT MAX(maxtemperature) as maxtemp FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- Minimale Temperatur:
SELECT MIN(mintemperature) as mintemp FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- mittlere Temperatur:
SELECT
	(SUM(maxtemperature)+SUM(mintemperature))/
	(COUNT(maxtemperature)+COUNT(mintemperature)) as average FROM weatherinfo 
	WHERE weatherdaytime BETWEEN '2022-05-12 00:00:00' AND '2022-05-13 00:00:00';
	
-- Max, Min, Mittlere Temperatur
SELECT
	MAX(maxtemperature) as maxtemp,
	MIN(mintemperature) as mintemp ,
	(SUM(maxtemperature)+SUM(mintemperature))/
	(COUNT(maxtemperature)+COUNT(mintemperature)) as average
	FROM weatherinfo 
	WHERE weatherdaytime BETWEEN '2022-05-12 00:00:00' AND '2022-05-13 00:00:00';
	
-- Maximale Windgeschwindigkeit + Windrichtung
SELECT MAX(windspeed) as maxwind, winddirectionasstring as winddir FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- Minimale Windgeschwindigkeit + Windrichtung
SELECT MIN(windspeed) as minwind, winddirectionasstring as winddir FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- Häufigste Wetterart
SELECT weatherdescription, COUNT(weatherdescription) as frequency
	FROM weatherinfo 
	WHERE weatherdaytime BETWEEN '2022-05-15 00:00:00' AND '2022-05-15 12:00:00'
	GROUP BY weatherdescription
	LIMIT 1;
	
-- Häufigste Wetterarten (hier 2)
SELECT weatherdescription, COUNT(weatherdescription) AS frequency
	FROM weatherinfo
	WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00'
	GROUP BY weatherdescription
	ORDER BY frequency DESC
	LIMIT 2;

-- following query demonstrates the query for search citynames
SELECT cityname FROM citylist 
	WHERE upper(cityname) LIKE 'RA%';
	
SELECT weatherdaytime FROM weatherinfo
	WHERE weatherdaytime == '2022-05-13 08:00:00';
	
	UPDATE weatherinfoSET weatherdescription = 'light rain',weathericon = '10d.png',maxtemperature = '22.62',mintemperature = '18.37',winddirection = '7',winddirectionasstring = 'N', windspeed = '0.85', humidity = '58')WHERE cityid = 2767974 AND cityname = 'Rankweil' AND countryzip = 'AT' AND weatherdaytime = '2022-05-14 18:00:00