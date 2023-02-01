using Itau.TestePratico.Dominio.IRepositorio;
using Itau.TestePratico.Dominio.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Itau.TestePratico.Api.Controllers
{
    [Route("Feriados")]
    public class FeriadosController : ControllerBase
    {
        private readonly IRepositorioFeriado _repositorioFeriado;
        public FeriadosController(IRepositorioFeriado repositorioFeriado)
        {
            _repositorioFeriado = repositorioFeriado;
        }
        [HttpGet("Obter")]
        public async Task<JsonResult> Obter() => new JsonResult(await _repositorioFeriado.Obter());
        

        [HttpGet("ObterPor")]
        public async Task<JsonResult> ObterPor(string Mes, string Ano) => new JsonResult(await _repositorioFeriado.ObterPorMesAno(Mes, Ano));
        

        [HttpPost("Criar")]
        public async Task<IActionResult> Criar(Feriado feriado)
        {
            try
            {
                await _repositorioFeriado.Criar(feriado);
                return Ok("Feriado inserido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Atualizar")]
        public async Task<IActionResult> Atualizar([BindRequired, FromQuery] Guid EntityId, Feriado feriado)
        {
            try
            {
                await _repositorioFeriado.Atualizar(EntityId, feriado);
                return Ok("Feriado atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Remover")]
        public async Task<IActionResult> Remover([BindRequired, FromQuery] Guid EntityId)
        {
            try
            {
                await _repositorioFeriado.Remover(EntityId);
                return Ok("Feriado removido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
