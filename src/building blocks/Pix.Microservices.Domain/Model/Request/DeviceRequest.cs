using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions;
using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class DeviceRegisterRequest :  ICommand
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
        public string Model { get; set; }
        public string PhoneNumber { get; set; }

        public Guid CompanyId { get; set; }
    }

    public class DeviceUpdateRequest : DeviceRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

    public class DeviceFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals)]
        public Guid? Id { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Name { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Platform { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string PlatformVersion { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Model { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string PhoneNumber { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public Guid? CompanyId { get; set; }
    }
}
