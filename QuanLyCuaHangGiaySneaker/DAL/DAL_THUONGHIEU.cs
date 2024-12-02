using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_THUONGHIEU
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_THUONGHIEU() { }
        public List<THUONGHIEU> getTHUONGHIEUs()
        {
            var data = from th in _dbcontext.THUONGHIEUs select th;
            return data.ToList();
        }
        public THUONGHIEU getTHUONGHIEU(int id)
        {
            return getTHUONGHIEUs().FirstOrDefault(th => th.ID_ThuongHieu == id);
        }
    }
}
