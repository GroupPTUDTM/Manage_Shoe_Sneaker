using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_THUONGHIEU
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_THUONGHIEU() { }

        public async Task<List<THUONGHIEU>> getTHUONGHIEUs()
        {
            return await Task.Run(() => _dbcontext.THUONGHIEUs.ToList());
        }

        public async Task<THUONGHIEU> getTHUONGHIEU(int id)
        {
            var THUONGHIEUList = await getTHUONGHIEUs();
            return THUONGHIEUList.FirstOrDefault(th => th.ID_ThuongHieu == id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddTHUONGHIEU(THUONGHIEU newTHUONGHIEU)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _dbcontext.THUONGHIEUs.InsertOnSubmit(newTHUONGHIEU);
                    _dbcontext.SubmitChanges();
                    return (true, null);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            });
        }

        public async Task<bool> DeleteTHUONGHIEU(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var THUONGHIEUToDelete = _dbcontext.THUONGHIEUs.FirstOrDefault(th => th.ID_ThuongHieu == id);
                    if (THUONGHIEUToDelete == null) return false;

                    _dbcontext.THUONGHIEUs.DeleteOnSubmit(THUONGHIEUToDelete);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateTHUONGHIEU(THUONGHIEU updatedTHUONGHIEU)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var THUONGHIEUToUpdate = _dbcontext.THUONGHIEUs.FirstOrDefault(th => th.ID_ThuongHieu == updatedTHUONGHIEU.ID_ThuongHieu);
                    if (THUONGHIEUToUpdate == null) return (false, "Thông tin sửa rỗng");

                    THUONGHIEUToUpdate.TenThuongHieu = updatedTHUONGHIEU.TenThuongHieu;

                    _dbcontext.SubmitChanges();
                    return (true, null);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            });
        }

        public async Task<List<THUONGHIEU>> SearchTHUONGHIEUByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.THUONGHIEUs.Where(th => th.TenThuongHieu.Contains(name)).ToList()
            );
        }

        public async Task<THUONGHIEU> getTHUONGHIEUByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.THUONGHIEUs.FirstOrDefault(th => th.TenThuongHieu == name)
            );
        }
    }
}
