using BlogEdenilsom.DB;
using BlogEdenilsom.DB.Classes;
using BlogEdenilsomWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdenilsomWeb.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        
        public ActionResult index()
        {
            var conexao = new ConexaoBanco();

            var usuario = (from x in conexao.Usuarios
                           orderby x.Nome
                           select x).ToList();
            return View(usuario);
        }

        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        // GET: Usuario
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.Login = viewmodel.Login.ToUpper();
                usuario.Nome = viewmodel.Nome;
                usuario.Senha = viewmodel.Senha;

                conexao.Usuarios.Add(usuario);
                try
                {
                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já Existe Login Cadastrado com o login {0}.", usuario.Login));
                    }

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

        public ActionResult EditarUsuario(int id)
        {
            var conexao = new ConexaoBanco();

            var usuario = (from x in conexao.Usuarios
                        where x.Id == id
                        select x).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("login com o codigo  {0} não encontrado.", id));
            }
            var viewmodel = new CadastrarUsuarioViewModel();
            viewmodel.Login = usuario.Login.ToUpper();
            viewmodel.Nome = usuario.Nome;
            viewmodel.Senha = usuario.Senha;
            viewmodel.id = usuario.Id;

            return View(viewmodel);

                   
        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                var usuario = (from x in conexao.Usuarios
                            where x.Id == viewmodel.id
                            select x).FirstOrDefault();

                viewmodel.id = usuario.Id;
                usuario.Login = viewmodel.Login.ToUpper();
                usuario.Nome = viewmodel.Nome;
                usuario.Senha = viewmodel.Senha;

                try
                {

                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    && p.Id != usuario.Id
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já Existe Login Cadastrado com o login {0}.", usuario.Login));
                    }

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

        public ActionResult ExcluirUsuario(int id)
        {
            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios
                        where p.Id == id
                        select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuario com o Código  {0} Não Existe.", id));
            }
            conexao.Usuarios.Remove(usuario);
            conexao.SaveChanges();

            return RedirectToAction("Index");
        }


       
    }
}