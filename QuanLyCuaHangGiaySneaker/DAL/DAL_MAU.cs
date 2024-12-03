using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_MAU
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_MAU() { }
        public async Task<List<MAU>> getMAUs()
        {
            return await Task.Run(() => _dbcontext.MAUs.ToList());
        }

        public async Task<MAU> getMAU(int id)
        {
            var MAUList = await getMAUs();
            return MAUList.FirstOrDefault(m => m.ID_Mau == id);
        }

        public async Task<bool> AddMAU(MAU newMAU)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _dbcontext.MAUs.InsertOnSubmit(newMAU);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteMAU(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var MAUToDelete = _dbcontext.MAUs.FirstOrDefault(m => m.ID_Mau == id);
                    if (MAUToDelete == null) return false;

                    _dbcontext.MAUs.DeleteOnSubmit(MAUToDelete);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateMAU(MAU updatedMAU)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var MAUToUpdate = _dbcontext.MAUs.FirstOrDefault(m => m.ID_Mau == updatedMAU.ID_Mau);
                    if (MAUToUpdate == null) return false;

                    MAUToUpdate.TenMau = updatedMAU.TenMau;

                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<List<MAU>> SearchMAUByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.MAUs.Where(m => m.TenMau.Contains(name)).ToList()
            );
        }

        public async Task<MAU> getMAUByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.MAUs.FirstOrDefault(m => m.TenMau == name)
            );
        }

    }
}
