﻿using System;
using AutoMapper;

using Use_Wheels.Models.DTO;

namespace Use_Wheels
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			// Creates a mapping from the source to destination and vice versa
			CreateMap<RegisterationRequestDTO, UserDTO>().ReverseMap();
            CreateMap<RegisterationRequestDTO, User>().ReverseMap();
			CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CarDTO, Car>().ReverseMap();
			CreateMap<CategoryDTO, Category>().ReverseMap();
			CreateMap<CarDTO, Car>().ReverseMap();
            CreateMap<CarUpdateDTO, Car>().ReverseMap();
			CreateMap<OrderDTO, Orders>().ReverseMap();
			CreateMap<CarResponseDTO, Car>().ReverseMap();
        }
	}
}

