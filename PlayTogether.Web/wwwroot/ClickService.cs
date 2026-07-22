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
        lobbyService.getPlayerData(playername).click();
    }
    
    public bool BuyUpgrade(string playername)
    {
        return lobbyService.getPlayerData(playername).upgrade();
    }

    public int autoClickPower(string playername)
    {
        return lobbyService.getPlayerData(playername).autoClickPower;
    }

    public int autoClickUpgradeCost(string playername)
    {
        return lobbyService.getPlayerData(playername).autoClickUpgradeCost;
    }

    public bool BuyAutoClickUpgrade(string playername)
    {
        return lobbyService.getPlayerData(playername).upgradeAutoClicker();
    }
}