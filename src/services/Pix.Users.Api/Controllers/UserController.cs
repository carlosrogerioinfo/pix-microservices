using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Esterdigi.Api.Core.Database.Domain.Model;
using Esterdigi.Api.Core.Controller;
using Esterdigi.Api.Core.Response;
using Pix.Users.Api.Service;

namespace Pix.Users.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista dos registros da tabela usuario
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpGet, AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<UserResponse, PagedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var data = await _service.Listar(paginationFilter);

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Retorna o registro da tabela usuario filtrado pelo id
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpGet("{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var data = await _service.Handle(id);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Insere um registro na tabela usuario
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] UserRegisterRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela usuario
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpPut, AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] UserUpdateRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela usuario
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpDelete("{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var data = await _service.Delete(id);
            return await Response(data, _service.Notifications);
        }

    }
}
