using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_THUONGHIEU
    {
        DAL_THUONGHIEU _dalTHUONGHIEU = new DAL_THUONGHIEU();
        public BLL_THUONGHIEU() { }
        public async Task<List<THUONGHIEU>> getTHUONGHIEUs()
        {
            return await _dalTHUONGHIEU.getTHUONGHIEUs();
        }

        public async Task<THUONGHIEU> getTHUONGHIEU(int id)
        {
            return await _dalTHUONGHIEU.getTHUONGHIEU(id);
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> AddTHUONGHIEU(THUONGHIEU newTHUONGHIEU)
        {
            return await _dalTHUONGHIEU.AddTHUONGHIEU(newTHUONGHIEU);
        }

        public async Task<bool> DeleteTHUONGHIEU(int id)
        {
            return await _dalTHUONGHIEU.DeleteTHUONGHIEU(id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateTHUONGHIEU(THUONGHIEU updatedTHUONGHIEU)
        {
            return await _dalTHUONGHIEU.UpdateTHUONGHIEU(updatedTHUONGHIEU);
        }

        public async Task<List<THUONGHIEU>> SearchTHUONGHIEUByName(string name)
        {
            return await _dalTHUONGHIEU.SearchTHUONGHIEUByName(name);
        }

        public async Task<THUONGHIEU> getTHUONGHIEUByName(string name)
        {
            return await _dalTHUONGHIEU.getTHUONGHIEUByName(name);
        }
    }
}
