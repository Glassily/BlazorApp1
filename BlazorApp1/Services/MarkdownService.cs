using Markdig;
using Markdig.Extensions.AutoLinks;
using Markdig.Extensions.Tables;
using Markdig.Extensions.TaskLists;
using Markdig.Extensions.Emoji;

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
                .Build();
        }
        public string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }

    }
}
