using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace MoviesAPI.Services
{
    public interface Irepository
    {
       Task<List<Entities.Entities.Genre>> GetAllGenre();
        Entities.Entities.Genre GetGenreByID(int id);
    }
}
