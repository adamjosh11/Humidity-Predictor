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
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial2.begin(9600, SERIAL_8N1, RXp2, TXp2);
  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    Serial.println("Not Connected");
    delay(1000);
  }
  Serial.println("Connected");
}
void loop() {
  String data = Serial2.readString();
  // Serial.println(data.charAt(data.length() - 3));
    if(data.charAt(data.length() - 3) == '~'){
      data.remove(data.length() - 1);
      data.remove(data.length() - 1);
      data.remove(data.length() - 1);
      Serial.println("Data Received: ");
      Serial.println(data);
      if(WiFi.status() == WL_CONNECTED) {
        HTTPClient http;
        http.begin(serverName.c_str());
        http.addHeader("Content-Type", "application/json");
        int httpResponseCode = http.POST(data);

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
        Serial.println("WiFi Disconnected");
        WiFi.begin(ssid, password);
        delay(600000);
      }
    }
    else {
      Serial.print(data);
    }
}

// // this sample code provided by www.programmingboss.com
// #define RXp2 16
// #define TXp2 17
// void setup() {
//   // put your setup code here, to run once:
//   Serial.begin(115200);
//   Serial2.begin(9600, SERIAL_8N1, RXp2, TXp2);
// }
// void loop() {
//     Serial.println("Data Received: ");
//     Serial.println(Serial2.readString());
// }


// // void setup() {
// //   // put your setup code here, to run once:
// //   Serial.begin(115200);
// // }

// // void loop() {
// //   // put your main code here, to run repeatedly:
// //   // Serial.print("testing 123");
// //     // Serial.println("Serial Available");
// //     String data = Serial2.readStringUntil('\n');
// //     Serial.print("Data Read");
// //     Serial.print(data);
// //     Serial.println("Entering Process Data Method");
// //     processData(data);
// //   // else {
// //   //   Serial.println("Serial not available");
// //   // }
// //   delay(1000);
// // }

// // void processData(String data) {
  
// //   // Split the data into temperature and humidity
// //   float temperature = getValue(data, "Temperature:");
// //   float humidity = getValue(data, "Humidity:");

// //   // Do something with the temperature and humidity data
// //   // For now, just print it
// //   Serial.print("Received Temperature: ");
// //   Serial.print(temperature);
// //   Serial.print("Humidity: ");
// //   Serial.print(humidity);
// //   Serial.println("%");
// // }

// // float getValue(String data, String key) {
// //   int start = data.indexOf(key) + key.length() + 1;  // Skip the space after the colon
// //   int end = data.indexOf("~", start);  // Assume the value ends with '°'

// //   if (start >= 0 && end >= 0) {
// //     String valueStr = data.substring(start, end);
// //     return valueStr.toFloat();
// //   }

// //   return 0.0;  // Return 0 if the value extraction fails
// // }