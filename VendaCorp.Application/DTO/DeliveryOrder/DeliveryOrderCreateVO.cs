using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Application.DTO.DeliveryOrder
{
    public class DeliveryOrderCreateVO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Favor informar o Id do Pedido.")]
        public string OrderId { get; set; }
        [Required(ErrorMessage = "Favor informar a data de entrega.")]
        public DateTime DeliveryDate { get; set; }
        [Required(ErrorMessage = "Favor informar a transportadora.")]
        public string ShippingCompanyName { get; set; }

        [Required(ErrorMessage = "Favor informar o endereço.")]
        public string CustomerAddress { get; set; }

    }
}
