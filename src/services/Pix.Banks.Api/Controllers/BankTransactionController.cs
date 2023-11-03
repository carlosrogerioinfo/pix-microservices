using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Esterdigi.Api.Core.Database.Domain.Model;
using Pix.Banks.Api.Service;
using Esterdigi.Api.Core.Controller;
using Esterdigi.Api.Core.Response;

namespace Pix.Banks.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("bank-transactions")]
    public class BankTransactionController : BaseController
    {
        private readonly BankTransactionService _service;

        public BankTransactionController(BankTransactionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista dos registros da tabela transacoes pelos filtros dinamicos
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpGet("{companyid}"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAllByFilter([FromQuery] PaginationFilter paginationFilter, [FromQuery] BankTransactionFilterRoute filter, [FromRoute] Guid companyid)
        {
            var data = await _service.GetAllByFilter(paginationFilter, new BankTransactionFilter
            {
                CompanyId = companyid,
                DeviceId = filter.DeviceId,
                UserId = filter.UserId,
                SortBy = filter.SortBy
            });

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Retorna o registro da tabela transacao filtrado pelo id
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpGet("{id}/detail"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var data = await _service.Handle(id);
            return await Response(data, _service.Notifications);
        }

        /*
        /// <summary>
        /// Insere um registro na tabela  transacoes pix (essas informacoes virao de outro servico, pus aqui para podermos fazer a simulacao)
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] BankTransactionRegisterRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela conta bancaria
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpPut, AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] BankTransactionUpdateRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela conta bancaria
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condicao ou um algum erro interno.</response>
        [HttpDelete("{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<BankTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var data = await _service.Delete(id);
            return await Response(data, _service.Notifications);
        }
        */


    }
}
