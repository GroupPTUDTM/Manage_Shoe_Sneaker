using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_SANPHAM
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();

        public DAL_SANPHAM() { }

        public async Task<List<SANPHAM>> getSANPHAMs()
        {
            return await Task.Run(() => _dbcontext.SANPHAMs.ToList());
        }

        public async Task<SANPHAM> getSANPHAM(int id)
        {
            var sanPhamList = await getSANPHAMs();
            return sanPhamList.FirstOrDefault(sp => sp.ID_SanPham == id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddSANPHAM(SANPHAM newSanPham)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _dbcontext.SANPHAMs.InsertOnSubmit(newSanPham);
                    _dbcontext.SubmitChanges();
                    return (true, null);
                }
                catch (Exception ex)
                {
                    // Ghi lại thông tin lỗi
                    return (false, ex.Message);
                }
            });
        }


        public async Task<bool> DeleteSANPHAM(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sanPhamToDelete = _dbcontext.SANPHAMs.FirstOrDefault(sp => sp.ID_SanPham == id);
                    if (sanPhamToDelete == null) return false;

                    _dbcontext.SANPHAMs.DeleteOnSubmit(sanPhamToDelete);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateSANPHAM(SANPHAM updatedSanPham)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sanPhamToUpdate = _dbcontext.SANPHAMs.FirstOrDefault(sp => sp.ID_SanPham == updatedSanPham.ID_SanPham);
                    if (sanPhamToUpdate == null) return (false, "Thông tin sửa rỗng");

                    sanPhamToUpdate.TenSanPham = updatedSanPham.TenSanPham;
                    sanPhamToUpdate.ID_ThuongHieu = updatedSanPham.ID_ThuongHieu;
                    sanPhamToUpdate.ID_DanhMuc = updatedSanPham.ID_DanhMuc;
                    sanPhamToUpdate.ID_Anh = updatedSanPham.ID_Anh;
                    sanPhamToUpdate.ID_Size = updatedSanPham.ID_Size;
                    sanPhamToUpdate.Mota = updatedSanPham.Mota;
                    sanPhamToUpdate.DonViGia = updatedSanPham.DonViGia;
                    sanPhamToUpdate.SoLuongTon = updatedSanPham.SoLuongTon;

                    _dbcontext.SubmitChanges();
                    return (true, null);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            });
        }

        public async Task<List<SANPHAM>> SearchSANPHAMByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.SANPHAMs.Where(sp => sp.TenSanPham.Contains(name)).ToList()
            );
        }

        public async Task<SANPHAM> getSANPHAMByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.SANPHAMs.FirstOrDefault(sp => sp.TenSanPham == name)
            );
        }
    }
}
