﻿using AutoMapper;
using UniversityProgram.Api.Entities;
using UniversityProgram.Api.Models.Laptop;

namespace UniversityProgram.Api.Map
{
    public class LaptopProfile : Profile
    {
        public LaptopProfile()
        {
            CreateMap<LaptopAddModel, Laptop>();
            CreateMap<Laptop, LaptopModel>().ReverseMap();
            CreateMap<Laptop, LaptopWithCpuNameModel>()
                .ForMember(e=>e.LaptopName,o=>o.MapFrom(l=>l.Name))
                    .ForMember(e => e.CpuName, o => o.MapFrom(l => l.Cpu.Name)); 
        }
    }
}
