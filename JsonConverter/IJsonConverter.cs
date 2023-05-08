using analisadorDePagamento.Models;

namespace analisadorDePagamento.JsonConverter
{
    public interface IJsonConverter
    {
        void ConverterDepartamentosEmJson(List<Departamento> departamentos, string pasta);
    }
}
