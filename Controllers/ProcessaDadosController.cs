using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using analisadorDePagamento.Models;
using analisadorDePagamento.Repositories;
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
        public ActionResult<List<Departamento>> GetDados(string pasta)
        {
            return Ok(_dataRepository.GetDepartamentos(pasta));
        }
        
    }
}