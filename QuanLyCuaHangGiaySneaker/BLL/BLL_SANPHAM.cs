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

        public async Task<List<SANPHAM>> getSANPHAMs()
        {
            return await _dalSANPHAM.getSANPHAMs();
        }

        public async Task<SANPHAM> getSANPHAM(int id)
        {
            return await _dalSANPHAM.getSANPHAM(id);
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> AddSANPHAM(SANPHAM newSanPham)
        {
            return await _dalSANPHAM.AddSANPHAM(newSanPham);
        }


        public async Task<bool> DeleteSANPHAM(int id)
        {
            return await _dalSANPHAM.DeleteSANPHAM(id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateSANPHAM(SANPHAM updatedSanPham)
        {
            return await _dalSANPHAM.UpdateSANPHAM(updatedSanPham);
        }

        public async Task<List<SANPHAM>> SearchSANPHAMByName(string name)
        {
            return await _dalSANPHAM.SearchSANPHAMByName(name);
        }

        public async Task<SANPHAM> getSANPHAMByName(string name)
        {
            return await _dalSANPHAM.getSANPHAMByName(name);
        }
    }
}
