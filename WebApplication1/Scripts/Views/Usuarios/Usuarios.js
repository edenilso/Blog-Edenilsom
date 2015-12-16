$(document).ready(function () {
    $('.excluir-usuario').on('click', function (e) {
        if (!confirm('Deseja relamente excluir este post?')) {
            e.preventDefault();
        }
    });
});