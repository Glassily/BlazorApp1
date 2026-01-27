using Markdig;
using Markdig.Prism;

namespace BlazorApp1.Services
{
    public class MarkdownService
    {
        // Markdig pipeline with advanced extensions
        private readonly MarkdownPipeline _pipeline;
        public MarkdownService()
        {
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()            // 启用高级扩展
                .UseEmojiAndSmiley()                // 支持表情符号
                .UseSoftlineBreakAsHardlineBreak()  // 软换行转硬换行
                .UsePrism()                         // 代码高亮
                .Build();
        }
        public string ConvertToHtml(string markdown)
        {
            // Convert Markdown to HTML using the configured pipeline
            // Handle simple exceptions during conversion
            // TODO 可以根据需要扩展错误处理逻辑,例如记录日志等;
            // TODO 增加XSS防护等
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
    }
}
