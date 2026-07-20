namespace PlayTogether.Web.Services;

public class ClickerService
{
    public int Clicks { get; private set; } = 0;

    public int ClickPower { get; private set; } = 1;

    public int UpgradeCost { get; private set; } = 100;


    public void AddClick()
    {
        Clicks += ClickPower;
    }


    public bool BuyUpgrade()
    {
        if (Clicks >= UpgradeCost)
        {
            Clicks -= UpgradeCost;
            ClickPower *= 2;
            UpgradeCost *= 2;

            return true;
        }

        return false;
    }
}