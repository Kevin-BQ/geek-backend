using Data.Interfaces.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class WorkUnit : IWorkUnit
    {
        private readonly ApplicationDbContext _context;

        // Interfaces de Repositorios 


        public WorkUnit(ApplicationDbContext context)
        {
            _context = context;
            // 
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Guardar()
        {
            await _context.SaveChangesAsync();
        }
    }
}
