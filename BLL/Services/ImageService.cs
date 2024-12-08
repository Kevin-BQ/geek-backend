using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IUploadImage _uploadImage;

        public ImageService(IWorkUnit workUnit, IMapper mapper, IUploadImage uploadImage)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _uploadImage = uploadImage;
        }

        public async Task<ImageDto> AddImage(int Id, IFormFile imageFile)
        {
            try
            {
                var product = _workUnit.Product.GetFirst(d => d.Id == Id);

                if (product == null)
                {
                    throw new InvalidOperationException("Producto no encontrado.");
                }

                string urlImage = await _uploadImage.UploadImageAsync(imageFile, "products");

                Image image = new Image
                {
                    UrlImage = urlImage,
                    ProductId = product.Id
                };
    
                await _workUnit.Image.Add(image);
                await _workUnit.Save();

                if (image.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agregar una nueva Categoria");
                }

                return _mapper.Map<ImageDto>(image);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var imageDb = await _workUnit.Image.GetFirst(e => e.Id == id);
                if (imageDb == null)
                {
                    throw new TaskCanceledException("La Imagen no Existe");
                }
                _uploadImage.DeleteImage(imageDb.UrlImage);
                _workUnit.Image.Remove(imageDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ImageDto>> GetAllImages()
        {
            try
            {
                var lista = await _workUnit.Image.GetAll(
                                    incluirPropiedades: "Product",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<ImageDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
