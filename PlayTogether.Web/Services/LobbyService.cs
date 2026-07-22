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

    /**
     * <returns>1 -> Lobby nicht gefunden;   </returns>
     * <returns>2 -> falsches Passwort;    </returns>
     * <returns>3 -> player schon in der lobby;   </returns>
     * <returns>0 -> erfolg!</returns>
     */
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
        // lobby existiert
        
        if (passwort != existing.Password)
        {
            Console.WriteLine("Passwords do not match");
            return 2;
        }
        // passwort stimmt

        if (existing.Players.ContainsKey(spielername))
        {
            Console.WriteLine("Spieler ist bereits in der Lobby");
            return 3;
        }
        // spieler ist noch nicht vorhanden
        
        existing.Players.Add(spielername, new Playerdata());
        return 0;
    }

    /**
     * <param name="lobby">An Instance of Lobby</param>
     * <returns>A list of Tupels, format: ( playername, clicks )</returns>
     */
    public List<(string name, int score)> GetScoreboard(Lobby? lobby)
    {
        List<(string name, int score)> list = new();
        
        if (lobby == null) return list;
        
        foreach (var pair in lobby.Players)
        {
            list.Add((pair.Key, pair.Value.Clicks));
        }

        return list;
    }
}
// ______________________________LOBBY CLASS______________________________________
public class Lobby
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    
    public Dictionary<string, Playerdata> Players = new();
}

//____________________________________PLAYERDATA CLASS__________________________________
public class Playerdata
{   
    // ________________________DATA_____________________
    public string Name = "";
    public int Clicks = 0;
    public int ClickMultiplier = 1;
    public int UpgradeCost = 100;
    
    // ____________________PLAYER ACTIONS__________________
    public void Click()
    {
        Clicks += ClickMultiplier;
    }
    
    public bool Upgrade()
    {
        if(Clicks < UpgradeCost) return false;
        
        Clicks -= UpgradeCost;
        ClickMultiplier *= 2;
        UpgradeCost *= 2;
        return true;
    }
}
