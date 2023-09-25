function exportToExcel() {
    var wb = XLSX.utils.table_to_book(document.getElementById('userTable'), { sheet: "Usuários" });
    var blob = XLSX.write(wb, { bookType: "xlsx", type: "blob" });
    var url = URL.createObjectURL(blob);
    var a = document.createElement("a");
    a.href = url;
    a.download = "lista_usuarios.xlsx";
    a.click();
}

window.exportToExcel = exportToExcel;
