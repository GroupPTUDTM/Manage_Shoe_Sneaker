using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_HINHANH
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_HINHANH() { }
        public async Task<List<HINHANH>> getHINHANHs()
        {
            return await Task.Run(() => _dbcontext.HINHANHs.ToList());
        }

        public async Task<HINHANH> getHINHANH(int id)
        {
            var HINHANHList = await getHINHANHs();
            return HINHANHList.FirstOrDefault(ha => ha.ID_Anh == id);
        }

        public async Task<bool> AddHINHANH(HINHANH newHINHANH)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _dbcontext.HINHANHs.InsertOnSubmit(newHINHANH);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteHINHANH(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var HINHANHToDelete = _dbcontext.HINHANHs.FirstOrDefault(ha => ha.ID_Anh == id);
                    if (HINHANHToDelete == null) return false;

                    _dbcontext.HINHANHs.DeleteOnSubmit(HINHANHToDelete);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateHINHANH(HINHANH updatedHINHANH)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var HINHANHToUpdate = _dbcontext.HINHANHs.FirstOrDefault(ha => ha.ID_Anh == updatedHINHANH.ID_Anh);
                    if (HINHANHToUpdate == null) return false;

                    HINHANHToUpdate.AnhChinh = updatedHINHANH.AnhChinh;
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<HINHANH> getHINHANHByName(string name)
        {
            return await Task.Run(() =>
                _dbcontext.HINHANHs.FirstOrDefault(ha => ha.AnhChinh == name)
            );
        }

    }
}
