﻿using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface IShipRejected
    {
        Task<IEnumerable<RejectShipmentModel>> GetRejectedShipData(int proc);
        Task<bool> InsertShipRejectData(RejectShipmentModel ncr, int proc);
        Task<bool> UpdateShipRejectData(RejectShipmentModel ncr);
    }
}
