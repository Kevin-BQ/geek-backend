using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;
using System.Net;

namespace API.Controllers
{
    public class ProductController: BaseApiController
    {
        private readonly IProductService _productService;
        private ApiResponse _response;

        public ProductController(IProductService productService)
        {
            _productService = productService;
            _response = new();
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _productService.GetAllProducts();
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                _response.Result = await _productService.GetProduct(id);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [Authorize(Policy = "AdminRol")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            try
            {
                await _productService.AddProduct(productDto);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [Authorize(Policy = "AdminRol")]
        [HttpPut]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            try
            {
                await _productService.UpdateProduct(productDto);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [Authorize(Policy = "AdminRol")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpGet("GetAssets")]
        public async Task<IActionResult> GetAssests()
        {
            try
            {
                _response.Result = await _productService.GetProductsAssests();
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetFilterProduct(
                    string? searchString,
                    [FromQuery] List<int>? brandsIds,
                    [FromQuery] List<int>? categoryIds,
                    [FromQuery] List<int>? subCategoryIds,
                    int? orderType,
                    int page = 1
                    )
        {
            try
            {
                var filter = await _productService.FilterProducts(searchString, brandsIds, categoryIds, subCategoryIds,
                    orderType, page);
                _response.Result = filter.Products;
                _response.Total = filter.Total;
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
    }
}
