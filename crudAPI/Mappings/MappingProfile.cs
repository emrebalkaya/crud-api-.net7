using System;
using AutoMapper;
using crudAPI.Dto;
using crudAPI.Entity;

namespace crudAPI.Mappings
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}