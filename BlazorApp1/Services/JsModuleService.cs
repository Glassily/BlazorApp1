using Microsoft.JSInterop;

namespace BlazorApp1.Services
{
    // 定义接口（便于测试和扩展）
    public interface IJsModuleService : IAsyncDisposable
    {
        Task<IJSObjectReference> GetOrImportAsync(string moduleName);
    }

    public class JsModuleService(IJSRuntime js) : IJsModuleService
    {
        private readonly IJSRuntime _js = js;
        private readonly Dictionary<string, IJSObjectReference> _modules = [];
        private readonly SemaphoreSlim _lock = new(1, 1);


        public async Task<IJSObjectReference> GetOrImportAsync(string moduleName)
        {
            await _lock.WaitAsync();
            try
            {
                if (!_modules.TryGetValue(moduleName, out var module))
                {
                    module = await _js.InvokeAsync<IJSObjectReference>(
                        "import", moduleName);
                    _modules[moduleName] = module;
                }
                return module;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {

            // Dispose all imported JS modules
            foreach (var module in _modules.Values)
            {
                await module.DisposeAsync();
            }
            _modules.Clear();
            _lock.Dispose();

            GC.SuppressFinalize(this);
        }

    }
}
