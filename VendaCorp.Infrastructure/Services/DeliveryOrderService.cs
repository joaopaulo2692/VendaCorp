using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO.DeliveryOrder;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.Infrastructure.Services
{
    public class DeliveryOrderService : IDeliveryOrderService
    {
        private readonly IDeliveryOrderRepository _deliveryRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IShippingCompanyRepository _shippingRepo;

        public DeliveryOrderService(IDeliveryOrderRepository deliveryRepo, IOrderRepository orderRepo, IShippingCompanyRepository shippingRepo)
        {
            _deliveryRepo = deliveryRepo;
            _orderRepo = orderRepo;
            _shippingRepo = shippingRepo;
        }

        public async Task<Result> CreateAsync(DeliveryOrderCreateVO delivery)
        {
            Order order = await _orderRepo.GetById(delivery.OrderId);
            if (order == null) return Result.Fail("Pedido não encontrado");

            if(order.OrderItems == null || order.OrderItems.Count == 0) return Result.Fail("Pedido sem itens");

            if (order.Status == ContantsOrder.Cancelled || order.Status == ContantsOrder.Created) return Result.Fail("Pedido ainda não foi aprovado ou está cancelado");

            ShippingCompany shippingCompany = await _shippingRepo.GetByName(delivery.ShippingCompanyName);
            if (shippingCompany == null) return Result.Fail("Transportadora informada não cadastrada no sistema");

            DeliveryOrder deliveryOrder = new DeliveryOrder()
            {
                CustomerAddress = delivery.CustomerAddress,
                DeliveryDate = delivery.DeliveryDate,
                Order = order,
                ShippingCompany = shippingCompany,
                Status = ContantsDeliveryOrder.Peding,
                ShippingCompanyName = delivery.ShippingCompanyName,
            };

            Result response = await _deliveryRepo.CreateAsync(deliveryOrder);

            return response;

        }

        public async Task<Result> DeliveredAsync(int deliveryId)
        {
            DeliveryOrder deliveryOrder = await _deliveryRepo.GetByIdAsync(deliveryId);
            if (deliveryOrder == null) return Result.Fail("Pedido de entrega não localizado");

            if (deliveryOrder.Status != ContantsDeliveryOrder.OnTheWay) return Result.Fail("Pedido cancelado ou ainda não foi passado pela 'etapa a caminho'");

            Result delivered = await _deliveryRepo.DeliveredAsync(deliveryOrder);

            return delivered;
        }

        public async Task<List<DeliveryOrder>> GetAllAsync()
        {
            List<DeliveryOrder> deliveryOrders = await _deliveryRepo.GetAllAsync();

            return deliveryOrders;
        }

        public async Task<Result> OnTheWayAsync(int deliveryId)
        {
            DeliveryOrder deliveryOrder = await _deliveryRepo.GetByIdAsync(deliveryId);
            if (deliveryOrder == null) return Result.Fail("Pedido de entrega não localizado");

            if (deliveryOrder.Status != ContantsDeliveryOrder.Peding) return Result.Fail("Pedido cancelado ou já está a caminho");

            Result delivered = await _deliveryRepo.OnTheWayAsync(deliveryOrder);

            return delivered;
        }
    }
}
