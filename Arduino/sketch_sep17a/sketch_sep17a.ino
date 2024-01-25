#include <DHT.h>
#define DHTPIN 2
#define DHTTYPE DHT22

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
  
}
void loop() {
  float temp = dht.readTemperature(true);
  float humid = dht.readHumidity();
  String tempString = String(temp);
  String humidString = String(humid);
  String data = ("{ \"Temperature\": " + tempString + ", \"Humidity\": " + humidString + "}~");
  Serial.println(data);
  //5min delay
  //900000
  delay(900000);
}



// // void setup() {
// //   // put your setup code here, to run once:
// //   Serial.begin(9600);
// // }


// // void loop() {
// //   // put your main code here, to run repeatedly:
// //   //int chk = dht.read11(DHTPIN);
// //   // Serial.print("Humidity = ");
// //   // Serial.print(humid);
// //   // Serial.println("~");

// //   delay(5000);
// // }

// // this sample code provided by www.programmingboss.com
// void setup() {
//   Serial.begin(9600);
// }
// void loop() {
//   Serial.println("Hello Boss");
//   delay(1500);
// }
