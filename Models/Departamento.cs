using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace analisadorDePagamento.Models
{
    public class Departamento
    {
        public string? Nome { get; set; }
        public string? MesVigencia { get; set; }
        public string? AnoVigencia { get; set; }
        public Decimal TotalPagar { get; set; }
        public Decimal TotalDescontos { get; set; }
        public Decimal TotalExtras { get; set; }

        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    }
}