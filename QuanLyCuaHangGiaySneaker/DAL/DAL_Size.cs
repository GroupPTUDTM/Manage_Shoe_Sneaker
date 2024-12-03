using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_Size
    {
        QLBGsDataContext _dbcontext = new QLBGsDataContext();
        public DAL_Size() { }
        public async Task<List<Size>> getSizes()
        {
            return await Task.Run(() => _dbcontext.Sizes.ToList());
        }

        public async Task<Size> getSize(int id)
        {
            var SizeList = await getSizes();
            return SizeList.FirstOrDefault(s => s.ID_Size == id);
        }

        public async Task<bool> AddSize(Size newSize)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _dbcontext.Sizes.InsertOnSubmit(newSize);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteSize(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var SizeToDelete = _dbcontext.Sizes.FirstOrDefault(s => s.ID_Size == id);
                    if (SizeToDelete == null) return false;

                    _dbcontext.Sizes.DeleteOnSubmit(SizeToDelete);
                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateSize(Size updatedSize)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var SizeToUpdate = _dbcontext.Sizes.FirstOrDefault(s => s.ID_Size == updatedSize.ID_Size);
                    if (SizeToUpdate == null) return false;

                    SizeToUpdate.Size1 = updatedSize.Size1;

                    _dbcontext.SubmitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<List<Size>> SearchSizeBySize(int size)
        {
            return await Task.Run(() =>
                _dbcontext.Sizes.Where(s => s.Size1 == size).ToList()
            );
        }

        public async Task<Size> getSizeBySize(int size)
        {
            return await Task.Run(() =>
                _dbcontext.Sizes.FirstOrDefault(s => s.Size1 == size)
            );
        }
    }
}
