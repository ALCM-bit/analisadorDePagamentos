using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Interfaces.Repositories;
using analisadorDePagamento.Interfaces.Services;
using analisadorDePagamento.Models;

namespace analisadorDePagamento.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly ICsvServices _csvServices;
        public DataRepository(ICsvServices csvServices)
        {
            _csvServices = csvServices;
            
        }
        public async Task<List<Departamento>> GetDepartamentos(string pasta)
        {
            return await _csvServices.GetDadosCsv(pasta);
        }
    }
}