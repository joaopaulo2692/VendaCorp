﻿using FluentResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO;
using VendaCorp.Application.DTO.Order;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;
using static Azure.Core.HttpHeader;

namespace VendaCorp.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IProductExternApiService _productExtern;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IEnterpriseRepository enterpriseRepository, IProductExternApiService productExtern, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _enterpriseRepository = enterpriseRepository;
            _productExtern = productExtern;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Result> ApproveAsync(string orderId)
        {
            Order order = await _orderRepository.GetById(orderId);
            if (order == null) return Result.Fail("Erro ao buscar pedido por Id");

            Result response = await _orderRepository.ApproveAsync(order);
            return response;
        }

        public async Task<Result> CancellAsync(string orderId)
        {
            Order order = await _orderRepository.GetById(orderId);
            if (order == null) return Result.Fail("Erro ao buscar pedido por Id");

            Result response = await _orderRepository.CancellAsync(order);
            return response;
        }

        public async Task<Result> CreateAsync(OrderCreateVO orderCreateVO)
        {

            try
            {
                if (orderCreateVO.Products == null || orderCreateVO.Products.Count == 0)
                {
                    Result.Fail("Não foi passado nenhum produto");
                }
                int itemAmount = 0;

                int items = orderCreateVO.Products
                                 .SelectMany(dict => dict.Values)
                                 .Sum();

                if (items > 3) return Result.Fail("Não pode ter mais que 3 produtos");

           
                
                

                Enterprise enterprise = await _enterpriseRepository.GetById(orderCreateVO.EnterpriseId);

                if (enterprise == null) return Result.Fail("Empresa não localizada");

                if (enterprise.Status == ConstantsEnterprise.Waiting) return Result.Fail("Empresa ainda não foi ativada");
                if (enterprise.Status == ConstantsEnterprise.Disable) return Result.Fail("Empresa está desativada");

                List<ProductVO> productsExternalVO = await _productExtern.GetAll();
                List<string> productsExtenalString = productsExternalVO.Select(x => x.Title.Trim()).ToList();
                
                double totalAmount = 0;



                string products = String.Join(", ", orderCreateVO.Products);

                Order order = new Order()
                {
                    CustomerDocument = enterprise.Document,
                    Enterprise = enterprise,
                    //Products = products,
                    TotalAmount = totalAmount,
                    CustomerName = enterprise.TradeName,
                    OrderDate = DateTime.Now
                };


                Result response = await _orderRepository.CreateAsync(order);
                if (response.IsFailed) return Result.Fail("Erro ao criar pedido");
                string orderId = response.Successes.FirstOrDefault().Message;

                Order orderCreated = await _orderRepository.GetById(orderId);


                List<OrderItem> orderItemList = new List<OrderItem>();
                
              
                 
                foreach (Dictionary<string, int> item in orderCreateVO.Products)
                {
                    foreach (var key in item.Keys)
                    {
                        
                        string productName = key;
                        itemAmount += item[key];
                        ProductVO productVOaux = productsExternalVO.Where(x => x.Title.Trim() == key.Trim()).FirstOrDefault();
                        double price = productsExternalVO.Where(x => x.Title.Trim() == key.Trim()).FirstOrDefault().Price;
                        OrderItem orderItem = new OrderItem()
                        {
                            ProductName = productName,
                            Price = price * item[key],
                            Quantity = item[key],
                            Order = orderCreated,
                            OrderId = orderCreated.Id
                        };
                        //Result responseOrderItem = await _orderItemRepository.CreateAsync(orderItem, orderCreated);
                        orderCreated.TotalAmount += orderItem.Subtotal;
                        orderItemList.Add(orderItem);
                    }
                }
           

                Result responseOrderItem = await _orderItemRepository.CreateAsync(orderItemList);

                //Result updateOrder = await _orderRepository.UpdateAsync(orderCreated);

                return responseOrderItem;
            }
            catch(Exception ex)
            {
                return Result.Fail("Erro ao fazer pedido");
            }
            
        }

        public async Task<List<Order>> GetAll()
        {
            List<Order> orders = await _orderRepository.GetAll();

            return orders;
        }

        public async Task<List<Order>> GetAllFilteredAmount(int amount)
        {
            List<Order> orders = await _orderRepository.GetAll();
            List<Order> limitedOrders = orders.Take(amount).ToList();

            return limitedOrders;

        }

        public async Task<Order> GetById(string id)
        {
            Order order = await _orderRepository.GetById(id);
            return order;
        }
    }
}
