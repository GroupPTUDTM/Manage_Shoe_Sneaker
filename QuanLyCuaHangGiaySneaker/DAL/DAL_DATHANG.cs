using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_DATHANG
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_DATHANG() { }
        public List<DATHANG> GetDATHANGs()
        {
            var data = from dh in _dbcontext.DATHANGs select dh;
            return data.ToList();
        }
    }
}
