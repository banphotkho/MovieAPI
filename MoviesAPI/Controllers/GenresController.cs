
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoviesAPI.Entities.Entities;

namespace MoviesAPI.Controllers 
{

    [Route("api/genres")]
    public class GenresController: ControllerBase
    {   
        
        private readonly ILogger<GenresController> logging;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresController(ILogger<GenresController> logging , ApplicationDbContext context,IMapper mapper )
        {
            this.logging = logging;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet] // api/genres
        [HttpGet("list")] //api/genres/list
        [HttpGet("/genres")] // /genres
        public  async Task<ActionResult<List<GenreDTO>>> Get()
        {
            
            var genres = await context.genres.AsNoTracking().ToListAsync();
            var genresDTO = mapper.Map<List<GenreDTO>>(genres);
            return genresDTO;
        }
        //[HttpGet("{}/{}/{}")] example mutil parametor
        //[HttpGet("{Id:int}/{param1:string}/{}")] example specification type of variable.
        [HttpGet("{Id}",Name ="getGenre")]
        public async Task<ActionResult<Genre>> Get(int Id)
        {
            var genre = await context.genres.FirstOrDefaultAsync(x => x.Id == Id);

            if (genre == null)
            {
                return NotFound();
            }

            var genresDTO = mapper.Map<GenreDTO>(genre);
            return Ok(genresDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenresCreateDTO genresCreate)
        {
            var genre = mapper.Map<Genre>(genresCreate);
            context.Add(genre);
            await context.SaveChangesAsync();
            // mapping back to GenreDTO
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return new CreatedAtRouteResult("getGenre", new { Id = genreDTO.Id }, genreDTO);
            
        }
        [HttpPut ("{Id}")]
        public async Task<ActionResult>  Put(int Id ,[FromBody] GenresCreateDTO  genresCreate)
        {
            var genre = mapper.Map<Genre>(genresCreate);
            genre.Id = Id;
            context.Entry(genre).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var exesis = await context.genres.AnyAsync(x => x.Id == Id);
            if (!exesis)
            {
                return NotFound();
            }
            context.Remove(new Genre() { Id = Id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
