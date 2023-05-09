using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Interfaces.Repositories
{
    public interface IDataRepository
    {
        Task<List<Departamento>> GetDepartamentos(string pasta);
    }
}