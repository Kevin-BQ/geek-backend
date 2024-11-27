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
        private readonly Cloudinary _cloudinary;

        public BrandService(IWorkUnit workUnit, IMapper mapper, Cloudinary cloudinary)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<BrandDto> AddBrand(BrandDto brandDto)
        {
            try
            {
                string imageUrl = await UploadImageAsync(brandDto.ImageUrl);

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
                    string newImageUrl = await UploadImageAsync(brandDto.ImageUrl);
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

        public async Task<string> UploadImageAsync(IFormFile formFile)
        {
            // Verifica si el file esta vacio
            if (formFile == null)
            {
                return null;
            }

            // Abre el archivo para leerlo como un flujo de datos
            await using var stream = formFile.OpenReadStream();

            // Clase de Cloudinary
            var uploadParms = new ImageUploadParams
            {
                // File el cual contiene el nombre del archivo y su flujo de datos
                File = new FileDescription(formFile.FileName, stream),
                // Nombre de la carpeta donde se alamcenara
                Folder = "brands"
            };

            // Se sube la imagen a cloudinary con los pareametros configurados
            var uploadResult = await _cloudinary.UploadAsync(uploadParms);

            // Verifica la respuesta si es exita
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error al Subir la Imagen");
            }
            
            // Retorna la url en un string
            return uploadResult.SecureUrl.ToString();
        }

    }
}
