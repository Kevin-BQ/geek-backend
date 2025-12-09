using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class SubcategoryController : BaseApiController
    {
        private readonly ISubcategoryService _subcategoryService;
        private ApiResponse _response;

        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
            _response = new();
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _subcategoryService.GetAllSubcategories();
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
        public async Task<IActionResult> Create(SubcategoryDto subcategoryDto)
        {
            try
            {
                await _subcategoryService.AddSubcategory(subcategoryDto);
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
        public async Task<IActionResult> Edit(SubcategoryDto subcategoryDto)
        {
            try
            {
                await _subcategoryService.UpdateSubcategory(subcategoryDto);
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
        [HttpPut("{subcategoryId:int}")]
        public async Task<IActionResult> UpdateStatus(int subcategoryId)
        {
            try
            {
                await _subcategoryService.UpdateStatus(subcategoryId);
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

        [HttpGet("active-subcategories")]
        public async Task<IActionResult> GetAssests()
        {
            try
            {
                _response.Result = await _subcategoryService.GetSubcategoriesAssests();
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
