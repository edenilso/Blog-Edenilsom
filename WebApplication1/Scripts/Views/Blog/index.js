$(document).ready(function () {
    $('.excluir-post').on('click', function (e) {
        if (!confirm('Deseja realmente excluir este post?')){
            e.preventDefault();
        }
    });
});

$(document).ready(function () {
    $('.excluir-comentario').on('click', function (e) {
        if (!confirm('Deseja realmente excluir este comentario?')) {
            e.preventDefault();
        }
    });
});