using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pix.Core.Lib.Controller;
using Pix.Core.Lib.Response;
using Pix.Gateway.Api.Service;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Repository.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Pix.Gateway.Api.Controllers
{
    [Route("user")]
    public class UserController: BaseController
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista dos registros da tabela de bancos
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<UserResponse, PagedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var data = await _service.GetAll(Request.Headers["Authorization"], paginationFilter);

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Retorna o registro da tabela banco filtrado pelo id
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get([Required] Guid id)
        {
            return await Response(await _service.Get(id, Request.Headers["Authorization"]), _service.Notifications);
        }

        /// <summary>
        /// Insere um registro na tabela bancos
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("add"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] UserRegisterRequest request)
        {
            return await Response(await _service.Add(request, Request.Headers["Authorization"]), _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela banco
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPut, Route("update"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] UserUpdateRequest request)
        {
            return await Response(await _service.Update(request, Request.Headers["Authorization"]), _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela banco
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpDelete, Route("delete"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete([FromQuery, Required] Guid id)
        {
            return await Response(await _service.Delete(id, Request.Headers["Authorization"]), _service.Notifications);
        }

    }
}