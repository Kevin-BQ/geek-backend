using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUploadImage
    {
        Task<string> UploadImageAsync(IFormFile formFile, string file);
        Task<string> DeleteImage(string imageUrl);

        string ExtractUrlImage(string imageUrl);

    }
}
