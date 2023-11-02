using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class CompanyUserRegisterRequest :  ICommand
    {
        public bool Active { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class CompanyUserUpdateRequest : CompanyUserRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }
}
