using Microsoft.AspNetCore.Components;

namespace PlayTogether.Web.Services;
public class Roomservice
{
    public int Room1 { get; private set; }
    public int Room2 { get; private set; }
    public int Room3 { get; private set; }
    
    
    public event EventHandler? CountChanged;

   
    public void room1join()
    {
        
        Room1 = Room1 + 1;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void room2join()
    {
        
        Room2 = Room2 + 1;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void room3join()
    {
        
        Room3 = Room3 + 1;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void room3leafe()
    {

        Room3 = Room3 - 1;
    }
    public void room2leafe()
    {

        Room2 = Room2 - 1;
    }
    public void room1leafe()
    {

        Room1 = Room1 - 1;
    }
   
}