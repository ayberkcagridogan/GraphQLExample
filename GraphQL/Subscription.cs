using CommanderGQL.Models;

namespace CommanderGQL.GraphQl;

public class Subscription{

    [Subscribe]
    [Topic]
    public Platform OnPlatformAdded([EventMessage] Platform platform) => platform;
}