using analisadorDePagamento.Models;

namespace analisadorDePagamento.Interfaces.Services
{
    public interface IDepartamentoService
    {
        Task<Departamento> ProcessarDados(Departamento departamento);
    }
}
