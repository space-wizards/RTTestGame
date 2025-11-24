using System.Diagnostics;
using System.Net;
using Robust.Shared.Player;
using Robust.Shared.Toolshed;
using Robust.Shared.Toolshed.Errors;
using Robust.Shared.Utility;

namespace Content.Shared;

public abstract class BaseLocalHostConGroup : IPermissionController
{
    private record NotLocalError : IConError
    {
        public FormattedMessage DescribeInner()
        {
            return FormattedMessage.FromUnformatted("Not the local user, refusing!");
        }

        public string? Expression { get; set; }

        public Vector2i? IssueSpan { get; set; }

        public StackTrace? Trace { get; set; }
    }

    protected static bool IsLocal(ICommonSession player) {
        var ep = player.Channel.RemoteEndPoint;
        var addr = ep.Address;
        if (addr.IsIPv4MappedToIPv6) {
            addr = addr.MapToIPv4();
        }

        return Equals(addr, IPAddress.Loopback) || Equals(addr, IPAddress.IPv6Loopback);
    }

    public bool CheckInvokable(CommandSpec command, ICommonSession? user, out IConError? error)
    {
        if (user != null && !IsLocal(user))
        {
            error = new NotLocalError();
            return false;
        }

        error = null;
        return true;
    }
}