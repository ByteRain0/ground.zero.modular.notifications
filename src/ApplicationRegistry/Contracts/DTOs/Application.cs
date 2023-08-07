namespace ApplicationRegistry.Contracts.DTOs;

public class Application
{
    public string Code { get; set; }

    public string Name { get; set; }

    public List<Event> Events { get; set; }
}
