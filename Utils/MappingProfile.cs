using AutoMapper;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Brand, BrandDto>()

                     .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0))
                     .ForMember(d => d.ImageUrl, m => m.Ignore());

            CreateMap<BrandDto, Brand>()
                .ForMember(d => d.ImageUrl, m => m.Ignore());

            CreateMap<Category, CategoryDto>()
                     .ForMember(d => d.Estatus, m => m.MapFrom(o => o.Estatus == true ? 1 : 0));

            CreateMap<Subcategory, SubcategoryDto>()
                     .ForMember(d => d.Estatus, m => m.MapFrom(o => o.Estatus == true ? 1 : 0))
                     .ForMember(d => d.NameCategory, m => m.MapFrom(o => o.Category.NameCategory));
        }
    }
}
