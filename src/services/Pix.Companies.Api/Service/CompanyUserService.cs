using AutoMapper;
using FluentValidator;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Request;
using Pix.Microservices.Domain.Http.Response;
using Esterdigi.Api.Core.Database.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Esterdigi.Api.Core.Commands;

namespace Pix.Companies.Api.Service
{
    public class CompanyUserService: Notifiable,
                ICommandHandler<CompanyUserRegisterRequest>,
                ICommandHandler<CompanyUserUpdateRequest>
    {
        private readonly ICompanyUserRepository _repository;
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IUserRepository _repositoryUser;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public CompanyUserService(ICompanyUserRepository repository, IMapper mapper, IUow uow, ICompanyRepository repositoryCompany, IUserRepository repositoryUser)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
            _repositoryCompany = repositoryCompany;
            _repositoryUser = repositoryUser;
        }

        public async Task<PagedResponse<CompanyUserResponse, PagedResult>> Listar(PaginationFilter paginationFilter)
        {
            var entity = await _repository.GetAllAsync(paginationFilter);

            if (entity.Data.Count() <= 0) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return new PagedResponse<CompanyUserResponse, PagedResult>(_mapper.Map<List<CompanyUserResponse>>(entity.Data), entity.Paging);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetAsync(id);

            if (entity is null) AddNotification("Alert", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<CompanyUserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(CompanyUserRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new CompanyUser(default, request.Active, request.CompanyId, request.UserId);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CompanyUserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(CompanyUserUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new CompanyUser(request.Id, request.Active, request.CompanyId, request.UserId, (validation is null ? default : validation.CreatedAt));
            
            AddNotifications(entity.Notifications);
            
            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CompanyUserResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CompanyUserResponse>(entity);
        }

        private async Task<CompanyUser> ValidateInsert(CompanyUserRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.UserId == request.UserId && x.CompanyId == request.CompanyId);
            if (entity is not null) AddNotification("Warning", "Já existe uma empresa associada a este usuário");

            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);
            if (user is null) AddNotification("Warning", "Usuário não encontrado");

            var company = await _repositoryCompany.GetDataAsync(x => x.Id == request.CompanyId);
            if (company is null) AddNotification("Warning", "Empresa não encontrada");

            return entity;
        }

        private async Task<CompanyUser> ValidateUpdate(CompanyUserUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && (x.UserId == request.UserId && x.CompanyId == request.CompanyId));
            if (entity is not null) AddNotification("Warning", "Já existe uma empresa associada a este usuário");

            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);
            if (user is null) AddNotification("Warning", "Usuário não encontrado");

            var company = await _repositoryCompany.GetDataAsync(x => x.Id == request.CompanyId);
            if (company is null) AddNotification("Warning", "Empresa não encontrada");

            return entity;
        }

    }
}
