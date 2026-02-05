namespace CretCollect.App.Wasm.General.Services;

public interface IApplicationService
{
    string GetAppVersion();
    string GetEnvironmentName();
}