using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class GenresCreateDTO
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
    }
}
