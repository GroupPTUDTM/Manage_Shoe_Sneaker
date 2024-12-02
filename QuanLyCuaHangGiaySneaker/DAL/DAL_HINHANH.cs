using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_HINHANH
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_HINHANH() { }
        public List<HINHANH> getHINHANHs()
        {
            var data = from ha in _dbcontext.HINHANHs select ha;
            return data.ToList();
        }
        public HINHANH getHINHANH(int id)
        {
            return getHINHANHs().FirstOrDefault(ha => ha.ID_Anh == id);
        }

    }
}
