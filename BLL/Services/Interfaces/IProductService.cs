﻿using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<ProductDto> AddProduct(ProductDto productDto);
        Task UpdateProduct(ProductDto productDto);
        Task DeleteProduct(int id);
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetProductsAssests();
        Task<ProductFilterDTO> FilterProducts(string? searchString,
            List<int>? brandIds, List<int>? categoryIds,
            List<int>? subCategoryIds, int? orderType, int page = 1, int pageSize = 12);
    }
}
