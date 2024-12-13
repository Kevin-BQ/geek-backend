using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var lista = await _workUnit.Product.GetAll(
                                    incluirPropiedades: "Brand,Category,Subcategory,Images", 
                                    orderBy: e => e.OrderBy(e => e.NameProduct));

                return _mapper.Map<IEnumerable<Product>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAssests()
        {
            try
            {
                var lista = await _workUnit.Product.GetAll(
                                    incluirPropiedades: "Brand,Category,Subcategory,Images",
                                    filtro: e => e.Status == true,
                                    orderBy: e => e.OrderBy(e => e.NameProduct));

                return _mapper.Map<IEnumerable<Product>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Product>> FilterProducts(int? brandId, int? categoryId, int? subCategoryId)
        {
            try
            {
                var list = await _workUnit.Product.GetAll(
                    incluirPropiedades: "Brand,Category,Subcategory,Images",
                    filtro: e => e.Status == true,  
                    orderBy: e => e.OrderBy(e => e.NameProduct)
                );

                if (brandId.HasValue && brandId.Value != 0)
                {
                    list = list.Where(e => e.BrandId == brandId.Value);
                }

                if (categoryId.HasValue && categoryId.Value != 0)
                {
                    list = list.Where(e => e.CategoryId == categoryId.Value);
                }

                if (subCategoryId.HasValue && subCategoryId.Value != 0)
                {
                    list = list.Where(e => e.SubCategoryId == subCategoryId.Value);
                }

                return _mapper.Map<IEnumerable<Product>>(list);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                var productDb = await _workUnit.Product.GetFirst(e => e.Id == id,
                                                        incluirPropiedades: "Brand,Category,Subcategory,Images");

                if (productDb == null)
                {
                    throw new TaskCanceledException("El producto no Existe");
                }

                var product = _mapper.Map<Product>(productDb);

                
                return product;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
