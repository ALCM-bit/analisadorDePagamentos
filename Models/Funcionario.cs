using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace analisadorDePagamento.Models
{
    public class Funcionario
    {
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public Decimal TotalReceber { get; set; }
        public Decimal HorasExtra { get; set; }
        public Decimal HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public int DiasTrabalhados { get; set; }

    }
}