using Microsoft.JSInterop;
using System.Collections.Concurrent;

namespace BlazorApp1.Services
{
    public interface IJsModuleService : IAsyncDisposable
    {
        Task<IJSObjectReference> GetOrImportAsync(string modulePath);
    }

    public class JsModuleService(IJSRuntime js) : IJsModuleService
    {
        private readonly IJSRuntime _jsRuntime = js;
        private readonly Dictionary<string, IJSObjectReference> _modules = [];
        private readonly SemaphoreSlim _loadLock = new(1, 1);


        /// <summary>
        /// 获取或引入JS模块
        /// </summary>
        /// <param name="modulePath">模块</param>
        /// <returns>JS模块对象</returns>
        public async Task<IJSObjectReference> GetOrImportAsync(string modulePath)
        {
            await _loadLock.WaitAsync();
            try
            {
                if (!_modules.TryGetValue(modulePath, out var module))
                {
                    module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", modulePath);
                    _modules.TryAdd(modulePath, module);
                }
                return module;
            }
            finally
            {
                _loadLock.Release();
            }
        }

        public bool IsModuleLoaded(string moduleName)
        {
            return _modules.ContainsKey(moduleName);
        }

        public async ValueTask DisposeAsync()
        {

            await _loadLock.WaitAsync();
            try
            {
                foreach (var module in _modules)
                {
                    await module.Value.DisposeAsync();
                }
                _modules.Clear();
            } 
            catch (AggregateException agEx)
            {
                // 记录日志，但不重新抛出，确保所有模块都被尝试释放
                Console.WriteLine($"释放JS模块时发生错误: {agEx.Message}");
            }
            finally { 
                _loadLock.Release();
            }

            GC.SuppressFinalize(this);
        }
    }
}