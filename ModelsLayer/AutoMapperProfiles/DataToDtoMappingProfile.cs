using AutoMapper;
using ModelsLayer.Dtos;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer.AutoMapperProfiles
{
    public class DataToDtoMappingProfile : Profile
    {
        public DataToDtoMappingProfile() {

            CreateMap<AppCheckHistoryInsert, AppCheckHistory>();
            CreateMap<AppCheckHistoryUpdate, AppCheckHistory>();
            CreateMap<AppCheckHistory, AppCheckHistoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(r => r.TargetApplication.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(r => r.TargetApplication.SystemUser.Email))
                .ForMember(dest => dest.URL, opt => opt.MapFrom(r => r.TargetApplication.URL));
          

            CreateMap<SystemUser, SystemUserDto>();

            CreateMap<TargetApplicationInsert, TargetApplication>();
            CreateMap<TargetApplicationUpdate, TargetApplication>();

            CreateMap<TargetApplicationDto, TargetApplicationUpdate>();
                ;
            CreateMap<TargetApplication, TargetApplicationDto>()
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(r => r.SystemUser.Email))
                 .ForMember(dest => dest.LastCheckIsUp, opt => opt.MapFrom(r => r.AppCheckHistories.OrderBy(r=>r.ExecuteTime).LastOrDefault().IsUp))
                 .ForMember(dest => dest.LastCheckTime, opt => opt.MapFrom(r => r.AppCheckHistories.OrderBy(r => r.ExecuteTime).LastOrDefault().ExecuteTime))
                 .ForMember(dest => dest.Interval, opt => opt.MapFrom(r => r.Interval / 1000)); 
        }
    }
    }
