using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class CompanyRegisterRequest :  ICommand
    {
        public string CompanyName { get; set; }
        public string TradingName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Contact { get; set; }
        public bool Active { get; set; }
    }

    public class CompanyUpdateRequest : CompanyRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

    public class CompanyFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? Id { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string CompanyName { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string TradingName { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string Cnpj { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string Email { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string PhoneNumber { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string Contact { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public bool? Active { get; set; }

    }
}
