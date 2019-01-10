using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DbServer.LunchChoice.Core.application;
using System.Threading.Tasks;
using System;
using DbServer.LunchChoice.Core.application.Resources;

namespace DbServer.LunchChoice.api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VotacaoController : Controller
    {

        private VotoApplicationService _app = new VotoApplicationService();

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resumo = await _app.ContabilizarVotos();

                if (resumo == null)
                    return NotFound();

                return Ok(resumo);

            }
            catch (Exception exception)
            {
               return BadRequest($"Ocorreu um erro na encerramento dos votos. {exception.Message}");
                //Todo() Logar erro exception.
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VotoResource voto)
        {
            try
            {
                var profissionalId = await _app.Votar(voto);
                return Ok(new { profissionalId, mensagem = "Voto concluído com sucesso." });
            }
            catch (Exception exception)
            {
                return BadRequest($"Ocorreu um erro ao votar. {exception.Message}");
            }
        }
      
    }
}
