using AutoMapper;
using FluentValidator;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Repository.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Core.Lib.Commands;
using Pix.Core.Lib.Extensions;

namespace Pix.Companies.Api.Service
{
    public class CompanyService: Notifiable,
                ICommandHandler<CompanyRegisterRequest>,
                ICommandHandler<CompanyUpdateRequest>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public CompanyService(ICompanyRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;

        }

        public async Task<PagedResponse<CompanyResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, CompanyFilter dynamicFilter)
        {
            var entity = await _repository.SearchPaged(dynamicFilter, paginationFilter, dynamicFilter.SortBy);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<CompanyResponse, PagedResult>(_mapper.Map<List<CompanyResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<CompanyResponse>(entity);
        }

        public async Task<ICommandResult> Handle(CompanyRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new Company(default, request.CompanyName, request.TradingName, request.Cnpj, request.Email, request.PhoneNumber, request.Contact, request.Active);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CompanyResponse>(entity);
        }

        public async Task<ICommandResult> Handle(CompanyUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new Company(request.Id, request.CompanyName, request.TradingName, request.Cnpj, request.Email, request.PhoneNumber, request.Contact, request.Active, (validation is null ? default : validation.CreatedAt));
            
            AddNotifications(entity.Notifications);
            
            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CompanyResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CompanyResponse>(entity);
        }

        private async Task<Company> ValidateInsert(CompanyRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.CompanyName == request.CompanyName || x.TradingName.ToLower() == request.TradingName.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe uma empresa com esse mesmo nome ou razão social");

            if (!StringExtensionTools.IsValidCnpj(request.Cnpj)) AddNotification("Warning", "CNPJ da empresa inválido");

            return entity;
        }

        private async Task<Company> ValidateUpdate(CompanyUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && (x.CompanyName == request.CompanyName || x.TradingName.ToLower() == request.TradingName.ToLower()));
            if (entity is not null) AddNotification("Warning", "Já existe uma empresa com esse mesmo nome ou razão social");

            entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Empresa não encontrada");

            if (!StringExtensionTools.IsValidCnpj(request.Cnpj)) AddNotification("Warning", "CNPJ da empresa inválido");

            return entity;
        }

    }
}
