using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_THUONGHIEU
    {
        DAL_THUONGHIEU _dalTHUONGHIEU = new DAL_THUONGHIEU();
        public BLL_THUONGHIEU() { }
        public List<THUONGHIEU> getTHUONGHIEUs()
        {
            return _dalTHUONGHIEU.getTHUONGHIEUs();
        }
        public THUONGHIEU getTHUONGHIEU(int id)
        {
            return _dalTHUONGHIEU.getTHUONGHIEU(id);
        }
    }
}
