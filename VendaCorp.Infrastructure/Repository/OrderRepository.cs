using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Infrastructure.Data;

namespace VendaCorp.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result> ApproveAsync(Order order)
        {
            Order orderDb = await _db.Orders.Where(x => x.Id == order.Id).FirstOrDefaultAsync();
            if (orderDb == null) return Result.Fail("Pedido não encontrado");

            orderDb.UpdatedAt = DateTime.Now;
            orderDb.Status = ContantsOrder.Approved;
            await _db.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> CancellAsync(Order order)
        {
            Order orderDb = await _db.Orders.Where(x => x.Id == order.Id).FirstOrDefaultAsync();
            if (orderDb == null) return Result.Fail("Pedido não encontrado");

            orderDb.UpdatedAt = DateTime.Now;
            orderDb.DisabledAt = DateTime.Now;
            orderDb.Status = ContantsOrder.Cancelled;
            await _db.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> CreateAsync(Order order)
        {
            try
            {
                order.CreatedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;
                order.Enterprise = order.Enterprise;
                order.Status = ContantsOrder.Created;

                _db.Orders.Add(order);
                await _db.SaveChangesAsync();

                return Result.Ok().WithSuccess(order.Id);
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<List<Order>> GetAll()
        {
            List<Order> orders = await _db.Orders.ToListAsync();

            return orders;
        }

        public async Task<Order> GetById(string id)
        {
            Order order = await _db.Orders.Where(x => x.Id == id)
                                .Include(x => x.OrderItems)
                                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<Result> UpdateAsync(Order order)
        {
            Order orderDb = await _db.Orders.Where(x => x.Id == order.Id).FirstOrDefaultAsync();
            if (orderDb == null) return Result.Fail("Pedido não encontrado");

            orderDb.UpdatedAt = DateTime.Now;
            orderDb.TotalAmount = order.TotalAmount;
            await _db.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
