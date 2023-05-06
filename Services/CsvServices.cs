using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;
using CsvHelper;

namespace analisadorDePagamento.Services
{
    public class CsvServices : ICsvServices
    {
        public List<FolhaPonto> GetDadosCsv(string pasta)
        {
            var departamentos = new List<Departamento>();
            var files = Directory.GetFiles(@$"{pasta}", "*.csv");

            foreach (var file in files)
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                var parts = filename.Split("_");

                var departamento = new Departamento
                {
                    Nome = parts[0],
                    MesVigencia = parts[1],
                    AnoVigencia = parts[2]
                };

                departamentos.Add(departamento);

                //Depois de criar o departamento - Analisar os dados do ponto é necessário
                var folhasPonto = new List<FolhaPonto>();
                using var reader = new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                // ignore a primeira linha do arquivo csv
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;

                var records = csv.GetRecords<dynamic>();
                foreach (var record in records.Skip(1))
                {
                    var folhaPonto = new FolhaPonto
                    {
                        Nome = record.Nome,
                        Codigo = record.Codigo,
                        ValorHora = record.ValorHora,
                        Data = record.Data,
                        Entrada = record.Entrada,
                        Saida = record.Saida,
                        IniciAlmoco = record.IniciAlmoco,
                        TerminoAlmoco = record.TerminoAlmoco
                    };

                    folhasPonto.Add(folhaPonto);
                }
            }
        }
    }
}