using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Thêm thư viện này cho async/await
using DTO;

namespace DAL
{
    public class DAL_SANPHAM
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();

        public DAL_SANPHAM() { }

        // Sử dụng async/await cho phương thức lấy danh sách sản phẩm
        public async Task<List<SANPHAM>> getSANPHAMs()
        {
            return await Task.Run(() => _dbcontext.SANPHAMs.ToList());
        }

        // Sử dụng async/await cho phương thức lấy một sản phẩm
        public async Task<SANPHAM> getSANPHAM(int id)
        {
            var sanPhamList = await getSANPHAMs();
            return sanPhamList.FirstOrDefault(sp => sp.ID_SanPham == id);
        }
    }
}
