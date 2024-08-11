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
        public Task<Result> ApproveAsync(DeliveryOrder salesOrder)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CancelAsync(DeliveryOrder salesOrder)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreateAsync(DeliveryOrder salesOrder)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeliveryOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
