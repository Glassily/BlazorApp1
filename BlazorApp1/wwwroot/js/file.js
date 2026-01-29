window.saveTextAsFile = async (fileName, content) => {
    if (!window.showSaveFilePicker) {
        fallbackSave(fileName, content); // 降级方案
        return;
    }
    try {
        const handle = await window.showSaveFilePicker({
            suggestedName: fileName,
            types: [{ description: 'Markdown', accept: { 'text/markdown': ['.md'] } }]
        });
        const writable = await handle.createWritable();
        await writable.write(new Blob([content], { type: 'text/markdown' }));
        await writable.close();
    } catch (e) {
        if (e.name !== 'AbortError') fallbackSave(fileName, content);
    }
};

function fallbackSave(fileName, content) {
    const blob = new Blob([content], { type: 'text/markdown;charset=utf-8' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    setTimeout(() => {
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
    }, 0);
}