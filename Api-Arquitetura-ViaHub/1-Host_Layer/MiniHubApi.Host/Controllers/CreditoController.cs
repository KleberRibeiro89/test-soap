using Elastic.CommonSchema;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniHubApi.Application.Dtos;
using MiniHubApi.Application.Interfaces;

namespace MiniHubApi.Host.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CreditoController : ControllerBase
    {
        private readonly ICreditoServices _creditoService;

        public CreditoController(ICreditoServices creditoService)
        {
            _creditoService = creditoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StatusCreditoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCreditoResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObterStatusCreditoAsync([FromBody] StatusCreditoResquestDto credito) 
        {
            try {
                Serilog.Log.Information($"PostClient method called: { credito.Acao }");
                var results = await _creditoService.ObterStatusCredito(credito);
                if (results.Count > 0)
                {
                    Serilog.Log.Information($"Retorno da mensagem: {results}");
                    return Ok(results);
                }
                else
                {
                    Serilog.Log.Information($"Dados nulos !!!");
                    return BadRequest(null);
                }
            }
            catch(Exception ex) { return Problem(ex.Message); }
        }

        [HttpGet("GetRetornandoUmStringXML")]
        public async Task<ActionResult> GetRetornandoUmStringXML()
        {
            return Ok(_creditoService.GetRetornandoUmStringXML());
        }

        [HttpGet("GetRetornandoUmObjeto")]
        public async Task<ActionResult> GetRetornandoUmObjeto()
        {
            return Ok(_creditoService.GetRetornandoUmObjeto());
        }
    }
}
