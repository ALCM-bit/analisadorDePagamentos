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
                    TimeSpan horasTrabalhadas = folha.Saida - folha.Entrada - (folha.IniciAlmoco - folha.TerminoAlmoco);
                    if (horasTrabalhadas.TotalHours > 8) {
                        funcionario.HorasExtra += (decimal)horasTrabalhadas.TotalHours - 8;
                        funcionario.TotalReceber += (8 * folha.ValorHora) + ((decimal)(horasTrabalhadas.TotalHours - 8) * folha.ValorHora * 1.5M);
                    }
                    else {
                        funcionario.TotalReceber +=(decimal)horasTrabalhadas.TotalHours * folha.ValorHora;
                    }

                    // Adicionar dias trabalhados e dias de falta
                    funcionario.DiasTrabalhados++;
                    funcionariosProcessados.Add(funcionario);
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