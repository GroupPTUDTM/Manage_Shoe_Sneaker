using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_DANHMUC
    {
        DAL_DANHMUC _dalDANHMUC = new DAL_DANHMUC();

        public BLL_DANHMUC() { }

        public async Task<List<DANHMUC>> getDANHMUCs()
        {
            return await _dalDANHMUC.getDANHMUCs();
        }

        public async Task<DANHMUC> getDANHMUC(int id)
        {
            return await _dalDANHMUC.getDANHMUC(id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddDANHMUC(DANHMUC newDANHMUC)
        {
            return await _dalDANHMUC.AddDANHMUC(newDANHMUC);
        }

        public async Task<bool> DeleteDANHMUC(int id)
        {
            return await _dalDANHMUC.DeleteDANHMUC(id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateDANHMUC(DANHMUC updatedDANHMUC)
        {
            return await _dalDANHMUC.UpdateDANHMUC(updatedDANHMUC);
        }

        public async Task<List<DANHMUC>> SearchDANHMUCByName(string name)
        {
            return await _dalDANHMUC.SearchDANHMUCByName(name);
        }

        public async Task<DANHMUC> getDANHMUCByName(string name)
        {
            return await _dalDANHMUC.getDANHMUCByName(name);
        }
    }
}
