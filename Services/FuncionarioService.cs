using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Interfaces.Services;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        public async Task<List<Funcionario>> CalculaDados(List<FolhaPonto> ponto)
        {
            int diasUteis = VerificarDiasUteis();
            List<Funcionario> funcionariosProcessados = new List<Funcionario>();

            await Task.Run(() => 
            { 

            // Processar folhas de ponto e calcular dados dos funcionários
            foreach (FolhaPonto folha in ponto)
            {
                // Verificar se já existe um funcionário com o mesmo código
                Funcionario funcionarioExistente = funcionariosProcessados.Find(f => f.Codigo == folha.Codigo);
                //pega horas trabalhadas
                TimeSpan horasTrabalhadas = folha.Saida - folha.Entrada - (folha.TerminoAlmoco - folha.IniciAlmoco);
                //cria objeto funcionario que será usado na lógica
                Funcionario funcionario = new Funcionario();
                // Se o funcionário ainda não foi processado, criar um novo objeto Funcionario
                if (funcionarioExistente == null)
                {
                    funcionario.Nome = folha.Nome;
                    funcionario.Codigo = folha.Codigo;
                    funcionario.TotalReceber = 0;
                    funcionario.HorasExtra = 0;
                    funcionario.HorasDebito = 0;
                    funcionario.DiasTrabalhados = 0;
                    funcionario.DiasExtras = 0;
                    funcionario.DiasFalta = 0;

                    // Adicionar horas trabalhadas
                    if (horasTrabalhadas.TotalHours > 8)
                    {
                        funcionario.HorasExtra += (decimal)horasTrabalhadas.TotalHours - 8;
                    }
                    else
                    {
                        funcionario.HorasDebito = (decimal)(8 - horasTrabalhadas.TotalHours);
                    }
                    funcionario.TotalReceber += (decimal)horasTrabalhadas.TotalHours * folha.ValorHora;
                    // Adicionar dias trabalhados e dias de falta
                    funcionario.DiasTrabalhados++;

                    //Adiciona o funcionário a nova lista
                    funcionariosProcessados.Add(funcionario);
                }
                else
                {
                    // Adicionar horas trabalhadas
                    if (horasTrabalhadas.TotalHours > 8)
                    {
                        funcionarioExistente.HorasExtra += (decimal)horasTrabalhadas.TotalHours - 8;
                    }
                    else
                    {
                        funcionarioExistente.HorasDebito = (decimal)(8 - horasTrabalhadas.TotalHours);
                    }
                    funcionarioExistente.TotalReceber += (decimal)horasTrabalhadas.TotalHours * folha.ValorHora;
                    funcionarioExistente.TotalReceber -= (decimal)funcionario.HorasDebito * folha.ValorHora;
                    // Adicionar dias trabalhados e dias de falta
                    funcionarioExistente.DiasTrabalhados++;

                }

            }

            // Calcular dias extras e dias de falta
            foreach (Funcionario funcionario in funcionariosProcessados)
            {
                if (funcionario != null)
                {
                    // Verificar se o funcionário trabalhou mais de 22 dias no mês
                    if (funcionario.DiasTrabalhados > diasUteis)
                    {
                        funcionario.DiasExtras = funcionario.DiasTrabalhados - diasUteis;
                    }

                    // Verificar se o funcionário trabalhou em todos os dias úteis do mês (considerando 22 dias úteis)
                    int diasFalta = diasUteis - funcionario.DiasTrabalhados;
                    if (diasFalta > 0)
                    {
                        funcionario.DiasFalta = diasFalta;
                    }

                }

            }
            });
            return funcionariosProcessados;
        }

        public int VerificarDiasUteis()
        {
            // Obter o mês anterior ao atual
            var mesAnterior = DateTime.Now.AddMonths(-1);

            // Criar um objeto CultureInfo para o Brasil, onde a semana começa na segunda-feira
            var culturaBrasil = new CultureInfo("pt-BR");

            // Contar o número de dias úteis do mês anterior
            var totalDiasUteis = 0;
            for (var dia = 1; dia <= DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month); dia++)
            {
                var data = new DateTime(mesAnterior.Year, mesAnterior.Month, dia);
                if (culturaBrasil.DateTimeFormat.DayNames[(int)data.DayOfWeek] != "sábado" &&
                    culturaBrasil.DateTimeFormat.DayNames[(int)data.DayOfWeek] != "domingo")
                {
                    totalDiasUteis++;
                }
            }

            return totalDiasUteis;
        }
    }
}