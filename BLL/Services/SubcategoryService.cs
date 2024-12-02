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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public SubcategoryService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<SubcategoryDto> AddSubcategory(SubcategoryDto subcategoryDto)
        {
            try
            {
                Subcategory subcategory = new Subcategory
                {
                    NameSubcategory = subcategoryDto.NameSubcategory,
                    CategoryId = subcategoryDto.CategoryId,
                    Estatus = subcategoryDto.Estatus == 1 ? true : false
                };

                await _workUnit.Subcategory.Add(subcategory);
                await _workUnit.Save();

                if (subcategory.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agrear una nueva Subcategoria");
                }

                return _mapper.Map<SubcategoryDto>(subcategory);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateSubcategory(SubcategoryDto subcategoryDto)
        {
            try
            {
                var SubcategoryDb = await _workUnit.Subcategory.GetFirst(e => e.Id == subcategoryDto.Id);

                if (SubcategoryDb == null)
                {
                    throw new TaskCanceledException("La Subcategoria no Existe");
                }

                SubcategoryDb.NameSubcategory = subcategoryDto.NameSubcategory;
                SubcategoryDb.Estatus = subcategoryDto.Estatus == 1 ? true : false;

                _workUnit.Subcategory.Update(SubcategoryDb);

                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteSubcategory(int id)
        {
            try
            {
                var subcategoryDb = await _workUnit.Subcategory.GetFirst(e => e.Id == id);

                if (subcategoryDb == null)
                {
                    throw new TaskCanceledException("La Subcategoria no Existe");
                }

                subcategoryDb.Estatus = false;

                _workUnit.Subcategory.Update(subcategoryDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<SubcategoryDto>> GetAllSubcategories()
        {
            try
            {
                var lista = await _workUnit.Subcategory.GetAll(
                                    incluirPropiedades: "Category",
                                    orderBy: e => e.OrderBy(e => e.NameSubcategory));

                return _mapper.Map<IEnumerable<SubcategoryDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategoriesAssests()
        {
            try
            {
                var lista = await _workUnit.Subcategory.GetAll(
                                    incluirPropiedades:"Category",
                                    filtro: e => e.Estatus == true,
                                    orderBy: e => e.OrderBy(e => e.NameSubcategory));

                return _mapper.Map<IEnumerable<Subcategory>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
