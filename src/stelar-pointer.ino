#include <Servo.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

#define SERVO_A_PIN 10
#define SERVO_B_PIN 11
#define SERVO_C_PIN 12

#define SERVO_A_MIN 10
#define SERVO_A_MAX 10
#define SERVO_B_MIN 10
#define SERVO_B_MAX 10
#define SERVO_C_MIN 10
#define SERVO_C_MAX 10

#define MOTOR_STEPS 384 // nr of steps for a full turn
#define MAX_MOTOR_STEPS 360
#define RPM 120

// define pins
#define SLEEP 7
#define DIR 8
#define STEP 9

#define IR_PIN A7 //same pin pos: 6-digitla / A7-analog

Servo servoA, servoB, servoC;

int servoAPos;
int servoBPos;
int servoCPos;
int steps;
int irValue;

void setup() {
  pinMode(IR_PIN,INPUT);
  
  servoA.attach(SERVO_A_PIN);
  servoB.attach(SERVO_B_PIN);
  servoC.attach(SERVO_C_PIN);

  pinMode(SLEEP, OUTPUT);  // SLEEP
  pinMode(DIR, OUTPUT);  // DIR
  pinMode(STEP, OUTPUT);  // STEP
  
  digitalWrite(SLEEP, HIGH);
  digitalWrite(DIR, HIGH);
  digitalWrite(STEP, LOW);

  resetPositions();

  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0) {
    String sentCommand = Serial.readString();
    sentCommand.trim();
    
    if (isSentCommandValid(sentCommand)) {
      resetPositions();
      char commands[sentCommand.length() + 1];
      sentCommand.toCharArray(commands, sentCommand.length());
      
      char *command = strtok(commands, "<;>");
      while (command != NULL) {
        String message = command;
        handleMotors(command);
        
        command = strtok(NULL, "<;>");
      }
    }
  }
}

void moveOneStep() {
  digitalWrite(STEP, HIGH);
  delay(10);          
  digitalWrite(STEP, LOW); 
  delay(10);
}

void moveStepper(int stepperLength) {
  steps = stepperLength;
  if (stepperLength <= MAX_MOTOR_STEPS) {
    while (stepperLength != 0) {
      moveOneStep();
      if (stepperLength % 15 == 0) {
        moveOneStep();
      }
      stepperLength--;
    }
  } else {
    Serial.println("Dimensiunea introdusa este prea mare.");
  }
}

void resetPositions() {
  servoA.write(90);
  servoB.write(90);
  servoC.write(90);
  
  digitalWrite(DIR, LOW);
  irValue = analogRead(IR_PIN);

  while (steps != 0 || irValue > 100) {
    irValue = analogRead(IR_PIN);
    
    digitalWrite(STEP, HIGH);
    delay(10);          
    digitalWrite(STEP, LOW); 
    delay(10);
    if (steps > 0) {
      steps--;
    }
  }

  digitalWrite(DIR, HIGH);
}

void handleMotors(String command) {
  char commandName = command.charAt(0);
  int commandValue = command.substring(1, command.length()).toInt();

  if (commandName == 'A') {
    servoA.write(commandValue);
  }
  else if (commandName == 'B') {
    servoB.write(commandValue);
  }
  else if (commandName == 'C') {
    servoC.write(commandValue);
  }
  else if (commandName == 'S') {
    moveStepper(commandValue);
  }

  delay(100);
}

void pointStar(int A, int B, int C, int S) {
  resetPositions();
  
  servoA.write(A);
  delay(300);
  servoB.write(B);
  delay(300);
  servoC.write(C);
  delay(300);
  moveStepper(S);
}

bool isSentCommandValid(String sentCommand) {
  return sentCommand.startsWith("<") && sentCommand.endsWith(">");
}
