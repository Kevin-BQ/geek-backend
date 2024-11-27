using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrands();
        Task<BrandDto> AddBrand(BrandDto brandDto);
        Task UpdateBrand(BrandDto brandDto);
        Task DeleteBrand(int id);
        Task<string> UploadImageAsync(IFormFile formFile);

        Task<IEnumerable<Brand>> GetBrandsAssests();


    }
}
