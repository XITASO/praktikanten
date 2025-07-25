using Microsoft.AspNetCore.Components;

namespace PlayTogether.Web.Services;
public class Roomservice
{
    public int Room1 { get; private set; } = 0;
    public int Room2 { get; private set; }= 0;
    public int Room3 { get; private set; } = 0;
    
    
    public event EventHandler? CountChanged;
    public void test()
    {

        Console.WriteLine("Room1" + Room1);
        Console.WriteLine("Room2" + Room2);
        Console.WriteLine("Room3" + Room3);
    }

    public void Room1reset()
    {
        Room1 = 0;
    }
    public void Room2reset()
    {
        Room2 = 0;
    }
    public void Room3reset()
    {
        Room3 = 0;
    }
    public void room1join()
    {

        Room1++;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void room2join()
    {

        Room2++;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void room3join()
    {
        
        Room3++;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void room3leafe()
    {

        Room3--;
    }
    public void room2leafe()
    {

        Room2 --;
    }
    public void room1leafe()
    {

        Room1--;
    }
   
}