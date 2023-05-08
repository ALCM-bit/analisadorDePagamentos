using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        public List<Funcionario> CalculaDados(List<FolhaPonto> ponto)
        {
            List<Funcionario> funcionariosProcessados = new List<Funcionario>();

            // Processar folhas de ponto e calcular dados dos funcionários
            foreach (FolhaPonto folha in ponto) 
            {
                // Verificar se já existe um funcionário com o mesmo código
                Funcionario funcionarioExistente = funcionariosProcessados.Find(f => f.Codigo == folha.Codigo);
                //pega horas trabalhadas
                TimeSpan horasTrabalhadas = folha.Saida - folha.Entrada - (folha.TerminoAlmoco - folha.IniciAlmoco);
                // Se o funcionário ainda não foi processado, criar um novo objeto Funcionario
                if (funcionarioExistente == null) {
                    Funcionario funcionario = new Funcionario();
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
                    // Adicionar dias trabalhados e dias de falta
                    funcionarioExistente.DiasTrabalhados++;

                }
                //Adiciona funcionario a lista processada
               // funcionariosProcessados.Add(funcionarioExistente);
            }

            // Calcular dias extras e dias de falta
            foreach (Funcionario funcionario in funcionariosProcessados) {
                if (funcionario != null)
                {
                    // Verificar se o funcionário trabalhou mais de 22 dias no mês
                if (funcionario.DiasTrabalhados > 22) {
                    funcionario.DiasExtras = funcionario.DiasTrabalhados - 22;
                }

                // Verificar se o funcionário trabalhou em todos os dias úteis do mês (considerando 22 dias úteis)
                int diasFalta = 22 - funcionario.DiasTrabalhados;
                if (diasFalta > 0) {
                    funcionario.DiasFalta = diasFalta;
                }

                }
                
            }
            return funcionariosProcessados;
        }
    }
}