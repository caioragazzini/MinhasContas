using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Fatura
    {
        public int FaturaId { get; set; }
        public int EmissorId { get; set; }      
        public decimal ValorConta { get; set; }
        public DateTime DataFatura { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Status { get; set; }

        public Emissor emissors { get; set; }
    }
}