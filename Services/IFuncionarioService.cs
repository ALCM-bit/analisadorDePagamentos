using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Services
{
    public interface IFuncionarioService
    {
        List<Funcionario> CalculaDados(List<FolhaPonto> ponto);
    }
}