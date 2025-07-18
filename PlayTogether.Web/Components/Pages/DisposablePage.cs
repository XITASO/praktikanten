using Microsoft.AspNetCore.Components;

namespace PlayTogether.Web.Components.Pages;

public class DisposablePage : ComponentBase, IDisposable
{
    public List<Action> DisposeActions { get; } = new();
    
    public void Dispose()
    {
        foreach (var action in DisposeActions)
        {
            action();
        }
    }
}