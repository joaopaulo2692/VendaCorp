using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Infrastructure.Data;

namespace VendaCorp.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result> CreateAsync(List<OrderItem> items)
        {
            //Order orderDB = await _db.Orders.Where(x => x.Id == order.Id).FirstOrDefaultAsync();
            //items.Order = orderDB;
            //foreach (OrderItem item in items)
            //{
            //    item.Order = orderDB;
            //}
            //_db.OrderItems.Add(items);
            _db.OrderItems.AddRange(items);
            await _db.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
