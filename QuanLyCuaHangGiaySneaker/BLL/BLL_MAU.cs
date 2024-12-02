using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_MAU
    {
        DAL_MAU _dalMAU = new DAL_MAU();
        public BLL_MAU() { }
        public List<MAU> getMAUs()
        {
            return _dalMAU.getMAUs();
        }
        public MAU getMAU(int id)
        {
            return _dalMAU.getMAU(id);
        }

    }
}
