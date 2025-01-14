using Bridger.App.Views;
using MongoDB.Driver;

namespace Bridger.App.Repository;

public abstract class MongoRepository {
    public static string ConnectionString { get; set; }

    public static string DatabaseName { get; set; }

    public static string Host { get; set; }

    public static IMongoDatabase GetDatabase() {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            new RegisterConnectionStringView().ShowDialog();
        }

        var client = new MongoClient(ConnectionString);
        return client.GetDatabase("MTS");
    }
}