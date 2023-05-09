using analisadorDePagamento.Interfaces.Services;
using analisadorDePagamento.Models;
using Newtonsoft.Json;

namespace analisadorDePagamento.Services
{
    public class JsonService : IJsonService
    {
        public void ConverterDepartamentosEmJson(List<Departamento> departamentos, string pasta)
        {
            string json = JsonConvert.SerializeObject(departamentos, Formatting.Indented);
            File.WriteAllText(Path.Combine(pasta, "resultado.json"), json); ;
        }
    }
}
