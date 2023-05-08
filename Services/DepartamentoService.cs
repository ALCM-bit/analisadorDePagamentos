using analisadorDePagamento.Models;

namespace analisadorDePagamento.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        public Departamento ProcessarDados(Departamento departamento)
        {
            var totalPagar = departamento.Funcionarios.Sum(fs => fs.TotalReceber);
            var totalDescontos = departamento.Funcionarios
                .Select(f => f.HorasDebito * (f.TotalReceber/f.DiasTrabalhados))
                .Sum();
            var totalExtra = departamento.Funcionarios
                .Select(f => f.HorasExtra * (f.TotalReceber/f.DiasTrabalhados))
                .Sum();

            departamento.TotalPagar = totalPagar;
            departamento.TotalDescontos = totalDescontos;
            departamento.TotalExtras = totalExtra;
            return departamento;
        }
    }
}
