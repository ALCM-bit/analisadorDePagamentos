using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Services
{
    public interface ICsvServices
    {
        List<Departamento> GetDadosCsv(string pasta);
    }
}