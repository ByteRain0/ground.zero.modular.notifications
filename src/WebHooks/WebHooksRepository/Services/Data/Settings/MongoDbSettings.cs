using System.ComponentModel.DataAnnotations;

namespace WebHooks.WebHooksRepository.Services.Data.Settings;

public class MongoDbSettings
{
    [Required]
    public string ConnectionString { get; set; }

    [Required]
    public string DatabaseName { get; set; }

    [Required]
    public string WebHooksCollectionName { get; set; }
}
