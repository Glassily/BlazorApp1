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
            return Markdown.ToHtml(markdown, _pipeline);
        }
    }
}
