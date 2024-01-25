#include <WiFi.h>
#include <HTTPClient.h>
#include <esp_wifi.h>
#include <esp_http_client.h>

#define RXp2 16
#define TXp2 17

String ssid = "Adams-Family";
String password = "markadams";
String serverName = "https://capstone-api-exlxxpyksq-uc.a.run.app/ClimateData";
unsigned long lastTime = 0;
unsigned long timerDelay = 600000;
short reconnectTries = 0;


void setup() {
  Serial.begin(115200);
  //set up a second serial to listen for data over the wire
  Serial2.begin(9600, SERIAL_8N1, RXp2, TXp2);

  //connect to the internet
  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    Serial.println("Not Connected");
    delay(1000);
  }
  Serial.println("Connected");
}
void loop() {
  //read data over the wire
  String data = Serial2.readString();

    //check to ensure that the data that came over the wire was correct
    if(data.charAt(data.length() - 3) == '~'){
      //remove white space
      data.remove(data.length() - 1);
      data.remove(data.length() - 1);
      data.remove(data.length() - 1);

      //debugging printlns
      Serial.println("Data Received: ");
      Serial.println(data);

      //make sure I'm still connected to the wifi
      if(WiFi.status() == WL_CONNECTED) {
        //Make a POST request to my API
        HTTPClient http;
        http.begin(serverName.c_str());
        http.addHeader("Content-Type", "application/json");
        int httpResponseCode = http.POST(data);

        //debugging response codes
        if(httpResponseCode>0) {
          Serial.println("HTTP Response code: ");
          Serial.println(httpResponseCode);
          String payload = http.getString();
          Serial.println(payload);
        }
        else {
          Serial.println("Error code: ");
          Serial.println(httpResponseCode);
        }
        http.end();
      }
      else {
        //If I did disconnect from the wifi, attempt to reconnect every 10 minutes.
        //*Side Note* maybe try to only reconnect a couple of times before just shutting off the device
        Serial.println("WiFi Disconnected");
        WiFi.begin(ssid, password);
        delay(600000);
      }
    }
    else {
      Serial.print(data);
    }
}
