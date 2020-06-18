#include <time.h>

float radius = 20; 
float circle = (2*radius*3.14)/100; 

float byc_speed=0; 

float ckTime=0; 
float uckTime=0; 
float cycleTime=0; 

float distance = 0; 
int count=0; 
boolean temp = 0; 




void setup() {

  Serial.begin(9600);
}

void loop() {

  boolean check = digitalRead(A0);
  if(check==1 && temp==0) 
  {
    ckTime = millis(); 
    temp=1; 
    
  }
  else if(check==0 && temp==1 && count>5) 
  {
    int i = 0;
    uckTime = millis(); 
    cycleTime = (uckTime - ckTime) / 1000;
    cycleTime = cycleTime + i;
    byc_speed = (circle/cycleTime)*3.6; 
    byc_speed = byc_speed + i;
     i++;
    temp=0;
    count=0;
    distance = distance + circle;

    
    Serial.print(int(byc_speed));
    Serial.print(" ");
  
    Serial.println((int)distance);
    
    
    delay(1000);
  }


  if(check==1) 
  {
    count++;
    
    if(count>150) 
    {
      byc_speed=0;
    }
  }

}
