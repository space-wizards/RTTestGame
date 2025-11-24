using Robust.Server.Player;
using Robust.Shared.Asynchronous;
using Robust.Shared.Enums;
using Robust.Shared.Player;

namespace Content.Server;

public sealed class ConnectionManager
{
    [Dependency] private readonly IPlayerManager _playerManager = null!;
    [Dependency] private readonly ITaskManager _taskManager = null!;

    public void Initialize()
    {
        _playerManager.PlayerStatusChanged += PlayerManagerOnPlayerStatusChanged;
    }

    private void PlayerManagerOnPlayerStatusChanged(object? sender, SessionStatusEventArgs e)
    {
        if (e.NewStatus != SessionStatus.Connected)
            return;

        _taskManager.RunOnMainThread(() =>
        {
            _playerManager.JoinGame(e.Session);
        });
    }
}