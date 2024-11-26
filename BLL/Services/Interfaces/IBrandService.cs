using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrands();
        Task<BrandDto> AddBrand(BrandDto brandDto);
        Task UpdateBrand(BrandDto brandDto);
        Task DeleteBrand(int id);

    }
}
