using Content.Shared;
using Robust.Client.Console;
using Robust.Client.Player;

namespace Content.Client;

public sealed class ClientConGroupController : BaseLocalHostConGroup, IClientConGroupImplementation, IPostInjectInit
{
    [Dependency] private readonly IPlayerManager _playerManager = null!;
    [Dependency] private readonly IClientConGroupController _controller = null!;

    void IPostInjectInit.PostInject() {
        _controller.Implementation = this;
    }

    public bool CanCommand(string cmdName) => IsLocal();
    public bool CanViewVar() => IsLocal();
    public bool CanAdminPlace() => IsLocal();
    public bool CanScript() => IsLocal();
    public bool CanAdminMenu() => IsLocal();

    private bool IsLocal()
    {
        return _playerManager.LocalSession != null && IsLocal(_playerManager.LocalSession);
    }

    public event Action? ConGroupUpdated;
}