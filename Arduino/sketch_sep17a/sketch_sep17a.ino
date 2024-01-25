#include <DHT.h>
#define DHTPIN 2
#define DHTTYPE DHT22

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
  
}
void loop() {
  //Collect the climate data
  float temp = dht.readTemperature(true);
  float humid = dht.readHumidity();

  //Convert them to strings
  String tempString = String(temp);
  String humidString = String(humid);

  //create JSON request to send over the wire
  String data = ("{ \"Temperature\": " + tempString + ", \"Humidity\": " + humidString + "}~");
  
  //Send the request over the wire to the ESP32 
  Serial.println(data);
 
 //do this again every 15 minutes
  delay(900000);
}


