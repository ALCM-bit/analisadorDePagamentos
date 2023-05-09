using analisadorDePagamento.Models;

namespace analisadorDePagamento.Interfaces.Services
{
    public interface IJsonService
    {
        void ConverterDepartamentosEmJson(List<Departamento> departamentos, string pasta);
    }
}
