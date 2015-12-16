using BlogEdenilsom.DB;
using BlogEdenilsomWeb.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogEdenilsomWeb.Controllers
{  [Authorize]
    public class LoginController : Controller
    {
        #region Index
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index (LoginViewModel ViewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios
                           where p.Login.ToUpper() == ViewModel.Login.ToUpper()
                           && p.Senha == ViewModel.Senha
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário e/ou senha estão incorretos.");
                return View(ViewModel);
            }

            FormsAuthentication.SetAuthCookie(usuario.Login, ViewModel.Lembrar);
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index","Blog");
        }
        #endregion

        #region sair
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        #endregion
    }
}