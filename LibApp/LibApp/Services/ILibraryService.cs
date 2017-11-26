using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApp.Services
{
    public interface ILibraryService
    {
        string GetById(int id);
    }
}
