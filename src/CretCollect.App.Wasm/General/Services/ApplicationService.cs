namespace CretCollect.App.Wasm.General.Services;

public class ApplicationService : IApplicationService
{
    private readonly IConfiguration _configuration;

    public ApplicationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GetEnvironmentName()
    {
        return _configuration["ASPNETCORE_ENVIRONMENT"] ?? "Unknown";
    }
    
    public string GetAppVersion()
    {
        return VersionInfo.SemVer;
    }
}

public static class VersionInfo
{
    public static string SemVer { get; } = global::GitVersionInformation.FullSemVer ?? "0.0.0-gitversion-unavailable";
}