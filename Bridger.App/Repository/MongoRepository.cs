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
        DatabaseName = "DATABASE: bridger";
        var index = ConnectionString.IndexOf($"@", StringComparison.Ordinal);
        var indexOfBar = ConnectionString.IndexOf("/", index, StringComparison.Ordinal);
        Host = $"HOST: {ConnectionString.Substring(index, indexOfBar - index)}";
        return client.GetDatabase("bridger");
    }
}