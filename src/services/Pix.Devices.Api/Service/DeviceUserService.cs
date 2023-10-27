using AutoMapper;
using FluentValidator;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Integration.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Core.Lib.Commands;

namespace Pix.Devices.Api.Service
{
    public class DeviceUserService: Notifiable,
                ICommandHandler<DeviceUserRegisterRequest>,
                ICommandHandler<DeviceUserUpdateRequest>
    {
        private readonly IDeviceUserRepository _repository;
        private readonly IDeviceRepository _repositoryDevice;
        private readonly IUserRepository _repositoryUser;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public DeviceUserService(IDeviceUserRepository repository, IMapper mapper, IUow uow, IDeviceRepository repositoryDevice, IUserRepository repositoryUser)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            _repositoryDevice = repositoryDevice;
            _repositoryUser = repositoryUser;
        }

        public async Task<PagedResponse<DeviceUserResponse, PagedResult>> Listar(PaginationFilter paginationFilter)
        {
            var entity = await _repository.GetAllAsync(paginationFilter);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<DeviceUserResponse, PagedResult>(_mapper.Map<List<DeviceUserResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetAsync(id);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<DeviceUserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(DeviceUserRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new DeviceUser(default, request.Active, request.DeviceId, request.UserId);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<DeviceUserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(DeviceUserUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new DeviceUser(request.Id, request.Active, request.DeviceId, request.UserId, (validation is null ? default : validation.CreatedAt));
            
            AddNotifications(entity.Notifications);
            
            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<DeviceUserResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<DeviceUserResponse>(entity);
        }

        private async Task<DeviceUser> ValidateInsert(DeviceUserRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.UserId == request.UserId && x.DeviceId == request.DeviceId);
            if (entity is not null) AddNotification("Warning", "Já existe um dispositivo associado a este usuário");

            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);
            if (user is null) AddNotification("Warning", "Usuário não encontrado");

            var device = await _repositoryDevice.GetDataAsync(x => x.Id == request.DeviceId);
            if (device is null) AddNotification("Warning", "Dispositivo não encontrado");

            return entity;
        }

        private async Task<DeviceUser> ValidateUpdate(DeviceUserUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && (x.UserId == request.UserId && x.DeviceId == request.DeviceId));
            if (entity is not null) AddNotification("Warning", "Já existe um dispositivo associado a este usuário");

            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);
            if (user is null) AddNotification("Warning", "Usuário não encontrado");

            var device = await _repositoryDevice.GetDataAsync(x => x.Id == request.DeviceId);
            if (device is null) AddNotification("Warning", "Dispositivo não encontrado");

            return entity;
        }

    }
}
