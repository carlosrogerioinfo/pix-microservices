﻿using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class BankRegisterRequest :  ICommand
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public bool Active { get; set; }
    }

    public class BankUpdateRequest : BankRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

    public class BankFilter : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Guid? Id { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public string Name { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public int? Number { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public bool? Active { get; set; }

    }

}
