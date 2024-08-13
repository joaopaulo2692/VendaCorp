using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface IOrderItemRepository
    {
        public Task<Result> CreateAsync(List<OrderItem> items);
    }
}
