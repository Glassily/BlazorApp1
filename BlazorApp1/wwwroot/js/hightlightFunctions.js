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