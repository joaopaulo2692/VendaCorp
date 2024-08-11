using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface ISalesOrderRepository
    {
        public Task<List<SalesOrder>> GetAllAsync();
        public Task<Result> CreateAsync(SalesOrder salesOrder);
        public Task<Result> CancelAsync(SalesOrder salesOrder);
        public Task<Result> ApproveAsync(SalesOrder salesOrder);
    }
}
