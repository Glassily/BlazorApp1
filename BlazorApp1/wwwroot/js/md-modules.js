/* 备用方案 */
import { marked } from "../lib/marked.esm.js"
import MarkdownIt from "../lib/markdown-it.esm.js"
import hljs from "../lib/highlight.min.js";
import CopyButtonPlugin from "../lib/highlightjs-copy.min.js";

// 注册插件
hljs.addPlugin(new CopyButtonPlugin({
    buttonText: '📋 Copy',
    successText: '✓ Copied!',
    errorText: '✗ Error',
    duration: 2000,
    style: {
        top: '0.75em',
        right: '0.75em',
        padding: '0.3em 0.6em',
        fontSize: '0.85em',
        backgroundColor: '#f6f8fa',
        border: '1px solid #dfe2e5',
        borderRadius: '4px'
    }
}));

// 配置 marked 使用 highlight.js
marked.setOptions({
    highlight: function (code, lang) {
        if (lang && hljs.getLanguage(lang)) {
            // 指定语言
            return hljs.highlight(code, { language: lang }).value;
        }
        // 自动检测语言
        return hljs.highlightAuto(code).value;
    },
    breaks: true,      // 支持 GFM 换行
    gfm: true,         // GitHub Flavored Markdown
    pedantic: false,
    smartypants: false
});

// 创建 markdown-it 实例
const md = new MarkdownIt({
    html: true,        // 允许 HTML 标签
    linkify: true,     // 自动转换 URL 为链接
    typographer: true, // 启用智能标点符号
    breaks: true,      // 软换行转 <br>

    // 配置 highlight.js
    highlight: function (str, lang) {
        if (lang && hljs.getLanguage(lang)) {
            try {
                return hljs.highlight(str, { language: lang }).value;
            } catch (err) {
                console.error('Highlight error:', err);
            }
        }

        // 自动检测语言
        try {
            return hljs.highlightAuto(str).value;
        } catch (err) {
            console.error('Auto highlight error:', err);
        }

        // 如果高亮失败，返回原始内容
        return str;
    }
});

export function highlightElement(element) {
    if (!element || !element.querySelectorAll) return;
    element.querySelectorAll('pre code').forEach(block => {
        hljs.highlightElement(block);
    });
}

export function renderMarkdown1(markdown) {
    if (!markdown) return '';
    return marked.parse(markdown);
}

export function renderMarkdown2(markdown) {
    if (!markdown) return '';
    return md.render(markdown);
}
