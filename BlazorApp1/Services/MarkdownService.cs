using Markdig;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
    public class MarkdownService(IJsModuleService jsModule)
    {
        // Markdig pipeline with advanced extensions
        private readonly MarkdownPipeline _pipeline = 
            new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()            // 启用高级扩展
                .UseEmojiAndSmiley()                // 支持表情符号
                .UseSoftlineBreakAsHardlineBreak()  // 软换行转硬换行
                .Build();
        
        // JS模块服务
        private readonly IJsModuleService _jsModule = jsModule;

        public async Task<string> ConvertToHtmlAsync(string markdown, bool usejsModule = false)
        {
            // Convert Markdown to HTML using the configured pipeline
            // Handle simple exceptions during conversion
            // TODO 可以根据需要扩展错误处理逻辑,例如记录日志等;
            // TODO 增加XSS防护等
            if (!usejsModule)
            {
                try
                {
                    return Markdown.ToHtml(markdown, _pipeline);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Markdown转换出错:\n{e}");
                    return "渲染错误";
                }
            }
            try
            {
                var module = await _jsModule.GetOrImportAsync("/js/md-modules.js");
                var result = await module.InvokeAsync<string>(
                    "renderMarkdown1", 
                    markdown
                    );
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Markdown转换出错:\n{e}");
                return "渲染错误";
            }
        }
    }
}
