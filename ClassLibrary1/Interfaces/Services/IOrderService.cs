using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.Order;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<Result> CreateAsync(OrderCreateVO orderCreateVO);
        public Task<Result> ApproveAsync(string orderId);
        public Task<Result> CancellAsync(string orderId);
        public Task<Order> GetById(string id);
        public Task<List<Order>> GetAll();
    }
}
