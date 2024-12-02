using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DANHMUC
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_DANHMUC() { }
        public List<DANHMUC> getDANHMUCs()
        {
            var data = from dm in _dbcontext.DANHMUCs select dm;
            return data.ToList();
        }
        public DANHMUC getDANHMUC(int id)
        {
            return getDANHMUCs().FirstOrDefault(dm => dm.ID_DanhMuc == id);
        }

    }
}
