using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebHooks.WebHooksRepository.Services.Data.Models;

public class WebHookDataModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }

    public string Endpoint { get; set; }

    public string ClientCode { get; set; }

    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TenantCode { get; set; }
}
