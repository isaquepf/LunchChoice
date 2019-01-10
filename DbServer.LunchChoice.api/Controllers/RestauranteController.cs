using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DbServer.LunchChoice.Core.infra;

namespace DbServer.LunchChoice.api.Controllers
{
    [Produces("application/json")]
    [Route("api/restaurante")]
    public class RestauranteController : Controller
    {           
        public async Task<IActionResult> Get()
        {
            try
            {
                var restaurantes = await RestauranteRepository.ObterRestaurantes();

                if (restaurantes == null)
                    return NotFound();

                if (restaurantes.Count == 0)
                    return NoContent();

                return Ok(restaurantes);
            }
            catch (Exception exception)
            {
                //Todo() logar erro Logger.Error(exception);
                return BadRequest($"Ocorreu um erro ao listar os restaurantes.\n {exception.Message}");
            }
        }
    }
}