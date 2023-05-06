using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace analisadorDePagamento.Models
{
    public class FolhaPonto
    {
        public string? Nome { get; set; }
        public int Codigo { get; set; }
        public Decimal ValorHora { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan Saida { get; set; }
        public TimeSpan IniciAlmoco { get; set; }
        public TimeSpan TerminoAlmoco { get; set; }
    }
}