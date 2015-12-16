$(document).ready(function () {
    var Tags = new Array();

    $("#adcionar").on('click', function () {
        var textoTag = $('#Tag').val();
        var existe = Tags.filter(function (v) {
            return v.Tag.toLowerCase() === textoTag.toLocaleLowerCase();
        })[0];
        if (existe != undefined) {
            alert('Está Tag já foi informada.');
            return;
        }
        Tags.push(new Object({ Tag: textoTag }));
        if (textoTag.trim() === '') {
            alert('o campo Tag é obrigatório.');
            $('#Tag').focus();
            return;
        }
        montaListapeloArray();
        $('#Tag').val('');
        $('#Tag').focus();
    });

    function montaListapeloArray() {
        var form = $('form');

        $('input[Name="Tags"]').remove();
        $('#resultado').empty();
        $(Tags).each(function () {

            $('#resultado').append('<li><span>' + this.Tag + '</span><a tag= "' + this.Tag + '"class="remover-tag icone-excluir" title="Remover"></a></li>');
            form.append('<input name="Tags" type="hidden" value="' + this.Tag + '" />');
        });
    }
    $('body').on('click', '.remover-tag', function () {
        var tag = $(this).attr('tag');

        Tags = $.grep(Tags, function (e) {
            return e.Tag !== tag;
        });

        montaListapeloArray();
              
    });

    $("input[Name='Tags']")
        .map(function () {
            var tag = $(this).val();
            Tags.push(new Object({ Tag: tag }));
        }).get();

});