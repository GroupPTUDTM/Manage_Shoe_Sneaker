using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Size
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_Size() { }
        public List<Size> getSizes()
        {
            var data = from s in _dbcontext.Sizes select s;
            return data.ToList();
        }
        public Size getSize(int id)
        {
            return getSizes().FirstOrDefault(s => s.ID_Size == id);
        }
    }
}
