using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_DATHANG
    {
        DAL_DATHANG _dalDATHANG = new DAL_DATHANG();
        public BLL_DATHANG() { }
        public List<DATHANG> GetDATHANGs()
        {
            return _dalDATHANG.GetDATHANGs();
        }
        public DATHANG getDATHANG(int id)
        {
            return _dalDATHANG.getDATHANG(id);
        }

    }
}
