using Pix.Core.Lib.Extensions;
using Pix.Core.Lib.Gateway;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Repository.Domain.Model;

namespace Pix.Gateway.Api.Service
{
    public class UserService: ServiceBase
    {
        public readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponse> Get(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"user/get?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<UserResponse>("data");
        }

        public async Task<PagedResponse<UserResponse, PagedResult>> GetAll(string token, PaginationFilter paginationFilter)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .GetJsonAsync($"user/get-all?page={paginationFilter.Page}&pagesize={paginationFilter.PageSize}");

            if (!ResponseErrorHandling(response)) return default;

            var data = await response.Content.ReadJsonAsync<IEnumerable<UserResponse>>("data");
            var paging = await response.Content.ReadJsonAsync<PagedResult>("paging");

            return new PagedResponse<UserResponse, PagedResult>(data, paging);
        }

        public async Task<UserResponse> Add(UserRegisterRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PostJsonAsync($"user/add", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<UserResponse>("data");
        }

        public async Task<UserResponse> Update(UserUpdateRequest request, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .PutJsonAsync($"user/update", request);

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<UserResponse>("data");
        }

        public async Task<UserResponse> Delete(Guid id, string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.AddTokenAuthorization(token);

            var response = await _httpClient
                .DeleteJsonAsync($"user/delete?id={id}");

            if (!ResponseErrorHandling(response)) return default;

            return await response.Content.ReadJsonAsync<UserResponse>("data");
        }

    }
}
