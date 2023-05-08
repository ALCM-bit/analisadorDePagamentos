using analisadorDePagamento.Models;

namespace analisadorDePagamento.Services
{
    public interface IDepartamentoService
    {
        Departamento ProcessarDados(Departamento departamento);
    }
}
