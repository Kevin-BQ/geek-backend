using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    public interface IWorkUnit: IDisposable
    {
        // Interfaces de los modelos

        Task Guardar();
    }
}
