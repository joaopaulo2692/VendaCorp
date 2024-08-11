using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        public Task<Result> CreateAsync(Order order);
        public Task<Result> ApproveAsync(Order order);
        public Task<Result> CancellAsync(Order order);
    }
}
