using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEdenilsomWeb.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("Código")]
        public int id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Campo Nome é Obrigatorio.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A Quantidade de caracteres no campo Nome deve ser entre {2} e {1}.")]
        public string Nome { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O Campo Login é Obrigatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A Quantidade de caracteres no campo Login deve ser entre {2} e {1}.")]
        public string Login { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é Obrigatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A Quantidade de caracteres no Senha deve ser entre {2} e {1}.")]
        public string Senha { get; set; }

        [DisplayName("Confirmar Senha")]
        [Required(ErrorMessage = "O campo Senha é Obrigatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A Confirmar senha deve possuir no maximo {1} caracteres.")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não conferem.")]
        public string confirmarSenha { get; set; }
    }
}