using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ImageRepository: Repository<Image>, IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Image image)
        {
            var imageDb = _context.Images.FirstOrDefault(e => e.Id == image.Id);

            if (image != null)
            {
                imageDb.UrlImage = image.UrlImage;
                imageDb.ProductId = image.ProductId;
                _context.SaveChanges();
            }
        }
    }
}
