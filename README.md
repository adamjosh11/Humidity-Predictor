# Humidy-Predictor

My Humidity predictor has 4 main components to it. The Arduino, the API, the ML model, and the User Interface.

## Arduino

- Using the Arduino IDE I uploaded C code onto the Arduino to perform its functions
- Using the DHT-22 Sensor I collected the temperature and Humidity data
- I would pass the data over the wire the ESP-32 Microcontroller
- I uploaded code the ESP-32 that would connect it to the internet, and make a post request through my API

## API

- Created an API using .NET CORE
- Connects to my firebase database
- has controllers for climate data and prediction data
- Can make POST and GET requests for both of these datas

## Machine Learning Model

- Makes a GET request to receive all climate data stored by the Arduino/API
- Using PYTORCH, created a LSTM model that can predict the humidity of the next day
- Makes a POST request to store that prediction

## User Interface

- Created a UI using C# BLAZOR
- A user can request the latest temperature and humidity data stored in the DB. It requests from the DB instead of the Arduino in case the Arduino ever goes offline
- A user can request a prediction for the humidity tomorrow from the DB
