using Pix.Core.Lib.Extensions;
using Pix.Core.Lib.Gateway;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Integration.Domain.Model;

namespace Pix.Gateway.Api.Service
{
    public class CompanyService: ServiceBase
    {
        public readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompanyResponse> Get(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"company/get?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<CompanyResponse>("data");
        }

        public async Task<PagedResponse<CompanyResponse, PagedResult>> GetAll(string token, PaginationFilter paginationFilter)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"company/get-all?page={paginationFilter.Page}&pagesize={paginationFilter.PageSize}");

            if (!ResponseErrorHandling(response)) return default;

            var data = await response.Content.ReadJsonAsync<IEnumerable<CompanyResponse>>("data");
            var paging = await response.Content.ReadJsonAsync<PagedResult>("paging");

            return new PagedResponse<CompanyResponse, PagedResult>(data, paging);
        }

        public async Task<CompanyResponse> Add(CompanyRegisterRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PostJsonAsync($"company/add", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<CompanyResponse>("data");
        }

        public async Task<CompanyResponse> Update(CompanyUpdateRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PutJsonAsync($"company/update", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<CompanyResponse>("data");
        }

        public async Task<CompanyResponse> Delete(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .DeleteJsonAsync($"company/delete?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<CompanyResponse>("data");
        }

    }
}
