using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Esterdigi.Api.Core.Controller;
using Esterdigi.Api.Core.Response;
using Pix.Gateway.Api.Service;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Esterdigi.Api.Core.Database.Domain.Model;

namespace Pix.Gateway.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("bank-account")]
    public class BankAccountController: BaseController
    {
        private readonly BankAccountService _service;

        public BankAccountController(BankAccountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista dos registros da tabela de bancos
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpGet, AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<BankAccountResponse, PagedResult>), StatusCodes.Status200OK)]
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
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpGet("{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return await Response(await _service.Get(id, Request.Headers["Authorization"]), _service.Notifications);
        }

        /// <summary>
        /// Insere um registro na tabela bancos
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] BankAccountRegisterRequest request)
        {
            return await Response(await _service.Add(request, Request.Headers["Authorization"]), _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela banco
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpPut, AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] BankAccountUpdateRequest request)
        {
            return await Response(await _service.Update(request, Request.Headers["Authorization"]), _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela banco
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpDelete("{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return await Response(await _service.Delete(id, Request.Headers["Authorization"]), _service.Notifications);
        }

    }
}
