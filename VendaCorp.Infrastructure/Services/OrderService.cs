using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.Order;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        public Task<Result> ApproveAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CancellAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreateAsync(OrderCreateVO orderCreateVO)
        {
            throw new NotImplementedException();
        }
    }
}
