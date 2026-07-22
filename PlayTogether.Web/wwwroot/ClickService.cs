namespace PlayTogether.Web.Services;
using PlayTogether.Web.Services;


public class ClickerService(LobbyService lobbyService)
{
    public int clicks(string playername)
    {
        return lobbyService.getPlayerData(playername).clicks;
    }

    public int clickPower(string playername)
    {
        return lobbyService.getPlayerData(playername).clickMultiplier;
    }

    public int upgradeCost(string playername)
    {
        return lobbyService.getPlayerData(playername).upgradeCost;
    }
    
    public void AddClick(string playername)
    {
        //Clicks += ClickPower;
        lobbyService.getPlayerData(playername).click();
        OnClicksChanged();
    }
    
    public bool BuyUpgrade(string playername)
    {
        OnClicksChanged();
        return lobbyService.getPlayerData(playername).upgrade();
    }
    
    public event EventHandler? ClicksChanged;
    protected virtual void OnClicksChanged()
    {
        ClicksChanged?.Invoke(this, EventArgs.Empty);
    }
}