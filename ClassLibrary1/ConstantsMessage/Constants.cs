using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaCorp.Core.ConstantsMessage
{
    public class Constants
    {
    }
    public static class ConstantsEnterprise
    {
        public static string Activate = "Ativo";
        public static string Disable = "Inativo";
        public static string Waiting = "Aguardando Ativação";
    }

    public static class ContantsOrder
    {
        public static string Created = "Criado";
        public static string Approved = "Aprovado";
        public static string Cancelled = "Cancelado";
    }

    public static class ContantsDeliveryOrder
    {
        public static string Peding = "Pendente";
        public static string OnTheWay = "Em caminho";
        public static string Delivered = "Entregue";
    }
}
