function deleteSelectedUsers() {
    var selectedUserIds = [];
    $('input[name="userIds"]:checked').each(function () {
        selectedUserIds.push(parseInt($(this).val()));
    });

    if (selectedUserIds.length === 0) {
        alert('Selecione pelo menos um usuário para excluir.');
        return;
    }

    if (confirm('Tem certeza que deseja excluir os usuários selecionados?')) {
        $.ajax({
            type: 'POST',
            url: '/User/deleteUsers',
            data: JSON.stringify(selectedUserIds),
            contentType: 'application/json',
            success: function (data) {
                alert(data);
            },
            error: function (error) {
                alert('Erro ao excluir os usuários: ' + error.responseText);
            }
        });
    }
}