using BlogEdenilsom.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdenilsom.DB.Classes
{
    public class TagPost : ClasseBase
    {
        public int IdPost { get; set; }
        public string Tag { get; set; }

        public virtual Post Post { get; set; }
        public virtual TagClass TagClass { get; set; }
    }
}
