using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO;
using VendaCorp.Application.DTO.Order;
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

        public Task<Result> ApproveAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CancellAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CreateAsync(OrderCreateVO orderCreateVO)
        {

            try
            {
                if (orderCreateVO.Products == null || orderCreateVO.Products.Count == 0)
                {
                    Result.Fail("Não foi passado nenhum produto");
                }
                bool hasRepeatedNames = orderCreateVO.Products
                 .GroupBy(name => name)
                 .Any(group => group.Count() > 3);

                if (hasRepeatedNames) return Result.Fail("Não pode repetir o mesmo produto mais que 3 vezes");

                Enterprise enterprise = await _enterpriseRepository.GetById(orderCreateVO.EnterpriseId);

                if (enterprise == null) return Result.Fail("Empresa não localizada");

                List<ProductVO> productsExternalVO = await _productExtern.GetAll();
                List<string> productsExtenalString = productsExternalVO.Select(x => x.Title).ToList();
                double totalAmount = 0;

                foreach(string productValue in productsExtenalString)
                {
                    ProductVO productVOaux = productsExternalVO.Where(x => x.Title == productValue).FirstOrDefault();
                    totalAmount += productVOaux.Price;
                }

                foreach (string product in orderCreateVO.Products)
                {
                    if (!productsExtenalString.Contains(product))
                    {
                        return Result.Fail("Produto passado não está cadastrado no sistema");
                    }
                }


                string products = String.Join(", ", orderCreateVO.Products);

                Order order = new Order()
                {
                    CustomerDocument = enterprise.Document,
                    Enterprise = enterprise,
                    Products = products,
                    TotalAmount = totalAmount,
                    CustomerName = enterprise.TradeName,
                    OrderDate = DateTime.Now
                };

                Result response = await _orderRepository.CreateAsync(order);

                return response;
            }
            catch(Exception ex)
            {
                return Result.Fail("Erro ao fazer pedido");
            }
            
        }
    }
}
