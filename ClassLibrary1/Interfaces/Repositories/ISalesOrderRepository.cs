﻿using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;

namespace VendaCorp.Core.Interfaces.Repositories
{
    public interface ISalesOrderRepository
    {
        public Task<List<DeliveryOrder>> GetAllAsync();
        public Task<Result> CreateAsync(DeliveryOrder salesOrder);
        public Task<Result> CancelAsync(DeliveryOrder salesOrder);
        public Task<Result> ApproveAsync(DeliveryOrder salesOrder);
    }
}
