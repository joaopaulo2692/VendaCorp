using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;

namespace VendaCorp.Infrastructure.Repository
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        public Task<Result> ApproveAsync(SalesOrder salesOrder)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CancelAsync(SalesOrder salesOrder)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreateAsync(SalesOrder salesOrder)
        {
            throw new NotImplementedException();
        }

        public Task<List<SalesOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
