namespace WebHooks.WebHooksRepository.Contracts;

public static class ErrorCodes
{
    private static readonly string ModuleName = "WebHooksRepository";

    public static string AlreadyExists = $"{ModuleName}_DataExists";

    public static string DeleteIssues = $"{ModuleName}_DeleteIssues";

    public static string WebHookNotFound = $"{ModuleName}_NotFound";

    public static string GeneralIssues = $"{ModuleName}_GeneralIssues";
}
