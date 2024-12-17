using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class BrandController: BaseApiController
    {
        private readonly IBrandService _brandService;
        private ApiResponse _response;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _response = new();
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _brandService.GetAllBrands();
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
        public async Task<IActionResult> Create(BrandDto brandDto)
        {
            try
            {
                await _brandService.AddBrand(brandDto);
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
        public async Task<IActionResult> Edit(BrandDto brandDto)
        {
            try
            {
                await _brandService.UpdateBrand(brandDto);
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
        [HttpPut("{brandId:int}")]
        public async Task<IActionResult> UpdateStatus(int brandId)
        {
            try
            {
                await _brandService.UpdateStatus(brandId);
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
        
        [HttpGet("active-brands")]
        public async Task<IActionResult> GetAssests()
        {
            try
            {
                _response.Result = await _brandService.GetBrandsAssests();
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
