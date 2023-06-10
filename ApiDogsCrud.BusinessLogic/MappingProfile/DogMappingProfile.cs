using ApiDogsCrud.BusinessLogic.Models;
using ApiDogsCrud.DataLayer.Entity;
using AutoMapper;

namespace ApiDogsCrud.BusinessLogic.MappingProfile
{
    public class DogMappingProfile : Profile
    {
        public DogMappingProfile()
        {
            CreateMap<Dog, DogDto>();
            CreateMap<DogDto, Dog>();
        }
    }
}