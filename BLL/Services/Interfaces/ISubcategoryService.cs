using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryDto>> GetAllSubcategories();
        Task<SubcategoryDto> AddSubcategory(SubcategoryDto subcategoryDto);
        Task UpdateSubcategory(SubcategoryDto subcategoryDto);
        Task DeleteSubcategory(int id);
    }
}
