namespace WebHooks.WebHooksRepository.Services.Data.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string WebHooksCollectionName { get; set; }
}
