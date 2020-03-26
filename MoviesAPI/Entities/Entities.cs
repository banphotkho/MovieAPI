using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class Entities
    {
        public class Genre
        {
            public int Id { get; set; }
            [Required]
            [StringLength(60)]
            public  string Name { get; set; }
        }
    }
}
