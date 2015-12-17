using BlogEdenilsom.DB.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdenilsom.DB.Infra
{
    public class MeuCriadorDeBanco: CreateDatabaseIfNotExists<ConexaoBanco>
    {
        protected override void Seed(ConexaoBanco context)
        {
            context.Usuarios.Add(new Usuario { Login = "ADM", Nome = "Administrador", Senha = "admin"});

            base.Seed(context);
        }
    }
}
