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
    public class DeviceService: Notifiable,
                ICommandHandler<DeviceRegisterRequest>,
                ICommandHandler<DeviceUpdateRequest>
    {
        private readonly IDeviceRepository _repository;
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public DeviceService(IDeviceRepository repository, IMapper mapper, IUow uow, ICompanyRepository repositoryCompany)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            _repositoryCompany = repositoryCompany;
        }

        public async Task<PagedResponse<DeviceResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, DeviceFilter dynamicFilter)
        {
            var entity = await _repository.SearchPaged(dynamicFilter, paginationFilter, dynamicFilter.SortBy, include => include.Company);
            
            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<DeviceResponse, PagedResult>(_mapper.Map<List<DeviceResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id, include => include.Company);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<DeviceResponse>(entity);
        }

        public async Task<ICommandResult> Handle(DeviceRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new Device(default, request.Name, request.Platform, request.PlatformVersion, request.Model, request.PhoneNumber, request.CompanyId);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<DeviceResponse>(entity);
        }

        public async Task<ICommandResult> Handle(DeviceUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new Device(request.Id, request.Name, request.Platform, request.PlatformVersion, request.Model, request.PhoneNumber, request.CompanyId, (validation is null ? default : validation.CreatedAt));
            
            AddNotifications(entity.Notifications);
            
            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<DeviceResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<DeviceResponse>(entity);
        }

        private async Task<Device> ValidateInsert(DeviceRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Name.ToLower() == request.Name.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe um dispositivo cadastrado com esse nome");

            var company = await _repositoryCompany.GetDataAsync(x => x.Id == request.CompanyId && x.Active == true);
            if (company is null) AddNotification("Warning", "A empresa informada não foi encontrada ou não está ativa");

            return entity;
        }

        private async Task<Device> ValidateUpdate(DeviceUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Name.ToLower() == request.Name.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe um dispositivo cadastrado com esse nome");

            entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Dispositivo não encontrado");

            var company = await _repositoryCompany.GetDataAsync(x => x.Id == request.CompanyId && x.Active == true);
            if (company is null) AddNotification("Warning", "A empresa informada não foi encontrada ou não está ativa");

            return entity;
        }

    }
}
