using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.EntityFrameworkCore;
using GraphQL.Server.Ui.Voyager;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQl;

var builder = WebApplication.CreateBuilder(args);
//DB Connection
builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CommanderDbCons")));

//Add GraphQL Server
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()                
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
                

var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();

app.UseGraphQLVoyager(new VoyagerOptions() 
{
    GraphQLEndPoint = "/graphql"
} , "/graphql-voyager");

app.Run();
