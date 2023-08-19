namespace ApplicationRegistry.Data.Models;

public class ApplicationDataModel
{
    public string Code { get; set; }

    public string Name { get; set; }

    public List<EventDataModel> Events { get; set; }
}
