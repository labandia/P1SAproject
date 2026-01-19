using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class MetalMaskTransactionServices : IMetalMast_Transaction
    {
        public Task<List<MetalMaskTransaction>> GetMetalMaskTransaction(string search, int Stats, int ModelType, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<MetalMaskTransaction> GetMetalMaskTransacDetails()
        {
            throw new NotImplementedException();
        }


        public Task<bool> AddMetalMastTransaction(MetalMaskTransaction metal)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMetalMastTransaction(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMetalMastTransaction(MetalMaskTransaction metal)
        {
            throw new NotImplementedException();
        }
       
    }
}