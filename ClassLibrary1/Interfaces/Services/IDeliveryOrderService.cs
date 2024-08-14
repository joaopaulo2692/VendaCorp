using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.DeliveryOrder;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Services
{
    public interface IDeliveryOrderService
    {
        public Task<List<DeliveryOrder>> GetAllAsync();
        public Task<Result> CreateAsync(DeliveryOrderCreateVO delivery);
        public Task<Result> DeliveredAsync(int deliveryId);
        public Task<Result> OnTheWayAsync(int deliveryId);
    }
}
