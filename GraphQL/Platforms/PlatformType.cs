using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Platforms;

public class PlatformType : ObjectType<Platform> {

    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor){

       
        descriptor.Description("Represent any software or service that has a command line interface.");
        
        descriptor.Ignore(p => p.LicenseKey);

        descriptor
                .Field(p => p.Commands)
                .UseDbContext<AppDbContext>()
                .ResolveWith<Resolvers>(p => p.GetCommands(default , default))
                .Description("This is list of avaible commands for the this platform.");
    }
    private class Resolvers{        
        public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context){

            return context.Commands.Where(p => p.PlatformId == platform.Id);
        }
    }   
}

