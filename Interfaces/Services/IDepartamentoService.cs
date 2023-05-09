using analisadorDePagamento.Models;

namespace analisadorDePagamento.Interfaces.Services
{
    public interface IDepartamentoService
    {
        Departamento ProcessarDados(Departamento departamento);
    }
}
