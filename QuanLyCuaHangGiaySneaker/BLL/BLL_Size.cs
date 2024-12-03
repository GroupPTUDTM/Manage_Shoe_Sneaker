using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_Size
    {
        DAL_Size _dalSize = new DAL_Size();
        public BLL_Size() { }
        public async Task<List<Size>> getSizes()
        {
            return await _dalSize.getSizes();
        }

        public async Task<Size> getSize(int id)
        {
            return await _dalSize.getSize(id);
        }
        public async Task<bool> AddSize(Size newSize)
        {
            return await _dalSize.AddSize(newSize);
        }

        public async Task<bool> DeleteSize(int id)
        {
            return await _dalSize.DeleteSize(id);
        }

        public async Task<bool> UpdateSize(Size updatedSize)
        {
            return await _dalSize.UpdateSize(updatedSize);
        }

        public async Task<List<Size>> SearchSizeBySize(int size)
        {
            return await _dalSize.SearchSizeBySize(size);
        }

        public async Task<Size> getSizeBySize(int size)
        {
            return await _dalSize.getSizeBySize(size);
        }

    }
}
