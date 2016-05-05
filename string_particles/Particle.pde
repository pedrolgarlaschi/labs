class Particle
{
  
  public PVector pos = new PVector();
  public PVector accell = new PVector();
  public PVector speed = new PVector();
  private float radius = 3;
  
  public boolean iterate;
  
  public int connections = 0;
  
  
  float nx = random(0,1000);
  float ny = random(0,1000);
  
  public Particle()
  {
    
    pos = new PVector(random(0, width),random(0, height));
    
    addForce(new PVector(random(-1,1) , random(-1,1)));
    
  }
  
  public void addForce(PVector v)
  {
      accell.x += v.x;
      accell.y += v.y;
  }
  
  
  void update()
  {
    
    float n1 = random(-100,100) / 1000;
    float n2 = random(-100,100) / 1000;
    
    addForce(new PVector(n1 , n2));
    
    speed.x += accell.x;
    speed.y += accell.y;
    
    pos.x += speed.x;
    pos.y += speed.y;
    
    speed.x *= 0.98;
    speed.y *= 0.98;
    
    accell.x = 0;
    accell.y = 0;
    
    
    nx++;
    ny++;
    
    if(pos.x < 0 || pos.x > width)
      speed.x *= -1;
    if(pos.y < 0 || pos.y > height)
      speed.y *= -1;  
      
  }
  void draw()
  {   
    ellipse(pos.x,pos.y,radius,radius);
  }
  
  public float getDistance(Particle p)
  {
    float dx = p.pos.x - pos.x;
    float dy = p.pos.y - pos.y;
    
    return sqrt(dx * dx + dy * dy);
  }
  
}