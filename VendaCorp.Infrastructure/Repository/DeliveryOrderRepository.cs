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
    public class DeliveryOrderRepository : IDeliveryOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public DeliveryOrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result> OnTheWayAsync(DeliveryOrder salesOrder)
        {
            DeliveryOrder delivery = await _db.DeliveryOrder.Where(x => x.Id == salesOrder.Id).FirstOrDefaultAsync();

            delivery.Status = ContantsDeliveryOrder.OnTheWay;

            await _db.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> DeliveredAsync(DeliveryOrder salesOrder)
        {
            DeliveryOrder delivery = await _db.DeliveryOrder.Where(x => x.Id == salesOrder.Id).FirstOrDefaultAsync();

            delivery.Status = ContantsDeliveryOrder.Delivered;

            await _db.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> CreateAsync(DeliveryOrder salesOrder)
        {
            salesOrder.Status = ContantsDeliveryOrder.Peding;
            _db.DeliveryOrder.Add(salesOrder);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<List<DeliveryOrder>> GetAllAsync()
        {
            List<DeliveryOrder> deliveryOrders = await _db.DeliveryOrder.ToListAsync();

            return deliveryOrders;
        }
    }
}
