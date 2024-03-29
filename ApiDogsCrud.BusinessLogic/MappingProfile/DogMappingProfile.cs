﻿using ApiDogsCrud.DataLayer.Entity;
using ApiDogsCrud.Models;
using AutoMapper;

namespace ApiDogsCrud.BusinessLogic.MappingProfile
{
    public class DogMappingProfile : Profile
    {
        public DogMappingProfile()
        {
            CreateMap<Dog, DogDto>()
                .ReverseMap();
        }
    }
}