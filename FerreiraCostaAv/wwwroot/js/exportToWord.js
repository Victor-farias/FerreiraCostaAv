function exportToWord() {
    var table = document.getElementById('userTable');

    var options = {
        styleMap: [
            "table => table",
            "tr => tr",
            "td => td",
            "th => th",
        ],
    };

    mammoth.convertToHtml({ element: table }, options)
        .then(function (result) {
            var blob = new Blob([result.value], { type: "application/msword" });
            var url = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = url;
            a.download = "lista_usuarios.docx";
            a.click();
        })
        .catch(function (err) {
            console.error(err);
        });
}

window.exportToWord = exportToWord;