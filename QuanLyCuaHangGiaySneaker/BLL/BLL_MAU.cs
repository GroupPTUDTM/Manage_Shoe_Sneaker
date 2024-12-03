using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_MAU
    {
        DAL_MAU _dalMAU = new DAL_MAU();
        public BLL_MAU() { }
        public async Task<List<MAU>> getMAUs()
        {
            return await _dalMAU.getMAUs();
        }

        public async Task<MAU> getMAU(int id)
        {
            return await _dalMAU.getMAU(id);
        }
        public async Task<bool> AddMAU(MAU newMAU)
        {
            return await _dalMAU.AddMAU(newMAU);
        }

        public async Task<bool> DeleteMAU(int id)
        {
            return await _dalMAU.DeleteMAU(id);
        }

        public async Task<bool> UpdateMAU(MAU updatedMAU)
        {
            return await _dalMAU.UpdateMAU(updatedMAU);
        }

        public async Task<List<MAU>> SearchMAUByName(string name)
        {
            return await _dalMAU.SearchMAUByName(name);
        }

        public async Task<MAU> getMAUByName(string name)
        {
            return await _dalMAU.getMAUByName(name);
        }

    }
}
