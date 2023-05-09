using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Interfaces.Services
{
    public interface IFuncionarioService
    {
        Task<List<Funcionario>> CalculaDados(List<FolhaPonto> ponto);
        int VerificarDiasUteis();
    }
}