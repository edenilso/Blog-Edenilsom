using BlogEdenilsom.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEdenilsomWeb.Models.Blog
{
    public class DetalhesPostsViewModel
    {
        
  public int Id { get; set; }
  public string Titulo { get; set; }
  public string Autor { get; set; }
  public string Resumo { get; set; }
  public string Descricao { get; set; }
  public DateTime DataPublicacao { get; set; }
  public int QtdeComentarios { get; set; }
  public bool Visivel { get; set; }
  public IList<TagClass> Tags { get; set; }

        /*CADASTRAR COMENTARIO*/
        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage ="O Campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O Campo nome é Obrigatorio!")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O Campo E-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage = "E-mail Inválido!")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O Campo nome é Obrigatorio!")]
        public string ComentarioDescricao { get; set; }

        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O Campo Página web deve possuir no máximo {1} caracteres!")]
        public string ComentarioPaginaWeb { get; set; }

    }
}