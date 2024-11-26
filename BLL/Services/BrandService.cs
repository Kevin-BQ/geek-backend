using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public BrandService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<BrandDto> AddBrand(BrandDto brandDto)
        {
            try
            {
                Brand brand = new Brand
                {
                    NameBrand = brandDto.NameBrand,
                    ImageUrl = brandDto.ImageUrl,
                    Estado = brandDto.Estado == 1 ? true : false
                };

                await _workUnit.Brand.Add(brand);
                await _workUnit.Save();

                if (brand.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agrear una nueva Marca");
                }

                return _mapper.Map<BrandDto>(brand);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateBrand(BrandDto brandDto)
        {
            try
            {
                var brandDb = await _workUnit.Brand.GetFirst(e => e.Id == brandDto.Id);
                
                if (brandDb == null)
                {
                    throw new TaskCanceledException("La Marca no Existe");
                }

                brandDb.NameBrand = brandDto.NameBrand;
                brandDb.ImageUrl = brandDto.ImageUrl;
                brandDb.Estado = brandDto.Estado == 1 ? true : false;

                _workUnit.Brand.Update(brandDb);

                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteBrand(int id)
        {
            try
            {
                var brandDb = await _workUnit.Brand.GetFirst(e => e.Id == id);

                if (brandDb == null)
                {
                    throw new TaskCanceledException("La Marca no Existe");
                }

                _workUnit.Brand.Remove(brandDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            try
            {
                var lista = await _workUnit.Brand.GetAll(
                                    orderBy: e => e.OrderBy(e => e.NameBrand));

                return _mapper.Map<IEnumerable<BrandDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
