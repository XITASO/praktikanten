namespace PlayTogether.Web.Services;

public class LobbyService
{
    public List<Lobby> Lobbies { get; set; } = new();


    public void CreateLobby(string name, string password)
    {
        if (!Lobbies.Any(l => l.Name == name))
        {
            Lobbies.Add(new Lobby
            {
                Name = name,
                Password = password,
            });
            
            Console.WriteLine("Created new lobby");
        }
        else
        {
            Console.WriteLine("Duplicate lobby");
        }
    }

    public Lobby GetLobby(string name)
    {
        foreach (var lobby in Lobbies)
        {
            if (lobby.Name == name) return lobby;
        }
        return null;
    }

    public Lobby? FindPlayerLobby(string playername)
    {
        foreach (var lobby in Lobbies)
        {
            if (lobby.Players.ContainsKey(playername)) return lobby;
        }
        return null;
    }

    public void RemovePlayerFromLobby(string playername)
    {
        FindPlayerLobby(playername)?.Players.Remove(playername);
        Console.WriteLine("Player " + playername + " has left its lobby");
    }

    public Playerdata? GetPlayerData(string playername)
    {
        if(FindPlayerLobby(playername) == null) return null;
        return FindPlayerLobby(playername)?.Players[playername];
    }

    public int Join(string name, string passwort, string spielername)
    {
        bool lobbyexists = false;
        Lobby existing = new Lobby();
        
        foreach (var lobby in Lobbies)
        {
            if (lobby.Name == name)
            {
                lobbyexists = true;
                existing = lobby;
                break;
            }
        }

        if (!lobbyexists || existing == null)
        {
            Console.WriteLine("Lobby not found");
            return 1;
        }
        
        if (passwort != existing.Password)
        {
            Console.WriteLine("Passwords do not match");
            return 2;
        }

        if (existing.Players.ContainsKey(spielername))
        {
            Console.WriteLine("Spieler ist bereits in der Lobby");
            return 3;
        }
        
        existing.Players.Add(spielername, new Playerdata());
        return 0;
    }

    public List<(string name, double score)> GetScoreboard(Lobby? lobby)
    {
        List<(string name, double score)> list = new();
        
        if (lobby == null) return list;
        
        foreach (var pair in lobby.Players)
        {
            list.Add((pair.Key, pair.Value.Clicks));
        }

        return list;
    }
}

public class Lobby
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    
    public Dictionary<string, Playerdata> Players = new();
}

public class Playerdata
{
    private Random random = new();
    
    public string Name = "";
    
    public double Clicks = 0;
    public double ClickMultiplier = 1;
    public double UpgradeCost = 100;
    public int UpgradeLevel = 1;
    
    public double autoClickPower = 1;
    public double autoClickUpgradeCost = 100;
    public int autoClickUpgradeLevel = 1;

    public double criticalClickChance = 0.05; // 5 %
    public double criticalClickMultiplier = 2;
    
    public double Click()
    {
        double random = this.random.NextDouble();

        if (random <= criticalClickChance)  // crit
        {
            Clicks += ClickMultiplier *  criticalClickMultiplier;
            return ClickMultiplier * criticalClickMultiplier;
        }
        Clicks += ClickMultiplier;
        return ClickMultiplier;
    }
    
    public bool Upgrade()
    {
        if (Clicks < UpgradeCost) return false;
        
        Clicks -= UpgradeCost;

        double gain = Math.Ceiling(Math.Sqrt(UpgradeLevel) * 2);
        ClickMultiplier += gain;
        UpgradeLevel++;

        double growthRate = Math.Min(0.3 + UpgradeLevel * 0.015, 0.9);
        UpgradeCost += UpgradeCost * growthRate;

        return true;
    }
    
    public void autoClick()
    {
        Clicks += autoClickPower;
    }

    public bool upgradeAutoClicker()
    {
        if (Clicks < autoClickUpgradeCost) return false;
        
        Clicks -= autoClickUpgradeCost;

        double gain = Math.Ceiling(Math.Sqrt(autoClickUpgradeLevel) * 2);
        autoClickPower += gain;
        autoClickUpgradeLevel++;

        double growthRate = Math.Min(0.3 + autoClickUpgradeLevel * 0.015, 0.9);
        autoClickUpgradeCost += autoClickUpgradeCost * growthRate;

        return true;
    }
}