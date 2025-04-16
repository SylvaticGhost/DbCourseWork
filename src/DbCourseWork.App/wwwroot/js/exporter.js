window.exportToPdf = function (elementId, filename) {
    console.log('export to pdf')
    const element = document.getElementById(elementId);

    const opt = {
        margin:       0,
        filename:     filename || 'report.pdf',
        image:        { type: 'jpeg', quality: 0.98 },
        html2canvas:  { scale: 2 },
        jsPDF: {
            unit: 'px',
            format: [element.offsetWidth, element.offsetHeight],
            orientation: 'portrait'
        }
    };

    html2pdf().set(opt).from(element).save();
};
