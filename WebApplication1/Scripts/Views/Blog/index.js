$(document).ready(function () {
    $('.excluir-post').on('click', function (e) {
        if (!confirm('Deseja relamente excluir este post?')){
            e.preventDefault();
        }
    });
});