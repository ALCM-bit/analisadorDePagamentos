using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Repositories
{
    public interface IDataRepository
    {
        List<Departamento> GetDepartamentos(string pasta);
    }
}