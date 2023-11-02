using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Pix.Microservices.Domain.Enums;
using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class BankAccountRegisterRequest :  ICommand
    {
        public Guid BankId { get; set; }
        public Guid CompanyId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDigit { get; set; }
        public string Agency { get; set; }
        public AccountType AccountType { get; set; }
        public bool Active { get; set; }
    }

    public class BankAccountUpdateRequest : BankAccountRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class BankAccountFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? Id { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals)]
        public string AccountNumber { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Agency { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)] 
        public AccountType? AccountType { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)] 
        public bool? Active { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)]
        public Guid? BankId { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)]
        public Guid? CompanyId { get; set; }
        public string SortBy { get; set; }
    }
}