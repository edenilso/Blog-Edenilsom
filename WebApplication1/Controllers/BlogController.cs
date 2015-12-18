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

        public ActionResult Post(int id, int? pagina)
        {
            var conexaoBanco = new ConexaoBanco();

            var conexao = new ConexaoBanco();
            var post = (from x in conexao.Posts
                        where x.Id == id
                        select x).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado."));
            }
            var viewModel = new DetalhesPostsViewModel();
            preencherViewModel(viewModel, post, pagina);

            return View(viewModel);
        }

        private static void preencherViewModel(DetalhesPostsViewModel viewModel, Post post, int? pagina)
        {
            viewModel.Id = post.Id;
            viewModel.Autor = post.Autor;
            viewModel.Resumo = post.Resumo;
            viewModel.Titulo = post.Titulo;
            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.Descricao = post.Descricao;
            viewModel.Visivel = post.Visivel;
            viewModel.QtdeComentarios = post.Comentarios.Count;
            viewModel.Tags = post.TagPost.Select(x => x.TagClass).ToList();
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;
            var qtdeRegistros = post.Comentarios.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistroPular = (indiceDaPagina * registroPorPagina);
            var qtadePaginas = Math.Ceiling((decimal)qtdeRegistros / registroPorPagina);
            viewModel.Comentario = (from p in post.Comentarios
                                     orderby p.DataHora descending
                                     select p).Skip(qtdeRegistroPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtadePaginas;


        }

        public ActionResult Index(int? pagina, string tag, string pesquisa)
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
                         where p.TagPost.Any(x => x.Tag.ToUpper() == tag.ToUpper())
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
                               select new DetalhesPostsViewModel
                               {
                                   DataPublicacao = p.DataPublicacao,
                                   Autor = p.Autor,
                                   Descricao = p.Descricao,
                                   Id = p.Id,
                                   Resumo = p.Resumo,
                                   Titulo = p.Titulo,
                                   Visivel = p.Visivel,
                                   QtdeComentarios = p.Comentarios.Count
                               }).Skip(qtdeRegistroPular).Take(registroPorPagina).ToList();
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
        [HttpPost]
        public ActionResult Post(DetalhesPostsViewModel viewModel)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                        where p.Id == viewModel.Id
                        select p).FirstOrDefault();

            if (ModelState.IsValid)
            {

                if (post == null)
                {
                    throw new Exception(string.Format("Post Código {0} não encontrado.", viewModel.Id));
                }
                var comentario = new Comentario();
                comentario.AdmPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.Descricao = viewModel.ComentarioDescricao;
                comentario.Email = viewModel.ComentarioEmail;
                comentario.IdPost = viewModel.Id;
                comentario.Nome = viewModel.ComentarioNome;
                comentario.PaginaWeb = viewModel.ComentarioPaginaWeb;
                comentario.DataHora = DateTime.Now;

                try
                {
                    conexao.Comentarios.Add(comentario);
                    conexao.SaveChanges();
                    return Redirect(Url.Action("Post", new
                    {
                        ano = post.DataPublicacao.Year,
                        mes = post.DataPublicacao.Month,
                        dia = post.DataPublicacao.Day,
                        titulo = post.Titulo,
                        id = viewModel.Id
                    })+ "#comentarios");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            preencherViewModel(viewModel, post, null);
            return View(viewModel);
        }

        public ActionResult _PaginacaoPost()
        {
            return PartialView();
        }
    }
}