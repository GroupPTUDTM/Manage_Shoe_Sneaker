using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_SANPHAM
    {
        DAL_SANPHAM _dalSANPHAM = new DAL_SANPHAM();

        public BLL_SANPHAM() { }

        // Thay đổi phương thức thành bất đồng bộ
        public async Task<List<SANPHAM>> getSANPHAMs()
        {
            return await _dalSANPHAM.getSANPHAMs();
        }

        // Thay đổi phương thức thành bất đồng bộ
        public async Task<SANPHAM> getSANPHAM(int id)
        {
            return await _dalSANPHAM.getSANPHAM(id);
        }
    }
}
