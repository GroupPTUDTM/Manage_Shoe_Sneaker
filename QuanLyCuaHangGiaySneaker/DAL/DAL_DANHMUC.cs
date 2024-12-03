using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_DANHMUC
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();

        public DAL_DANHMUC() { }

        public async Task<List<DANHMUC>> getDANHMUCs()
        {
            return await Task.Run(() => _dbcontext.DANHMUCs.ToList());
        }

        public async Task<DANHMUC> getDANHMUC(int id)
        {
            var danhMucList = await getDANHMUCs();
            return danhMucList.FirstOrDefault(dm => dm.ID_DanhMuc == id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddDANHMUC(DANHMUC newDanhMuc)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _dbcontext.DANHMUCs.InsertOnSubmit(newDanhMuc);
                    _dbcontext.SubmitChanges();
                    return (true, null);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            });
        }

        public async Task<bool> DeleteDANHMUC(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var danhMucToDelete = _dbcontext.DANHMUCs.FirstOrDefault(dm => dm.ID_DanhMuc == id);
                    if (danhMucToDelete == null) return false;

                    _dbcontext.DANHMUCs.DeleteOnSubmit(danhMucToDelete);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateDANHMUC(DANHMUC updatedDanhMuc)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var danhMucToUpdate = _dbcontext.DANHMUCs.FirstOrDefault(dm => dm.ID_DanhMuc == updatedDanhMuc.ID_DanhMuc);
                    if (danhMucToUpdate == null) return (false, "Thông tin sửa rỗng");

                    danhMucToUpdate.TenDanhMuc = updatedDanhMuc.TenDanhMuc;

                    _dbcontext.SubmitChanges();
                    return (true, null);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }
            });
        }

        public async Task<List<DANHMUC>> SearchDANHMUCByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.DANHMUCs.Where(dm => dm.TenDanhMuc.Contains(name)).ToList()
            );
        }

        public async Task<DANHMUC> getDANHMUCByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.DANHMUCs.FirstOrDefault(dm => dm.TenDanhMuc == name)
            );
        }
    }
}
