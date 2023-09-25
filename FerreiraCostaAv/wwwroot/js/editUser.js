$(document).ready(function () {
    $('.edit-user-button').click(function () {
        var userId = $(this).data('user-id');
        $.ajax({
            type: 'GET',
            url: '/User/EditUser',
            data: { id: userId },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (user) {
                $('#CredentialDTO_Login').val(user.Credential.Login);
                $('#CredentialDTO_Password').val(user.Credential.Password);
                $('#Email').val(user.Email);
                $('#PhoneNumber').val(user.PhoneNumber);
                $('#Cpf').val(user.Cpf);
                $('#BirthDate').val(user.BirthDate);
                $('#MothersName').val(user.MothersName);
                $('#Status').val(user.Status);
            },
            error: function (error) {
                alert('Erro ao obter os dados do usuário: ' + error.responseText);
            }
        });
    });
});
