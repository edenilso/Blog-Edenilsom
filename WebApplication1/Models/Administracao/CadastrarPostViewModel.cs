using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEdenilsomWeb.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "O Campo Titulo é Obrigatorio.")]
        [StringLength(100,MinimumLength = 2, ErrorMessage ="A Quantidade de caracteres no campo titulo deve ser entre {2} e {1}.")]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "O Campo Autor é Obrigatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A Quantidade de caracteres no campo autor deve ser entre {2} e {1}.")]
        public string Autor { get; set; }

        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O campo Resumo é Obrigatorio.")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "A Quantidade de caracteres no Resumo titulo deve ser entre {2} e {1}.")]
        public string Resumo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é Obrigatorio.")]
        public string Descricao { get; set; }

        [DisplayName("Data Publicação")]
        [Required(ErrorMessage = "O campo Data da Publicação é Obrigatorio.")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora Publicação")]
        [Required(ErrorMessage = "O campo Hora da Publicação é Obrigatorio.")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Visível")]
        [Required(ErrorMessage = "O campo Visível é Obrigatorio.")]
        public bool Visivel { get; set; }

        public List<string> Tags { get; set; }


    }
}