using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Interfaces.Repositories;
using analisadorDePagamento.Models;
using Microsoft.AspNetCore.Mvc;

namespace analisadorDePagamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessaDadosController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public ProcessaDadosController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            
        }

        [HttpGet]
        [Route("{pasta}")]
        public async Task<ActionResult<List<Departamento>>> GetDados(string pasta)
        {
            return Ok( await _dataRepository.GetDepartamentos(pasta));
        }
        
    }
}