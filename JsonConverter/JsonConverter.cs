using analisadorDePagamento.Models;
using Newtonsoft.Json;

namespace analisadorDePagamento.JsonConverter
{
    public class JsonConverter : IJsonConverter
    {
        public void ConverterDepartamentosEmJson(List<Departamento> departamentos, string pasta)
        {
            string json = JsonConvert.SerializeObject(departamentos, Formatting.Indented);
            File.WriteAllText(Path.Combine(pasta, "resultado.json"), json); ;
        }
    }
}
