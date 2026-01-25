// 用于处理Markdown渲染和代码高亮的JavaScript函数

window.prismFunctions = {
    highlightAll: function () {
        if (typeof Prism !== 'undefined') {
            // 确保所有代码块都被高亮
            Prism.highlightAll();
        }
    },

    highlightElement: function (element) {
        if (typeof Prism !== 'undefined' && element) {
            Prism.highlightElement(element);
        }
    }
};

//window.updatePreview = (element, markdown) => {
//    const md = window.markdownit();
//    element.innerHTML = md.render(markdown);
//};