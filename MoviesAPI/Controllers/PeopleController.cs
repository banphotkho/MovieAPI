using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController] //Reference ApiController
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "person";

        public PeopleController(ApplicationDbContext context,IMapper mapper, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]

        public async Task<ActionResult<List<PersonDTO>>> Get()
        {
            var pepole = await context.People.ToListAsync();
            return mapper.Map<List<PersonDTO>>(pepole);
        } 

       [HttpGet("{Id}",Name ="getPerson")]
       public async Task<ActionResult<PersonDTO>> Get(int Id)
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == Id);

            if (person == null)
            {
                return NotFound();
            }

            return mapper.Map<PersonDTO>(person);

        }

        [HttpPost]
        
        public async Task<ActionResult> Post([FromForm] PersonCrateDTO personCrate)
        {
            //maper Person from PersonCreateDTO 
            var person = mapper.Map<Person>(personCrate);

            if(personCrate.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await personCrate.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extention = Path.GetExtension(personCrate.Picture.FileName);
                    person.Picture = await fileStorageService.SaveFile(content, extention, containerName, personCrate.Picture.ContentType);
                }

            }

            context.Add(person);
            await context.SaveChangesAsync();

            var PersonDTO = mapper.Map<PersonDTO>(person);
            return new CreatedAtRouteResult("getPerson", new { Id = person.Id },PersonDTO);


        }


    }
}
