using CretNet.Platform.Blazor.Services;

namespace CretCollect.App.Wasm.General.Services;

public class ServerTimeProvider : IServerTimeProvider
{
    public Task<DateTimeOffset> GetServerTimeAsync()
    {
        return Task.FromResult(DateTimeOffset.Now);
    }
}