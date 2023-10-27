using AutoMapper;
using FluentValidator;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Core.Integration.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Core.Lib.Commands;

namespace Pix.Banks.Api.Service
{
    public class BankTransactionHistoryService : Notifiable,
                ICommandHandler<BankTransactionHistoryRegisterRequest>,
                ICommandHandler<BankTransactionHistoryUpdateRequest>
    {
        private readonly IBankTransactionHistoryRepository _repository;
        private readonly IBankTransactionRepository _repositoryBankTransaction;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public BankTransactionHistoryService(IBankTransactionHistoryRepository repository, IBankTransactionRepository repositoryBankTransaction, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            _repositoryBankTransaction = repositoryBankTransaction;
        }

        public async Task<PagedResponse<BankTransactionHistoryResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, BankTransactionHistoryFilter dynamicFilter)
        {
            var entity = await _repository.SearchPaged(dynamicFilter, paginationFilter, dynamicFilter.SortBy,
                                                        include => include.BankTransaction);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<BankTransactionHistoryResponse, PagedResult>(_mapper.Map<List<BankTransactionHistoryResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id, include => include.BankTransaction);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<BankTransactionHistoryResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankTransactionHistoryRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new BankTransactionHistory(default, request.BankTransactionId, request.Status, request.Request, request.Response);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankTransactionHistoryResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankTransactionHistoryUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new BankTransactionHistory(request.Id, request.BankTransactionId, request.Status, request.Request, request.Response, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankTransactionHistoryResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankTransactionHistoryResponse>(entity);
        }

        private async Task<BankTransactionHistory> ValidateInsert(BankTransactionHistoryRegisterRequest request)
        {
            var bankTransaction = await _repositoryBankTransaction.GetDataAsync(x => x.Id == request.BankTransactionId);
            if (bankTransaction is null) AddNotification("Warning", "Transação bancária não identificada");

            return null;
        }

        private async Task<BankTransactionHistory> ValidateUpdate(BankTransactionHistoryUpdateRequest request)
        {
            var bankTransaction = await _repositoryBankTransaction.GetDataAsync(x => x.Id == request.BankTransactionId);
            if (bankTransaction is null) AddNotification("Warning", "Transação bancária não identificada");

            var entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Transação bancária não encontrada");

            return entity;
        }

    }
}
