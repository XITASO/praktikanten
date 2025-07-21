namespace PlayTogether.Web.Services;

/// <summary>
/// The CounterService class is registered as a singleton service in the Program.cs file.
/// This means, that it is only created once and can be used to share state across the whole application.
/// </summary>
public class CounterService
{
    /// <summary>
    /// Holds the current count value shown in the UI
    /// </summary>
    public int CurrentCount { get; private set; }
    
    /// <summary>
    /// You can subscribe to this event to get notified when the count changes
    /// </summary>
    public event EventHandler? CountChanged;

    /// <summary>
    /// Call this method to increment the current count.
    /// Notifies all subscribers of <see cref="CountChanged"/> that the count was changed.
    /// </summary>
    public void IncrementCount()
    {
        // Increment the count
        // CurrentCount++;
        CurrentCount = CurrentCount + 1;
        
        // Notify all subscribers of the CountChanged event about the change of CurrentCount
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void DecrementCount()
    {
        // Increment the count
        // CurrentCount++;
        CurrentCount = CurrentCount - 1;
        
        // Notify all subscribers of the CountChanged event about the change of CurrentCount
        CountChanged?.Invoke(this, EventArgs.Empty);
    }
}