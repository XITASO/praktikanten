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

    public void join(String name, String passwort, String spielername)
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
            return;
        }
        // lobby existiert
        
        if (passwort != existing.Password)
        {
            Console.WriteLine("Passwords do not match");
            return;
        }
        //passwort stimmt
        
        existing.Players.Add(spielername);
    }
}

public class Lobby
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";

    public List<String> Players = new List<String>();
}