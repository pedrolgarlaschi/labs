BussStop[] busses;
BufferedReader reader;
String line;
PFont f;
ArrayList<BussStop> bussStops = new ArrayList<BussStop>();

void setup()
{
   noCursor();
   
  reader = createReader("bus-stops.csv");  
  printArray(PFont.list());
  f = createFont("SourceCodePro-Regular.ttf", 12);
  textFont(f);
  
  //size(1280, 720, P3D);
  fullScreen();
  //size(1280 , 720);
  
 
  loadBusses();  
  
  
  

}
void draw()
{
  background(0);
  drawBusses();
}

void loadBusses()
{
  
  try {
    line = reader.readLine();
    
    } catch (IOException e) {
      e.printStackTrace();
      line = null;
  }
  
  while(line != null)
  {
    try {
      line = reader.readLine();
      
      if(line != null)
        createBussStop(line);
    
    } catch (IOException e) {
      e.printStackTrace();
      line = null;
    }
  } 
}

void drawBusses()
{
  BussStop closer = null;
  float minDist = 100;
  
  int len = bussStops.size();
  for(int i = 0 ; i < len;i++)
  {
    BussStop b = bussStops.get(i);
    
    float dx = (width / 2) - b.x;
    float dy = (height / 2) - b.y;
    
    float sq = sqrt(dx * dx + dy * dy);
    
    float mx = mouseX - b.x;
    float my = mouseY - b.y;
    
    float d = sqrt(mx * mx + my * my);
    
    float size = 0.7;
    
     float c =  500 - sq;
     
    
     if(d < 30)
     {
       size = 50 / (d + 1);
       c = 255;
     }
     
     if(c <= 100)
       c = 100;
     
     
     b.size += (size - b.size) * 0.3;
    
    noStroke();
    fill(c);
    ellipse(b.x, b.y, b.size, b.size);
    
    if(d < minDist)
    {
      closer = b;
      minDist = d;
    }
  }
  
  if(closer != null)
    println("closer: " + closer.name);
  
  if(closer == null)
  return;
  
  textAlign(LEFT);
  text(closer.name, 30, 30);
  
}

void createBussStop(String line)
{
  String[] arr = line.split(",");
  
  String code = arr[0];
  String name = arr[3];
  
  BussStop b = new BussStop(name , code);
  b.x = parseInt(arr[4]) / 70.0;
  b.y = parseInt(arr[5])/ 80.0;
  
  b.x -= 6800;
  b.y -= 1750;
  
  bussStops.add(b);
}