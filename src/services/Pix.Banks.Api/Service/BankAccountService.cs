using AutoMapper;
using FluentValidator;
using Pix.Core.Lib.Commands;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Integration.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;

namespace Pix.Banks.Api.Service
{
    public class BankAccountService : Notifiable,
                ICommandHandler<BankAccountRegisterRequest>,
                ICommandHandler<BankAccountUpdateRequest>
    {
        private readonly IBankAccountRepository _repository;
        private readonly IBankRepository _repositoryBank;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public BankAccountService(IBankAccountRepository repository, IBankRepository repositoryBank, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            _repositoryBank = repositoryBank;
        }

        public async Task<PagedResponse<BankAccountResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, BankAccountFilter dynamicFilter)
        {
            var entity = await _repository.SearchPaged(dynamicFilter, paginationFilter, dynamicFilter.SortBy,
                                                        include => include.Bank,
                                                        include => include.Company);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<BankAccountResponse, PagedResult>(_mapper.Map<List<BankAccountResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id,
                                                        include => include.Bank,
                                                        include => include.Company);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<BankAccountResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankAccountRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new BankAccount(default, request.AccountNumber, request.AccountDigit, request.AccountType, request.BankId, 
                request.CompanyId, request.Agency, request.Active);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankAccountResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankAccountUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new BankAccount(request.Id, request.AccountNumber, request.AccountDigit, request.AccountType, 
                request.BankId, request.CompanyId, request.Agency, request.Active, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankAccountResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);
            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankAccountResponse>(entity);
        }


        private async Task<BankAccount> ValidateInsert(BankAccountRegisterRequest request)
        {
            var bank = await _repositoryBank.GetDataAsync(x => x.Id == request.BankId && x.Active == true);
            if (bank is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");

            var entity = await _repository.GetDataAsync(x => x.Bank.Id == request.BankId && x.AccountNumber == request.AccountNumber);
            if (entity is not null) AddNotification("Warning", "Já existe uma conta cadastrada para esse banco");

            return entity;
        }

        private async Task<BankAccount> ValidateUpdate(BankAccountUpdateRequest request)
        {
            var bank = await _repositoryBank.GetDataAsync(x => x.Id == request.BankId);
            if (bank is null) AddNotification("Warning", "Banco não cadastrado");

            var entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Conta bancária não encontrada");

            return entity;
        }

    }
}
