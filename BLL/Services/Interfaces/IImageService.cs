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
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>> GetAllImages();
        Task<ImageDto> AddImage(int productId, IFormFile imageFile);
        public Task Remove(int id);
    }
}
