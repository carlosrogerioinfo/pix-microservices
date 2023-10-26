using AutoMapper;
using FluentValidator;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Repository.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Core.Lib.Commands;

namespace Pix.Banks.Api.Service
{
    public class BankService: Notifiable,
                ICommandHandler<BankRegisterRequest>,
                ICommandHandler<BankUpdateRequest>
    {
        private readonly IBankRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public BankService(IBankRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;

        }

        public async Task<PagedResponse<BankResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, BankFilter dynamicFilter)
        {
            var entity = await _repository.SearchPaged(dynamicFilter, paginationFilter, dynamicFilter.SortBy);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<BankResponse, PagedResult>(_mapper.Map<List<BankResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<BankResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new Bank(default, request.Name, request.Number, request.Active);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new Bank(request.Id, request.Name, request.Number, request.Active, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankResponse>(entity);
        }

        private async Task<Bank> ValidateInsert(BankRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Number == request.Number || x.Name.ToLower() == request.Name.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe um banco cadastrado com esse nome ou número do banco");

            return entity;
        }

        private async Task<Bank> ValidateUpdate(BankUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && (x.Number == request.Number || x.Name.ToLower() == request.Name.ToLower()));
            if (entity is not null) AddNotification("Warning", "Já existe um banco cadastrado com esse nome ou número do banco");

            entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Banco não encontrado");

            return entity;
        }

    }
}
