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
    public class BankTransactionService : Notifiable,
                ICommandHandler<BankTransactionRegisterRequest>,
                ICommandHandler<BankTransactionUpdateRequest>
    {
        private readonly IBankTransactionRepository _repository;
        private readonly IBankRepository _repositoryBank;
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IUserRepository _repositoryUser;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public BankTransactionService(IBankTransactionRepository repository,
            IBankRepository repositoryBank,
            ICompanyRepository repositoryCompany,
            IUserRepository repositoryUser,
            IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            _repositoryBank = repositoryBank;
            _repositoryCompany = repositoryCompany;
            _repositoryUser = repositoryUser;
        }

        public async Task<PagedResponse<BankTransactionResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, BankTransactionFilter dynamicFilter)
        {
            var entity = await _repository.SearchPaged(dynamicFilter, paginationFilter,
                                                        include => include.Bank,
                                                        include => include.BankAccount,
                                                        include => include.Company,
                                                        include => include.User,
                                                        include => include.Device);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<BankTransactionResponse, PagedResult>(_mapper.Map<List<BankTransactionResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id,
                                                        include => include.Bank,
                                                        include => include.BankAccount,
                                                        include => include.Company,
                                                        include => include.User,
                                                        include => include.Device);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<BankTransactionResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankTransactionRegisterRequest request)
        {
            //os campos foram comentados no request, para primeira parte

            await ValidateInsert(request);

            var entity = new BankTransaction(default, request.Amount, request.TransactionId, null, request.Description,
                Guid.NewGuid().ToString(), request.BankId, request.CompanyId, request.UserId, request.StatusCodeType, 
                request.QrCode, request.BankAccountId, request.DeviceId, 
                (request.IdempotentId.HasValue ? request.IdempotentId.Value : null), 
                (request.PayerBankId.HasValue ? request.PayerBankId.Value : null), 
                request.PayerName, request.PayerDescription);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankTransactionResponse>(entity);
        }

        public async Task<ICommandResult> Handle(BankTransactionUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new BankTransaction(request.Id, request.Amount, request.TransactionId, null, request.Description, 
                Guid.NewGuid().ToString(), request.BankId, request.CompanyId, request.UserId, request.StatusCodeType, request.QrCode,
                request.BankAccountId, request.DeviceId,
                (request.IdempotentId.HasValue ? request.IdempotentId.Value : null),
                (request.PayerBankId.HasValue ? request.PayerBankId.Value : null),
                request.PayerName, request.PayerDescription,
                (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankTransactionResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<BankTransactionResponse>(entity);
        }


        private async Task<BankTransaction> ValidateInsert(BankTransactionRegisterRequest request)
        {
            var bank = await _repositoryBank.GetDataAsync(x => x.Id == request.BankId && x.Active == true);
            var company = await _repositoryCompany.GetDataAsync(x => x.Id == request.CompanyId && x.Active == true);
            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);

            if (bank is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");
            if (company is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");
            if (user is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");

            return default;
        }

        private async Task<BankTransaction> ValidateUpdate(BankTransactionUpdateRequest request)
        {
            var bank = await _repositoryBank.GetDataAsync(x => x.Id == request.BankId && x.Active == true);
            var company = await _repositoryCompany.GetDataAsync(x => x.Id == request.CompanyId && x.Active == true);
            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);
            var entity = await _repository.GetDataAsync(x => x.Id == request.Id);

            if (bank is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");
            if (company is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");
            if (user is null) AddNotification("Warning", "Banco não cadastrado ou o banco não está ativo");
            if (entity is null) AddNotification("Warning", "Conta bancária não encontrada");

            return entity;
        }

    }
}
