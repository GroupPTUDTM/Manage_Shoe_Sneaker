using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_HINHANH
    {
        DAL_HINHANH _dalHINHANH = new DAL_HINHANH();
        public BLL_HINHANH() { }
        public async Task<List<HINHANH>> getHINHANHs()
        {
            return await _dalHINHANH.getHINHANHs();
        }

        public async Task<HINHANH> getHINHANH(int id)
        {
            return await _dalHINHANH.getHINHANH(id);
        }
        public async Task<bool> AddHINHANH(HINHANH newHINHANH)
        {
            return await _dalHINHANH.AddHINHANH(newHINHANH);
        }

        public async Task<bool> DeleteHINHANH(int id)
        {
            return await _dalHINHANH.DeleteHINHANH(id);
        }

        public async Task<bool> UpdateHINHANH(HINHANH updatedHINHANH)
        {
            return await _dalHINHANH.UpdateHINHANH(updatedHINHANH);
        }


        public async Task<HINHANH> getHINHANHByName(string name)
        {
            return await _dalHINHANH.getHINHANHByName(name);
        }
    }
}
