namespace TodoApp.Application.Common.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ItemsCollection { get; set; } = null!;
    public string UsersCollection { get; set; } = null!;
}