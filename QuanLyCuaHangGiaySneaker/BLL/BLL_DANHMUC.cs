using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_DANHMUC
    {
        DAL_DANHMUC _dalDANHMUC = new DAL_DANHMUC();
        public BLL_DANHMUC() { }
        public List<DANHMUC> getDANHMUCs()
        {
            return _dalDANHMUC.getDANHMUCs();
        }
        public DANHMUC getDANHMUC(int id)
        {
            return _dalDANHMUC.getDANHMUC(id);
        }

    }
}
