ArrayList<Particle> m_particles;

int numParticles = 1000;
PImage img;

void setup()
{ 
  //img = loadImage("img.jpg");
  
  createParticles();
  size(1280,720,P3D);
  
  //fullScreen(P2D);
  //noCursor();
  
}

void draw()
{
  background(0);
  
  
  if(m_particles == null)return;
  
  updateParticles();
}

float maxDist = 100;

private void updateParticles()
{
  for(int i = 0 ; i < numParticles;i++)
  {
    m_particles.get(i).connections = 0; 
    m_particles.get(i).iterate = false;
  }
  for(int i = 0 ; i < numParticles;i++)
  {
    Particle p = m_particles.get(i);
    p.update();
    
    
    
    for(int j = 0 ; j < numParticles;j++)
    {
      Particle p2 = m_particles.get(j);
      if(p == p2)
        continue;
      
      if(p.iterate || p2.iterate)
        continue; 
        
        float d = p.getDistance(p2);
      
        if(d < maxDist)  
        {
          drawLine(d,p.pos,p2.pos);
          p.connections ++;
        }          
    }
    
    p.iterate = true;
    p.draw();
  }
  
  
}

private void drawLine(float d , PVector p1 ,PVector p2)
{
  float normal = d / maxDist;
  
  normal = 1 - normal;
  
  stroke(normal * 255);
  strokeWeight(normal);
  line(p1.x, p1.y, p2.x, p2.y);
}

private void createParticles()
{
  m_particles = new ArrayList<Particle>();
  
  for(int i = 0 ; i < numParticles;i++)
  {
    m_particles.add(new Particle());
  }
}

void mousePressed() {
  
  for(int i = 0 ; i < numParticles;i++)
  {
    Particle p = m_particles.get(i);
    
    float dx = p.pos.x - mouseX;
    float dy = p.pos.y - mouseY;
    
    float d = sqrt(dx * dx + dy * dy);
    
    if(d < 100)
    {
      p.addForce(new PVector(dx / 10.0,dy / 10.0));
    }
    
  }

}