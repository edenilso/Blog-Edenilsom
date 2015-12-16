using BlogEdenilsom.DB;
using BlogEdenilsom.DB.Classes;
using BlogEdenilsomWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdenilsomWeb.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog

            public ActionResult Post (int id)
        {
            var conexaoBanco = new ConexaoBanco();

            var conexao = new ConexaoBanco();
            var post = (from x in conexao.Posts
                        where x.Id == id
                        select x).FirstOrDefault();

            var viewModel = new DetalhesPostsViewModel();

            viewModel.Resumo = post.Resumo;
            viewModel.Titulo = post.Titulo;
            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.Autor = post.Autor;
            viewModel.Descricao = post.Descricao;
            viewModel.Visivel = post.Visivel;
            viewModel.Id = post.Id;
            viewModel.QtdeComentarios = post.Comentarios.Count;
            viewModel.Tags = (from x in post.TagPost
                              select x.TagClass).ToList();


            return View(viewModel);
        }

       
        public ActionResult Index(int? pagina, string tag, string pesquisa )
        {
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;

            var conexaoBanco = new ConexaoBanco();

            var posts = (from p in conexaoBanco.Posts
                         where p.Visivel == true
                         select p);
            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.TagPost.Any(x => x.Tag.ToUpper() == tag.ToUpper() )
                        select p);
            }
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts
                         where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }

            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistroPular = (indiceDaPagina * registroPorPagina);
            var qtadePaginas = Math.Ceiling((decimal)qtdeRegistros / registroPorPagina);

            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select p).Skip(qtdeRegistroPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtadePaginas;
            viewModel.Tag = tag;
            viewModel.Tags = (from p in conexaoBanco.TagClass
                              where conexaoBanco.TagPosts.Any(x => x.Tag == p.Tag)
                              orderby p.Tag ascending
                              select p.Tag).ToList();
            viewModel.Pesquisa = pesquisa;
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }
    }
}