using FluentResults;
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

        public OrderService(IOrderRepository orderRepository, IEnterpriseRepository enterpriseRepository, IProductExternApiService productExtern)
        {
            _orderRepository = orderRepository;
            _enterpriseRepository = enterpriseRepository;
            _productExtern = productExtern;
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

                
                if (orderCreateVO.Products.Count > 3) return Result.Fail("Não pode ter mais que 3 produtos");

           
                
                

                Enterprise enterprise = await _enterpriseRepository.GetById(orderCreateVO.EnterpriseId);

                if (enterprise == null) return Result.Fail("Empresa não localizada");

                if (enterprise.Status == ConstantsEnterprise.Waiting) return Result.Fail("Empresa ainda não foi ativada");
                if (enterprise.Status == ConstantsEnterprise.Disable) return Result.Fail("Empresa está desativada");

                List<ProductVO> productsExternalVO = await _productExtern.GetAll();
                List<string> productsExtenalString = productsExternalVO.Select(x => x.Title.Trim()).ToList();
                
                double totalAmount = 0;





                //foreach (string product in orderCreateVO.Products)
                //{
                //    if (!productsExtenalString.Contains(product))
                //    {
                //        return Result.Fail("Produto passado não está cadastrado no sistema");
                //    }
                //}
                //foreach (string productSelect in orderCreateVO.Products)
                //{
                //    ProductVO productVOaux = productsExternalVO.Where(x => x.Title.Trim() == productSelect.Trim()).FirstOrDefault();
                //    totalAmount += productVOaux.Price;
                //}

           



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
                            Price = price,
                            Quantity = item[key],
                            OrderId = orderId
                        };
                        orderItemList.Add(orderItem);
                    }
                }


                return response;
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

        public async Task<Order> GetById(string id)
        {
            Order order = await _orderRepository.GetById(id);
            return order;
        }
    }
}
