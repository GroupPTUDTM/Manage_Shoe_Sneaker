using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_Size
    {
        DAL_Size _dalSize = new DAL_Size();
        public BLL_Size() { }
        public List<Size> getSizes()
        {
            return _dalSize.getSizes();
        }
        public Size getSIZE(int id)
        {
            return _dalSize.getSize(id);
        }

    }
}
