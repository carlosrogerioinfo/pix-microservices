using AutoMapper;
using FluentValidator;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Esterdigi.Api.Core.Database.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Esterdigi.Api.Core.Commands;
using Esterdigi.Api.Core.Helpers.Encrypt;

namespace Pix.Users.Api.Service
{
    public class UserService: Notifiable,
                ICommandHandler<UserRegisterRequest>,
                ICommandHandler<UserUpdateRequest>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public UserService(IUserRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;

        }

        public async Task<UserResponse> Handle(AuthenticationRequest request)
        {
            var entity = await _repository.LoginAsync(request.Email, Cryptography.EncryptPassword(request.Password));

            if (entity is null)
            {
                AddNotification("Warning", "Usuário ou senha inválidos");
                return default;
            }

            if (!IsValid()) return default;

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<PagedResponse<UserResponse, PagedResult>> Listar(PaginationFilter paginationFilter)
        {
            var entity = await _repository.GetAllAsync(paginationFilter);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<UserResponse, PagedResult>(_mapper.Map<List<UserResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(UserRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new User(default, request.Name, request.Email, request.UserType);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(UserUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new User(request.Id, request.Name, request.Email, request.UserType, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }


        private async Task<User> ValidateInsert(UserRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Email.ToLower() == request.Email.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe um registro com esse e-mail cadastrado");

            return entity;
        }

        private async Task<User> ValidateUpdate(UserUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && x.Email.ToLower() == request.Email.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe um registro com esse e-mail cadastrado");

            entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Usuário não encontrado");

            return entity;
        }

    }
}
