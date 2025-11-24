namespace Content.Client;

internal static class ClientContentIoC
{
    public static void Register(IDependencyCollection deps)
    {
        // DEVNOTE: IoCManager registrations for the client go here and only here.
        deps.Register<ClientConGroupController>();
    }
}