using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoviesAPI.Entities.Entities;

namespace MoviesAPI.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTO>(); //mapping for HttpGet
            CreateMap<GenresCreateDTO, Genre>(); // mapping for HttpPost
            CreateMap<Person, PersonDTO>().ReverseMap(); // mapping for HttpGet
            CreateMap<PersonCrateDTO, Person>().ForMember(x=>x.Picture, options => options.Ignore()); // mapping for HttpPost
        }
    }
}
