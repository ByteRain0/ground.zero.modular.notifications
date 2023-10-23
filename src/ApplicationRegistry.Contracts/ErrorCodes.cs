namespace ApplicationRegistry.Contracts;

public static class ErrorCodes
{
    private static readonly string ModuleName = "ApplicationRegistry";

    public static string ApplicationNotFound = $"{ModuleName}_NotFound";

    public static string GeneralModuleIssues = $"{ModuleName}_Issues";
}
