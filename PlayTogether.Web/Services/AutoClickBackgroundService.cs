namespace PlayTogether.Web.Services;

public class AutoClickBackgroundService : BackgroundService
{
    private readonly LobbyService _lobbyService;

    public AutoClickBackgroundService(LobbyService lobbyService)
    {
        _lobbyService = lobbyService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

            foreach (var lobby in _lobbyService.Lobbies)
            {
                foreach (var playerData in lobby.Players.Values)
                {
                    playerData.autoClick();
                }
            }
        }
    }
}