namespace PlayTogether.Web.Services;
using PlayTogether.Web.Services;


public class ClickerService(LobbyService lobbyService)
{
    public int? Clicks(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.Clicks;
    }

    public int? ClickPower(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.ClickMultiplier;
    }

    public int? UpgradeCost(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.UpgradeCost;
    }
    
    public void AddClick(string playername)
    {
        //Clicks += ClickPower;
        lobbyService.GetPlayerData(playername)?.Click();
        OnClicksChanged();
    }
    
    public bool? BuyUpgrade(string playername)
    {
        OnClicksChanged();
        return lobbyService.GetPlayerData(playername)?.Upgrade();
    }
    
    public event EventHandler? ClicksChanged;
    protected virtual void OnClicksChanged()
    {
        ClicksChanged?.Invoke(this, EventArgs.Empty);
    }
}