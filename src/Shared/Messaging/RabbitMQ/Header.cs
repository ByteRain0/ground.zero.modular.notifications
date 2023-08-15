using System.Text;

namespace Shared.Messaging.RabbitMQ;

public record Header
{
    public Header()
    {
        Properties = new Dictionary<string, object>();
    }

    public Header(string applicationCode, string tenantCode)
    {
        Properties = new Dictionary<string, object>
        {
            {HeaderConstants.AppCode, applicationCode}, {HeaderConstants.TenantCode, tenantCode}
        };
    }

    public IDictionary<string, object>? Properties { get; set; }

    public string AppCode => RetrieveStringHeader(HeaderConstants.AppCode);

    public string TenantCode => RetrieveStringHeader(HeaderConstants.TenantCode);

    private string RetrieveStringHeader(string key)
    {
        var value = Properties!.FirstOrDefault(x => x.Key == key).Value;

        if (value is null)
        {
            throw new InvalidOperationException("NotFound");
        }

        if (value is byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray);
        }

        if (value is string stringValue)
        {
            return stringValue;
        }

        return string.Empty;
    }
}
