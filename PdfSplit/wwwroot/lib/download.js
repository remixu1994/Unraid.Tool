window.saveFile = async function (content, fileName) {
    try {
        const opts = {
            suggestedName: fileName,
            StartIn: "Downloads",
            types: [{description: 'Text Files', accept: {'text/plain': ['.pdf',]}}]
        };
        const handle = await window.showSaveFilePicker(opts);
        const writable = await handle.createWritable();
        await writable.write(new Blob([content], {type: "application/octet-stream"}));
        await writable.close();
        alert(`File "${fileName}" has been saved successfully!`);
    } catch (err) {
        console.error("Save file failed.", err);
    }
}