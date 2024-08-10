using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Application.DTO;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.Infrastructure.Serivces
{
    public class ProductExternApiService : IProductExternApiService
    {
        public async Task<List<ProductVO>> GetAll()
        {
            string apiUrl = "https://fakestoreapi.com/products";

            
            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();


                List<ProductVO> data = JsonConvert.DeserializeObject<List<ProductVO>>(responseBody);

                List<ProductVO> filteredList = data.Where(x => x.Category == "electronics" || x.Category == "jewelery").ToList();
                return filteredList;
              

            }
            catch (HttpRequestException e)
            {
                throw new NotImplementedException();
            }
        
            
        }
    }
}
