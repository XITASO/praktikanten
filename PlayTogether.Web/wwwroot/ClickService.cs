namespace PlayTogether.Web.Services;
using PlayTogether.Web.Services;


public class ClickerService(LobbyService lobbyService)
{
    public double? Clicks(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.Clicks;
    }

    public double? ClickPower(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.ClickMultiplier;
    }

    public double? UpgradeCost(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.UpgradeCost;
    }
    
    public void AddClick(string playername)
    {
        lobbyService.GetPlayerData(playername)?.Click();
        OnClicksChanged();
    }
    
    public bool? BuyUpgrade(string playername)
    {
        var result = lobbyService.GetPlayerData(playername)?.Upgrade();
        OnClicksChanged();
        return result;
    }
    
    public event EventHandler? ClicksChanged;
    protected virtual void OnClicksChanged()
    {
        ClicksChanged?.Invoke(this, EventArgs.Empty);
    }

    public double? autoClickPower(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.autoClickPower;
    }

    public double? autoClickUpgradeCost(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.autoClickUpgradeCost;
    }

    public bool? BuyAutoClickUpgrade(string playername)
    {
        return lobbyService.GetPlayerData(playername)?.upgradeAutoClicker();
    }
}