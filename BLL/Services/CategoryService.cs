using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public CategoryService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                Category category = new Category
                {
                    NameCategory = categoryDto.NameCategory,
                    Estatus = categoryDto.Estatus == 1 ? true : false
                };

                await _workUnit.Category.Add(category);
                await _workUnit.Save();

                if (category.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agregar una nueva Categoria");
                }

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var categoryDb = await _workUnit.Category.GetFirst(e => e.Id == categoryDto.Id);

                if (categoryDb == null)
                {
                    throw new TaskCanceledException("La Categoria no existe");
                }

                categoryDb.NameCategory = categoryDto.NameCategory;
                categoryDb.Estatus = categoryDto.Estatus == 1 ? true : false;

                _workUnit.Category.Update(categoryDb);

                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                var categoryDb = await _workUnit.Category.GetFirst(e => e.Id == id);

                if (categoryDb == null)
                {
                    throw new TaskCanceledException("La Categoria no Existe");
                }

                _workUnit.Category.Remove(categoryDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            try
            {
                var lista = await _workUnit.Category.GetAll(
                                    orderBy: e => e.OrderBy(e => e.NameCategory));

                return _mapper.Map<IEnumerable<CategoryDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
