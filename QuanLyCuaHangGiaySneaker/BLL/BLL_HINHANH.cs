using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_HINHANH
    {
        DAL_HINHANH _dalHINHANH = new DAL_HINHANH();
        public BLL_HINHANH() { }
        public List<HINHANH> getHINHANHs()
        {
            return _dalHINHANH.getHINHANHs();
        }
        public HINHANH getHINHANH(int id)
        {
            return _dalHINHANH.getHINHANH(id);
        }

    }
}
