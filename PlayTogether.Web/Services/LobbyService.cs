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

    public Lobby findPlayerLobby(string playername)
    {
        foreach (var lobby in Lobbies)
        {
            if (lobby.Players.ContainsKey(playername)) return lobby;
        }
        return null;
    }

    public Playerdata getPlayerData(string playername)
    {
        if(findPlayerLobby(playername) == null) return null;
        return findPlayerLobby(playername).Players[playername];
    }

    public bool join(String name, String passwort, String spielername)
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
            return false;
        }
        // lobby existiert
        
        if (passwort != existing.Password)
        {
            Console.WriteLine("Passwords do not match");
            return false;
        }
        //passwort stimmt
        
        existing.Players.Add(spielername, new Playerdata());
        return true;
    }

    public List<(string name, int score)> getScoreboard(Lobby lobby)
    {
        List<(string name, int score)> List = new();
        foreach (var pair in lobby.Players)
        {
            List.Add((pair.Key, pair.Value.clicks));
        }

        return List;
    }
}

public class Lobby
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";

    //public List<String> Players = new List<String>();
    public Dictionary<string, Playerdata> Players = new();
}

public class Playerdata
{
    public string Name;
    public int clicks = 0;
    public int clickMultiplier = 1;
    public int upgradeCost = 100;

    public void click()
    {
        clicks += clickMultiplier;
    }

    public bool upgrade()
    {
        if (clicks >= upgradeCost)
        {
            clicks -= upgradeCost;
            clickMultiplier *= 2;
            upgradeCost *= 2;

            return true;
        }
        return false;
    }
}