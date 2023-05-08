using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using analisadorDePagamento.JsonConverter;
using analisadorDePagamento.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace analisadorDePagamento.Services
{
    public class CsvServices : ICsvServices
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IJsonConverter _jsonConverter;
        public CsvServices(IFuncionarioService funcionarioService, IDepartamentoService departamentoService, IJsonConverter jsonConverter)
        {
            _funcionarioService = funcionarioService;
            _departamentoService = departamentoService;
            _jsonConverter = jsonConverter;
        }

        

        public List<Departamento> GetDadosCsv(string pasta)
        {
            var departamentos = new List<Departamento>();
            var files = Directory.GetFiles(@$"{pasta}", "*.csv");

            foreach (var file in files)
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                var parts = filename.Split("-");

                var departamento = new Departamento
                {
                    Nome = parts[0],
                    MesVigencia = parts[1],
                    AnoVigencia = parts[2]
                };
                
                
                
                //Depois de criar o departamento - Analisar os dados do ponto é necessário
                var configuration = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true,
                    // TypeConverterOptions = { NumberStyles = NumberStyles.AllowDecimalPoint },
                    // CultureInfo = CultureInfo.GetCultureInfo("pt-BR")
                };
                using var reader = new StreamReader(file, Encoding.GetEncoding("iso-8859-1"));
                using var csv = new CsvReader(reader, configuration);
                var folhasPonto = new List<FolhaPonto>();
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {
                    var horarios = record.Almoço.Split('-');
                    var iniciAlmoco = TimeSpan.ParseExact(horarios[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture);
                    var terminoAlmoco = TimeSpan.ParseExact(horarios[1].Trim(), "hh\\:mm", CultureInfo.InvariantCulture);

                    var codigo = int.Parse(record.Código);
                    var valorHora = decimal.Parse(record.Valorhora.Replace("R$", "").Replace(" ", "").Trim(), CultureInfo.GetCultureInfo("pt-BR"));
                    var data = DateTime.ParseExact(record.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var entrada = TimeSpan.ParseExact(record.Entrada, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
                    var saida = TimeSpan.ParseExact(record.Saída, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);

                    var folhaPonto = new FolhaPonto
                    {
                        Nome = record.Nome,
                        Codigo = codigo,
                        ValorHora = valorHora,
                        Data = data,
                        Entrada = entrada,
                        Saida = saida,
                        IniciAlmoco = iniciAlmoco,
                        TerminoAlmoco = terminoAlmoco
                    };

                    folhasPonto.Add(folhaPonto);
                }
                folhasPonto.RemoveAll(item => item == null);
                var funcionariosProcessados = _funcionarioService.CalculaDados(folhasPonto);
                funcionariosProcessados.RemoveAll(item => item == null);
                departamento.Funcionarios.AddRange(funcionariosProcessados);
                var departamentoProcessado = _departamentoService.ProcessarDados(departamento);
                departamentos.Add(departamento);
            }
            _jsonConverter.ConverterDepartamentosEmJson(departamentos, pasta);
            return departamentos;
        }
    }
}