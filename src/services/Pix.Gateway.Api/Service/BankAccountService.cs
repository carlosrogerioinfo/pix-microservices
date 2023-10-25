using Pix.Core.Lib.Extensions;
using Pix.Core.Lib.Gateway;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Repository.Domain.Model;

namespace Pix.Gateway.Api.Service
{
    public class BankAccountService: ServiceBase
    {
        public readonly HttpClient _httpClient;

        public BankAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BankAccountResponse> Get(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"bank-account/get?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankAccountResponse>("data");
        }

        public async Task<PagedResponse<BankAccountResponse, PagedResult>> GetAll(string token, PaginationFilter paginationFilter)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"bank-account/get-all?page={paginationFilter.Page}&pagesize={paginationFilter.PageSize}");

            if (!ResponseErrorHandling(response)) return default;

            var data = await response.Content.ReadJsonAsync<IEnumerable<BankAccountResponse>>("data");
            var paging = await response.Content.ReadJsonAsync<PagedResult>("paging");

            return new PagedResponse<BankAccountResponse, PagedResult>(data, paging);
        }

        public async Task<BankAccountResponse> Add(BankAccountRegisterRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PostJsonAsync($"bank-account/add", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankAccountResponse>("data");
        }

        public async Task<BankAccountResponse> Update(BankAccountUpdateRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PutJsonAsync($"bank-account/update", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankAccountResponse>("data");
        }

        public async Task<BankAccountResponse> Delete(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .DeleteJsonAsync($"bank-account/delete?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankAccountResponse>("data");
        }

    }
}
