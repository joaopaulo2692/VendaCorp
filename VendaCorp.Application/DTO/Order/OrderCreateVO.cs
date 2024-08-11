using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Application.DTO.Order
{
    public class OrderCreateVO
    {
        public string OrderItems { get; set; }

        [Required(ErrorMessage = "Favor informar os produtos.")]
        public List<string> Products { get; set; }

        [Required(ErrorMessage = "Favor informar o nome da empresa.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Favor informar o documento da Empresa.")]
        public string CustomerDocument { get; set; }

        [Required(ErrorMessage = "Favor informar o Id da Gerencia.")]
        public int EnterpriseId { get; set; }
    }
}
