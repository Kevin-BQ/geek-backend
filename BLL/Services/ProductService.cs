using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IUploadImage _uploadImage;

        public ProductService(IWorkUnit workUnit, IMapper mapper, IUploadImage uploadImage)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _uploadImage = uploadImage;
        }

        public async Task<ProductDto> AddProduct(ProductDto productDto)
        {
            try
            {
                Product product = new Product
                {
                    NameProduct = productDto.NameProduct,
                    Description = productDto.Description,
                    LargeDescription = productDto.LargeDescription,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                    Status = productDto.Status == 1 ? true : false,
                    Discount = productDto.Discount,
                    BrandId = productDto.BrandId,
                    CategoryId = productDto.CategoryId,
                    SubCategoryId = productDto.SubCategoryId
                };

                product.Images = new List<Image>();
                foreach (var image in productDto.Images)
                {
                    var urlImage = await _uploadImage.UploadImageAsync(image, "products");
                    product.Images.Add(new Image { UrlImage = urlImage });
                }

                await _workUnit.Product.Add(product);
                await _workUnit.Save();

                if (product.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agrear una nueva Marca");
                }

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            try
            {
                var productDb = await _workUnit.Product.GetFirst(e => e.Id == productDto.Id);

                if (productDb == null)
                {
                    throw new TaskCanceledException("El producto no Existe");
                }

                productDb.NameProduct = productDto.NameProduct;
                productDb.Description = productDto.Description;
                productDb.LargeDescription = productDto.LargeDescription;
                productDb.Price = productDto.Price;
                productDb.Stock = productDto.Stock;
                productDb.Discount = productDto.Discount;
                productDb.BrandId = productDto.BrandId;
                productDb.CategoryId = productDto.CategoryId;
                productDb.SubCategoryId = productDto.SubCategoryId;


                _workUnit.Product.Update(productDb);

                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var productDb = await _workUnit.Product.GetFirst(e => e.Id == id);

                if (productDb == null)
                {
                    throw new TaskCanceledException("El producto no Existe");
                }

                productDb.Status = !productDb.Status;

                _workUnit.Product.Update(productDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductListDto>> GetAllProducts()
        {
            try
            {
                var lista = await _workUnit.Product.GetAll(
                                    incluirPropiedades: "Brand,Category,Subcategory,Images,Reviews", 
                                    orderBy: e => e.OrderBy(e => e.NameProduct));

                return _mapper.Map<IEnumerable<ProductListDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductListDto>> GetProductsAssests()
        {
            try
            {
                var lista = await _workUnit.Product.GetAll(
                                    incluirPropiedades: "Brand,Category,Subcategory,Images,Reviews",
                                    filtro: e => e.Status == true,
                                    orderBy: e => e.OrderBy(e => e.NameProduct));

                return _mapper.Map<IEnumerable<ProductListDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductListDto>> GetProductsPopular()
        {
            try
            {
                var lista = await _workUnit.Product.GetAll(
                                    incluirPropiedades: "Brand,Category,Subcategory,Images,Reviews",
                                    filtro: e => e.Status == true,
                                            orderBy: q => q.OrderByDescending(p => p.Reviews.Average(r => r.Rating))
                                      .ThenByDescending(p => p.Reviews.Count)
                                      .ThenBy(p => p.NameProduct));

                return _mapper.Map<IEnumerable<ProductListDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductFilterDTO> FilterProducts(
            string? searchString, 
            List<int>? brandIds = null, 
            List<int>? categoryIds = null, 
            List<int>? subCategoryIds = null,
            int? orderType = 0  ,  
            int page = 1, 
            int pageSize = 12
            )
        {
            try
            {
                var list = await _workUnit.Product.GetAll(
                    incluirPropiedades: "Brand,Category,Subcategory,Images",
                    filtro: e => e.Status == true,
                    orderBy: e => e.OrderBy(e => e.NameProduct
                ));

                // Aplicar filtro de búsqueda
                if (!string.IsNullOrEmpty(searchString))
                {
                    list = list.Where(p => p.NameProduct.ToLower().Contains(searchString.ToLower()));
                }

                if (brandIds != null && brandIds.Any())
                {
                    list = list.Where(e => brandIds.Contains(e.BrandId));
                }

                if (categoryIds != null && categoryIds.Any())
                {
                    list = list.Where(e => categoryIds.Contains(e.CategoryId));
                }

                if (subCategoryIds != null && subCategoryIds.Any())
                {
                    list = list.Where(e => subCategoryIds.Contains(e.SubCategoryId));
                }

                switch (orderType)
                {
                    case 1: 
                        list = list.OrderBy(p => p.Price);
                        break;

                    case 2: 
                        list = list.OrderByDescending(p => p.Price);
                        break;

                    default: 
                        list = list.OrderBy(p => p.NameProduct);
                        break;
                }

                // Calcular el número total de productos y páginas
                var totalProducts = list.Count();
                var totalPaginas = (int)Math.Ceiling((decimal)totalProducts / pageSize);

                // Aplicar paginación
                var productos = list
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var productoFilter = new ProductFilterDTO()
                {
                    Total = totalProducts,
                    Products = _mapper.Map<IEnumerable<Product>>(productos)
                };
                
                return productoFilter;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDetailsDto> GetProduct(int id)
        {
            try
            {
                var productDb = await _workUnit.Product.GetFirst(e => e.Id == id,
                                                        incluirPropiedades: "Brand,Category,Subcategory,Images,Reviews,Comments.User,Comments.CommentsChild");

                if (productDb == null)
                {
                    throw new TaskCanceledException("El producto no Existe");
                }

                var product = _mapper.Map<ProductDetailsDto>(productDb);

                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
