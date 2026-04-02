using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoNotes.Infrastructure;

public static class MongoNotesContext
{
    public static void AddRepositories(this IServiceCollection serviceCollection, string connection, string database)
    {
        serviceCollection.AddSingleton<IMongoClient>(_ => new MongoClient(connection));
        serviceCollection.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(database);
        });
    }
}