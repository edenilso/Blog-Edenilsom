using BlogEdenilsom.DB;
using BlogEdenilsom.DB.Classes;
using BlogEdenilsomWeb.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdenilsomWeb.Controllers
{
    [Authorize]
    public class AdministracaoController : Controller
    {
       // GET: Administracao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarPost()
        {
            var viewmodel = new CadastrarPostViewModel();
            viewmodel.DataPublicacao = DateTime.Now;
            viewmodel.HoraPublicacao = DateTime.Now;
        //    viewmodel.Autor = "edenilson";
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                           
            var conexao = new ConexaoBanco();
            var post = new Post();
            var dataConc = new DateTime(
               viewmodel.DataPublicacao.Year,
               viewmodel.DataPublicacao.Month,
               viewmodel.DataPublicacao.Day,
               viewmodel.HoraPublicacao.Hour,
               viewmodel.DataPublicacao.Minute,
               viewmodel.DataPublicacao.Second);
            post.Titulo = viewmodel.Titulo;
            post.Autor = viewmodel.Autor;
            post.Resumo = viewmodel.Resumo;
            post.Descricao = viewmodel.Descricao;
            post.Visivel = viewmodel.Visivel;
            post.DataPublicacao = dataConc;
                post.TagPost = new List<TagPost>();
                if (viewmodel.Tags != null)
                {
                    foreach (var item in viewmodel.Tags)
                    {
                        var tagExiste = (from p in conexao.TagClass
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass);
                        }

                        var tagPost = new TagPost();
                        tagPost.Tag = item;
                        post.TagPost.Add(tagPost);

                    }
                }


                try
                {
                    conexao.Posts.Add(post);
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {

                    ModelState.AddModelError("Ocorreu um erro no banco ao Incluir", exp.Message); ;
                }

            
            }
            return View(viewmodel);


        }

        public ActionResult EditarPost(int id)
        {
            var conexao = new ConexaoBanco();

            var post = (from x in conexao.Posts
                         where x.Id == id
                         select x).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post com o codigo  {0} não encontrado.", id));
            }
            var viewmodel = new CadastrarPostViewModel();
            viewmodel.DataPublicacao = post.DataPublicacao;
            viewmodel.HoraPublicacao = post.DataPublicacao;
            viewmodel.Autor = post.Autor;
            viewmodel.Titulo = post.Titulo;
            viewmodel.Resumo = post.Resumo;
            viewmodel.Descricao = post.Descricao;
            viewmodel.Visivel = post.Visivel;
            viewmodel.Id = post.Id;
            viewmodel.Tags = (from p in post.TagPost
                              select p.Tag).ToList();

            

            return View(viewmodel);

            

        }


        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                var post = (from x in conexao.Posts
                            where x.Id == viewmodel.Id
                            select x).FirstOrDefault();

                post.Resumo = viewmodel.Resumo;
                post.Titulo = viewmodel.Titulo;
                post.Autor = viewmodel.Autor;
                post.Descricao = viewmodel.Descricao;
                post.Visivel = viewmodel.Visivel;

                var dataConc = new DateTime(
                viewmodel.DataPublicacao.Year,
                viewmodel.DataPublicacao.Month,
                viewmodel.DataPublicacao.Day,
                viewmodel.HoraPublicacao.Hour,
                viewmodel.DataPublicacao.Minute,
                viewmodel.DataPublicacao.Second);
                post.DataPublicacao = dataConc;

                var postsTagsAtuais = post.TagPost.ToList();
                foreach (var item in postsTagsAtuais)
                {
                    conexao.TagPosts.Remove(item);

                }

                if (viewmodel.Tags != null)
                {
                    foreach (var item in viewmodel.Tags)
                    {
                        var tagExiste = (from p in conexao.TagClass
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass);
                        }

                        var tagPost = new TagPost();
                        tagPost.Tag = item;
                        post.TagPost.Add(tagPost);

                    }
                }



                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {

                    ModelState.AddModelError("Ocorreu um erro no banco ao salvar", exp.Message);
                }
                
            }
            return View(viewmodel);


        }

        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post com o Código  {0} Não Existe.", id));
            }
            conexao.Posts.Remove(post);
            conexao.SaveChanges();

            return RedirectToAction("Index","Blog");
        }
        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion
    }
}