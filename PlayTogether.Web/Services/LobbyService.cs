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
}

public class Lobby
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
}