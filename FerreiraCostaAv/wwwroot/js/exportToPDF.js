function exportToPDF() {
    var doc = new jsPDF();

    var x = 10;
    var y = 10;
    var fontSize = 12;

    var headers = [];
    $('#userTable th').each(function () {
        headers.push($(this).text());
    });

    var data = [];
    $('#userTable tbody tr').each(function () {
        var rowData = [];
        $(this).find('td').each(function () {
            rowData.push($(this).text());
        });
        data.push(rowData);
    });

    doc.setFontSize(fontSize);
    doc.text('Lista de Usuários', x, y);
    y += 10;
    doc.autoTable({
        head: [headers],
        body: data,
        startY: y
    });

    doc.save('lista_usuarios.pdf');
}

window.exportToPDF = exportToPDF;
