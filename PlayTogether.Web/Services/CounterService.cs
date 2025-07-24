namespace PlayTogether.Web.Services;
public class CounterService
{
    public int CurrentCount { get; private set; }
    
    
    public event EventHandler? CountChanged;

   
    public void IncrementCount()
    {
        
        CurrentCount = CurrentCount + 1;
        
      
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    public void DecrementCount()
    {
        CurrentCount = CurrentCount - 1;
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
}