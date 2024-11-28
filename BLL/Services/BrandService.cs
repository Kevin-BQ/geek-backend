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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IUploadImage _uploadImage;

        public BrandService(IWorkUnit workUnit, IMapper mapper, IUploadImage uploadImage)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _uploadImage = uploadImage;
        }

        public async Task<BrandDto> AddBrand(BrandDto brandDto)
        {
            try
            {
                string file = "brands";
                string imageUrl = await _uploadImage.UploadImageAsync(brandDto.ImageUrl, file);

                Brand brand = new Brand
                {
                    NameBrand = brandDto.NameBrand,
                    ImageUrl = imageUrl,
                    Status = brandDto.Status == 1 ? true : false
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

                if (brandDto.ImageUrl != null)
                {
                    string file = "brands";
                    string newImageUrl = await _uploadImage.UploadImageAsync(brandDto.ImageUrl, file);
                    brandDb.ImageUrl = newImageUrl;
                }

                brandDb.NameBrand = brandDto.NameBrand;
                brandDb.Status = brandDto.Status == 1 ? true : false;

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

                brandDb.Status = false;

                _workUnit.Brand.Update(brandDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            try
            {
                var lista = await _workUnit.Brand.GetAll(
                                    orderBy: e => e.OrderBy(e => e.NameBrand));

                return _mapper.Map<IEnumerable<Brand>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Brand>> GetBrandsAssests()
        {
            try
            {
                var lista = await _workUnit.Brand.GetAll(
                                    filtro: e => e.Status == true,
                                    orderBy: e => e.OrderBy(e => e.NameBrand));

                return _mapper.Map<IEnumerable<Brand>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
