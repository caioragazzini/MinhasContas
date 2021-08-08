using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Emissor
    {
        public int EmissorId { get; set; }
        public string Nome { get; set; }

        public virtual Categoria Categoria { get; set; }
        //public virtual List<Fatura> Faturas { get; set; }


    }
    
}
