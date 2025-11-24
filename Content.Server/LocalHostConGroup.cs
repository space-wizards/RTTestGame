using Content.Shared;
using JetBrains.Annotations;
using Robust.Server.Console;
using Robust.Shared.Player;

namespace Content.Server;

/// <summary>
///     Debug/example ConGroup controller implementation that gives any client connected through localhost every permission.
/// </summary>
[UsedImplicitly]
public sealed class LocalHostConGroup : BaseLocalHostConGroup, IConGroupControllerImplementation, IPostInjectInit
{
    [Dependency] private readonly IConGroupController _controller = null!;

    public bool CanCommand(ICommonSession session, string cmdName) {
        return IsLocal(session);
    }

    public bool CanViewVar(ICommonSession session) {
        return IsLocal(session);
    }

    public bool CanAdminPlace(ICommonSession session) {
        return IsLocal(session);
    }

    public bool CanScript(ICommonSession session) {
        return IsLocal(session);
    }

    public bool CanAdminMenu(ICommonSession session) {
        return IsLocal(session);
    }

    public bool CanAdminReloadPrototypes(ICommonSession session) {
        return IsLocal(session);
    }

    void IPostInjectInit.PostInject() {
        _controller.Implementation = this;
    }
}