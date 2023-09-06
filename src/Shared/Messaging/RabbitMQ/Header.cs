using System.Text;

namespace Shared.Messaging.RabbitMQ;

public record Header
{
    public Header()
    {
        Properties = new Dictionary<string, object>();
    }

    public Header(string sourceCode, string tenantCode, string eventCode)
    {
        Properties = new Dictionary<string, object>
        {
            {HeaderConstants.SourceCode, sourceCode},
            {HeaderConstants.TenantCode, tenantCode},
            {HeaderConstants.EventCode, eventCode}
        };
    }

    public IDictionary<string, object>? Properties { get; set; }

    public string SourceCode => RetrieveStringHeader(HeaderConstants.SourceCode);

    public string TenantCode => RetrieveStringHeader(HeaderConstants.TenantCode);

    public string EventCode => RetrieveStringHeader(HeaderConstants.EventCode);

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
