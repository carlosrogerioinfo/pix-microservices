using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class UserRegisterRequest :  ICommand
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public bool Active { get; set; }
        public string CpfCnpj { get; set; }

    }

    public class UserUpdateRequest : UserRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }
}