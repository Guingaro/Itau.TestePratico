using Itau.TestePratico.Api.Dto;
using Itau.TestePratico.Dominio.Enum;
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
        public async Task<IActionResult> Criar(FeriadoDto feriado)
        {
            try
            {
                await _repositorioFeriado.Criar(new Feriado { Nome = feriado.Nome,  Data = feriado.Data, Tipo = feriado.Tipo });
                return Created("Feriado inserido com sucesso.", feriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([BindRequired, FromQuery] Guid EntityId, FeriadoDto feriado)
        {
            try
            {
                await _repositorioFeriado.Atualizar(EntityId, new Feriado { Nome = feriado.Nome, Data = feriado.Data, Tipo = feriado.Tipo });
                return Ok("Feriado atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("AtualizarNome")]
        public async Task<IActionResult> AtualizarNome([BindRequired, FromQuery] Guid EntityId, [BindRequired, FromQuery] string nome)
        {
            try
            {
                await _repositorioFeriado.AtualizarNome(EntityId, nome);
                return Ok("Feriado atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("AtualizarData")]
        public async Task<IActionResult> AtualizarData([BindRequired, FromQuery] Guid EntityId, [BindRequired, FromQuery] string data)
        {
            try
            {
                await _repositorioFeriado.AtualizarData(EntityId, data);
                return Ok("Feriado atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("AtualizarTipo")]
        public async Task<IActionResult> Atualizar([BindRequired, FromQuery] Guid EntityId, [BindRequired, FromQuery] TipoFeriado tipoFeriado)
        {
            try
            {
                await _repositorioFeriado.AtualizarTipo(EntityId, tipoFeriado);
                return Ok("Feriado atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Remover")]
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
