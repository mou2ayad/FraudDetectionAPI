using System;
using AutoMapper;
using FRISS.Common.Models;
using FRISS.DataAccessLayer.Context;

namespace FRISS.DataAccessLayer.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDAO>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<PersonDAO, Person>();
        }
          
    }
}
