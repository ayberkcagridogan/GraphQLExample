using CommanderGQL.Data;
using CommanderGQL.GraphQl;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL;

public class Mutation{

    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(
        AddPlatformInput input , [ScopedService] AppDbContext context , 
        [Service] ITopicEventSender eventsender , CancellationToken cancellationToken ){
        
        var platform = new Platform{
            Name = input.name
        };

        context.Platforms.Add(platform);
        await context.SaveChangesAsync(cancellationToken);

        await eventsender.SendAsync(nameof(Subscription.OnPlatformAdded) , platform , cancellationToken);

        return new AddPlatformPayload(platform);
    }

    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context){

        var command = new Command{
            HowTo = input.howTo,
            CommandLine = input.commandLine,
            PlatformId = input.platformId
        };

        context.Commands.Add(command);
        await context.SaveChangesAsync();

        return new AddCommandPayload(command);
    }
}