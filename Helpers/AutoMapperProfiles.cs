using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PostVehicleViewModel, Vehicle>();
            CreateMap<Vehicle, VehicleViewModel>()
              .ForMember(dest => dest.VehicleId, options => options.
              MapFrom(src => src.Id));
        }
    }
}