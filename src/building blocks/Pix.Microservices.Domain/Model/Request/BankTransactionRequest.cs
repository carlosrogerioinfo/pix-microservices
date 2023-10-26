using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Pix.Core.Lib.Commands;
using Pix.Microservices.Domain.Enums;

namespace Pix.Microservices.Domain.Http.Request
{
    public class BankTransactionRegisterRequest :  ICommand
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public string QrCode { get; set; }
        public string TransactionId { get; set; }
        public Guid? IdempotentId { get; set; }
        public string PayerName { get; set; }
        public Guid? PayerBankId { get; set; }
        public string PayerDescription { get; set; }
        public StatusCodeType StatusCodeType { get; set; }
        public Guid BankId { get; set; }
        public Guid BankAccountId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
    }

    public class BankTransactionUpdateRequest : BankTransactionRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class BankTransactionFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? Id { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? UserId { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? DeviceId { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? CompanyId { get; set; }
    }

    public class BankTransactionFilterRoute
    {
        public Guid? UserId { get; set; }
        public Guid? DeviceId { get; set; }
    }

}