#include "pitches.h"
#include <WiFi.h>
#include <HTTPClient.h>

//ultrasonic sensor
#define echoUpper 26  // attach pin D2 Arduino to pin Echo of HC-SR04
#define trigUpper 27  //attach pin D3 Arduino to pin Trig of HC-SR04
#define echoLeg 14    // attach pin D2 Arduino to pin Echo of HC-SR04
#define trigLeg 12    //attach pin D3 Arduino to pin Trig of HC-SR04

#define pressure1 33
#define pressure2 25
#define pressure3 32

#define CLK 19
#define DT 5
#define SW 18

#define buzzer 15  // The Arduino pin connected to the buzzer

int melody[] = {
  NOTE_E5, NOTE_E5, NOTE_E5,
  NOTE_E5, NOTE_E5, NOTE_E5,
  NOTE_E5, NOTE_G5, NOTE_C5, NOTE_D5,
  NOTE_E5,
  NOTE_F5, NOTE_F5, NOTE_F5, NOTE_F5,
  NOTE_F5, NOTE_E5, NOTE_E5, NOTE_E5, NOTE_E5,
  NOTE_E5, NOTE_D5, NOTE_D5, NOTE_E5,
  NOTE_D5, NOTE_G5
};

int noteDurations[] = {
  8, 8, 4,
  8, 8, 4,
  8, 8, 8, 8,
  2,
  8, 8, 8, 8,
  8, 8, 8, 16, 16,
  8, 8, 8, 8,
  4, 4
};

int counter = 0;
int currentStateCLK;
int lastStateCLK;
String currentDir = "";
unsigned long lastButtonPress = 0;

long durationUpper;
float distanceUpper;

long durationLeg;
float distanceLeg;

float legAverage;
float backAverage;

byte press1state;
byte press2state;
byte press3state;
bool p1 = false;
bool p2 = false;
bool p3 = false;

void calculateUpperDistance();
void calculateLegDistance();
void calculatePressure();
void rotaryEncoder();
void calculateDistances();
const char* ssid = "spinewise";
const char* password = "spinewise1";
const char* server = "https://backend.spinewise.p2361.app.fit.ba/sensordata/log";
String key = "";
int chairId = 8;
void setup() {
  //ultrasonic
  pinMode(trigUpper, OUTPUT);  // Sets the trigPin as an OUTPUT
  pinMode(echoUpper, INPUT);   // Sets the echoPin as an INPUT
  pinMode(trigLeg, OUTPUT);    // Sets the trigPin as an OUTPUT
  pinMode(echoLeg, INPUT);     // Sets the echoPin as an INPUT

  //pressure buttons
  pinMode(pressure1, INPUT_PULLUP);
  pinMode(pressure2, INPUT_PULLUP);
  pinMode(pressure3, INPUT_PULLUP);


  //rotary
  pinMode(CLK, INPUT);
  pinMode(DT, INPUT);
  pinMode(SW, INPUT_PULLUP);
  lastStateCLK = digitalRead(CLK);

  //buzzer
  // iterate over the notes of the melody:
  // int size = sizeof(noteDurations) / sizeof(int);

  // for (int thisNote = 0; thisNote < size; thisNote++) {

  //   // to calculate the note duration, take one second divided by the note type.
  //   //e.g. quarter note = 1000 / 4, eighth note = 1000/8, etc.
  //   int noteDuration = 1000 / noteDurations[thisNote];
  //   tone(BUZZER_PIN, melody[thisNote], noteDuration);

  //   // to distinguish the notes, set a minimum time between them.
  //   // the note's duration + 30% seems to work well:
  //   int pauseBetweenNotes = noteDuration * 1.30;
  //   delay(pauseBetweenNotes);
  //   // stop the tone playing:
  //   noTone(BUZZER_PIN);
  // }


  //wifi
  WiFi.mode(WIFI_STA);  //Optional
  WiFi.begin(ssid, password);
  Serial.println("Connecting to WIFI…");

  int counter = 0;

  while (WiFi.status() != WL_CONNECTED) {
    counter++;
    delay(250);
    Serial.print(".");
    if (counter > 50) {
      Serial.println("Restartuje se esp!");
      ESP.restart();
    }
  }
  Serial.println("Uspjesno povezan");
  tone(buzzer, 0);  // Send 1KHz sound signal...
  delay(100);
  noTone(buzzer);  // Stop sound...
  delay(20);
  Serial.begin(115200);  // // Serial Communication is starting with 9600 of baudrate speed
}

void loop() {
  // put your main code here, to run repeatedly:
  // calculateUpperDistance();
  // calculateLegDistance();
  // calculatePressure();
  // Serial.println("Leda: ");
  // Serial.print(distanceUpper);
  // Serial.println("Noge: ");
  // Serial.print(distanceLeg);

  //rotaryEncoder();
  tone(buzzer, 0);  // Send 1KHz sound signal...
  delay(100);
  noTone(buzzer);  // Stop sound...
  delay(50);
  calculateDistances();

  delay(10);
}

void calculateDistances() {
  float legSum = 0;
  float backSum = 0;
  for (int i = 0; i < 20; i++) {
    calculateUpperDistance();
    calculateLegDistance();

    legSum += distanceLeg;
    backSum += distanceUpper;

    rotaryEncoder();
    delay(500);
  }
  float dvadeset = 20;
  legAverage = legSum / dvadeset;
  backAverage = backSum / dvadeset;

  Serial.println("Noge average: ");
  Serial.print(legAverage);

  Serial.println("Leda average: ");
  Serial.print(backAverage);

  calculatePressure();
  String jedan = "false";
  String dva = "false";
  String tri = "false";
  if (p1 == true) {
    jedan = "true";
  }
  if (p2 == true) {
    dva = "true";
  }
  if (p3 == true) {
    tri = "true";
  }
  HTTPClient http;
  http.begin(server);

  //http.addHeader("accept", "text/plain");
  http.addHeader("Content-Type", "application/json");
  String jsonString = "{";
  jsonString += "\"upperBackDistance\":" + String(backAverage, 2) + ",";
  jsonString += "\"legDistance\":" + String(legAverage, 2) + ",";
  jsonString += "\"pressureSensor1\":" + jedan + ",";
  jsonString += "\"pressureSensor2\":" + dva + ",";
  jsonString += "\"pressureSensor3\":" + tri + ",";
  jsonString += "\"chairId\":" + String(chairId) + ",";
  jsonString += "\"key\":\"" + key + "\"";
  jsonString += "}";
  //Serial.println(jsonString);

  int httpResponseCode = http.POST(jsonString);

  if (httpResponseCode > 0) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
    String payload = http.getString();
    Serial.println(payload);
  } else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }

  http.end();
}



void calculateUpperDistance() {
  digitalWrite(trigUpper, LOW);
  delay(2);

  digitalWrite(trigUpper, HIGH);
  delay(10);

  digitalWrite(trigUpper, LOW);
  durationUpper = pulseIn(echoUpper, HIGH);
  distanceUpper = durationUpper * 0.034 / 2;
}

void calculateLegDistance() {
  digitalWrite(trigLeg, LOW);
  delay(2);

  digitalWrite(trigLeg, HIGH);
  delay(10);

  digitalWrite(trigLeg, LOW);
  durationLeg = pulseIn(echoLeg, HIGH);
  distanceLeg = durationLeg * 0.034 / 2;
}



void calculatePressure() {
  p1 = false;
  p2 = false;
  p3 = false;
  press1state = digitalRead(pressure1);
  press2state = digitalRead(pressure2);
  press3state = digitalRead(pressure3);

  if (press1state == LOW) {
    Serial.println("pressure1 je kliknut");
    p1 = true;
  } else {
    Serial.println("pressure1 nije kliknut");
  }
  if (press2state == LOW) {
    Serial.println("pressure2 je kliknut");
    p2 = true;
  } else {
    Serial.println("pressure2 nije kliknut");
  }

  if (press3state == LOW) {
    Serial.println("pressure3 je kliknut");
    p3 = true;
  } else {
    Serial.println("pressure3 nije kliknut");
  }
}

void rotaryEncoder() {
  // Read the current state of CLK
  currentStateCLK = digitalRead(CLK);

  // If last and current state of CLK are different, then pulse occurred
  // React to only 1 state change to avoid double count
  if (currentStateCLK != lastStateCLK && currentStateCLK == 1) {

    // If the DT state is different than the CLK state then
    // the encoder is rotating CCW so decrement
    if (digitalRead(DT) != currentStateCLK) {
      counter--;
      currentDir = "CCW";
    } else {
      // Encoder is rotating CW so increment
      counter++;
      currentDir = "CW";
    }

    Serial.print("Direction: ");
    Serial.print(currentDir);
    Serial.print(" | Counter: ");
    Serial.println(counter);
  }

  // Remember last CLK state
  lastStateCLK = currentStateCLK;

  // Read the button state
  int btnState = digitalRead(SW);

  //If we detect LOW signal, button is pressed
  if (btnState == LOW) {
    //if 50ms have passed since last LOW pulse, it means that the
    //button has been pressed, released and pressed again
    if (millis() - lastButtonPress > 50) {
      Serial.println("Button pressed!");
      tone(buzzer, 1000);  // Send 1KHz sound signal...
      delay(100);          // ...for 1 sec
    }
    noTone(buzzer);  // Stop sound...
    delay(10);
    // Remember last button press event
    lastButtonPress = millis();
  }

  // Put in a slight delay to help debounce the reading
  delay(1);
}