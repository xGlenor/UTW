using BaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Contracts
{
    public interface IFeeRepository
    {
        Task<List<Fee>> GetAll();
        Task<Fee?> GetById(int feeID);
        Task<Fee> Insert(Fee fee);
        Task<Fee> Update(int id, Fee fee);
        Task<Fee> Delete(int feeID);
    }
}
