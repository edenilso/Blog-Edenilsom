using BlogEdenilsom.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdenilsom.DB.Classes
{
    public class Visita : ClasseBase
    {
     //   public int IdVisita { get; set; }
        public string Ip { get; set; }
        public DateTime DataHora { get; set; }
        public int IdPost { get; set; }

        public virtual Post Post { get; set; }
    }
}
