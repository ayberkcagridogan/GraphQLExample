using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Commands;

public class CommandType : ObjectType<Command>{

    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represent any executable command");

        descriptor
                .Field(c=> c.Platform)
                .ResolveWith<Resolvers>(r => r.GetPlatform(default! , default!))
                .UseDbContext<AppDbContext>()
                .Description("This is a platform to which command belongs");

    }

    private class Resolvers{

        public Platform GetPlatform([Parent] Command command , [ScopedService] AppDbContext context){

            return context.Platforms.FirstOrDefault(c => c.Id == command.PlatformId);
        }
    }
}