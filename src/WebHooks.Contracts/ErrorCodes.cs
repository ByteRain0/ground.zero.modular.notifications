namespace WebHooks.Contracts;

public static class ErrorCodes
{
    private static readonly string ModuleName = "WebHooksService";

    public static string GeneralModuleIssues = $"{ModuleName}_Issue";

    public static string WebHookNotFound = $"{ModuleName}_NotFound";

    public static string DataRetrievalIssues = $"{ModuleName}_DataRetrievalFailure";

    public static string DataMutationFailure = $"{ModuleName}_DataMutationFailure";
}
