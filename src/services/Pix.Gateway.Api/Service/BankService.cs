using Pix.Core.Lib.Extensions;
using Pix.Core.Lib.Gateway;
using Pix.Core.Lib.Helpers;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Integration.Domain.Model;

namespace Pix.Gateway.Api.Service
{
    public class BankService: ServiceBase
    {
        public readonly HttpClient _httpClient;

        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BankResponse> Get(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"bank/get?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankResponse>("data");
        }

        public async Task<PagedResponse<BankResponse, PagedResult>> GetAll(string token, PaginationFilter paginationFilter, BankFilter filter)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"bank/get-all?page={paginationFilter.Page}&pagesize={paginationFilter.PageSize}{Helper.ConvertToQueryString(filter, true)}");

            if (!ResponseErrorHandling(response)) return default;

            var data = await response.Content.ReadJsonAsync<IEnumerable<BankResponse>>("data");
            var paging = await response.Content.ReadJsonAsync<PagedResult>("paging");

            return new PagedResponse<BankResponse, PagedResult>(data, paging);
        }

        public async Task<BankResponse> Add(BankRegisterRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PostJsonAsync($"bank/add", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankResponse>("data");
        }

        public async Task<BankResponse> Update(BankUpdateRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PutJsonAsync($"bank/update", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankResponse>("data");
        }

        public async Task<BankResponse> Delete(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .DeleteJsonAsync($"bank/delete?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<BankResponse>("data");
        }

    }
}
