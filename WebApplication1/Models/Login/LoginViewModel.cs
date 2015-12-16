using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEdenilsomWeb.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Campo login é Obrigatório.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O Campo senha é Obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

      
        [Display(Name = "Lembrar ?")]
        public bool Lembrar { get; set; }
    }
}