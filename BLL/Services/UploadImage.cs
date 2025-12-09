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
            Console.WriteLine(uploadResult.JsonObj);
            // Retorna la url en un string
            return uploadResult.SecureUrl.ToString();
        }

        public async Task<string> DeleteImage(string imageUrl)
        {
            // Extreamos la cadena para poder eliminar
            var publicId = ExtractUrlImage(imageUrl);

            // Configurar los parametros para eliminar
            var deleteParams = new DelResParams()
            {
                // Busca el id solicitado
                PublicIds = new List<string> { publicId },

                // Busca el recurso bajo el metodo en que se subio
                Type = "upload",

                // Tipo del recurso 
                ResourceType = ResourceType.Image
            };

            // Se realiza la solicitud de eliminacion 
            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);

            Console.WriteLine(result.JsonObj);

            // Verifica el estado de la respuesta
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return "Imagen eliminada correctamente.";
            }

            // Se lanza una excepcion en caso de que no se elimine
            throw new Exception($"Error al eliminar la imagen: {result.JsonObj}");
        }

        public string ExtractUrlImage(string imageUrl)
        {
            // Transforma el string a un objeto Uri
            var uri = new Uri(imageUrl);

            // La uri se divide en segmentos 
            var segments = uri.AbsolutePath.Split('/');

            // Se obtiene el ultmio segmento (id de la imagen con extension) con todo y extension (png, jpg, ...)
            var publicIdWithExtension = segments[^1];

            // Se divide y se quita la extension 
            var publicId = publicIdWithExtension.Split(".")[0];

            // Si hay mas de 4 segmentos asume que esta en una carpeta
            if (segments.Length > 4)
            {
                // Se obtiene el antepenultimo segmento (folder) de la uri 
                var folder = segments[^2];

                // Se alamcena en la variable el nombre del folder y el id de la imagen
                publicId = $"{folder}/{publicId}";
            }

            return publicId;
        }

    }
}
