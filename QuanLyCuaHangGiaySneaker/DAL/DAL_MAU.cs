using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_MAU
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_MAU() { }
        public List<MAU> getMAUs()
        {
            var data = from m in _dbcontext.MAUs select m;
            return data.ToList();
        }
        public MAU getMAU(int id)
        {
            return getMAUs().FirstOrDefault(m => m.ID_Mau == id);
        }

    }
}
