﻿using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class BankTransactionHistoryRegisterRequest :  ICommand
    {
        public string Status { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public Guid BankTransactionId { get; set; }
    }

    public class BankTransactionHistoryUpdateRequest : BankTransactionHistoryRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

    public class BankTransactionHistoryFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? Id { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Status { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Request { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)]
        public string Response { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Equals)]
        public Guid? BankTransactionId { get; set; }

        public string SortBy { get; set; }
    }
}