using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Integration.Domain.Model;
using Pix.Core.Lib.Controller;
using Pix.Core.Lib.Response;
using Pix.Devices.Api.Service;
using System.ComponentModel.DataAnnotations;

namespace Pix.Devices.Api.Controllers
{
    [Route("device")]
    public class DeviceController : BaseController
    {
        private readonly DeviceService _service;

        public DeviceController(DeviceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista dos registros da tabela device pelos filtros dinamicos
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<DeviceResponse, PagedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAllByFilter([FromQuery] PaginationFilter paginationFilter, [FromQuery] DeviceFilter filter)
        {
            var data = await _service.GetAllByFilter(paginationFilter, filter);

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Retorna o registro da tabela device filtrado pelo id
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<DeviceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get([Required] Guid id)
        {
            var data = await _service.Handle(id);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Insere um registro na tabela device
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("add"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<DeviceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] DeviceRegisterRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela device
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPut, Route("update"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<DeviceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] DeviceUpdateRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela device
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpDelete, Route("delete"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<DeviceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete([FromQuery, Required] Guid id)
        {
            var data = await _service.Delete(id);
            return await Response(data, _service.Notifications);
        }

    }
}