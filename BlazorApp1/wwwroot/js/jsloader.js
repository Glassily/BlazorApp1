// js/jsloader.js
// 这个函数负责动态加载markdown-it库并初始化
export async function initMarkdownIt() {
    // 如果全局已存在，直接使用（避免重复加载）
    if (window.markdownit) {
        return window.markdownit;
    }

    // 动态加载markdown-it脚本
    await loadScript('/js/markdown-it.min.js');

    // 等待库初始化完成
    return new Promise((resolve) => {
        const checkInterval = setInterval(() => {
            if (window.markdownit) {
                clearInterval(checkInterval);
                resolve(window.markdownit);
            }
        }, 10);
    });
}

// 解析Markdown的核心函数
export async function renderMarkdown(mdText) {
    const markdownit = await initMarkdownIt();
    const md = markdownit();
    return md.render(mdText);
}

// 动态加载脚本的辅助函数
function loadScript(src) {
    return new Promise((resolve, reject) => {
        const script = document.createElement('script');
        script.src = src;
        script.onload = () => resolve();
        script.onerror = () => reject(new Error(`加载脚本失败: ${src}`));
        document.head.appendChild(script);
    });
}