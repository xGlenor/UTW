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
        IEnumerable<Fee> GetAll();
        Fee GetById(int FeeID);
        void Insert(Fee fee);
        void Update(Fee fee);
        void Delete(int FeeID);
        void Save();
    }
}
