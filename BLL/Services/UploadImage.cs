using BLL.Services.Interfaces;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UploadImage: IUploadImage
    {
        private readonly Cloudinary _cloudinary;

        public UploadImage(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile formFile, string file)
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
                Folder = file
            };

            // Se sube la imagen a cloudinary con los pareametros configurados
            var uploadResult = await _cloudinary.UploadAsync(uploadParms);

            // Verifica la respuesta si es exitosa
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error al Subir la Imagen");
            }

            // Retorna la url en un string
            return uploadResult.SecureUrl.ToString();
        }
    }
}
