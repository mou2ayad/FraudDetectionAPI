using System;
using AutoMapper;
using Fraud.Component.Common.Models;
using Fraud.Component.DataAccessLayer.Context;

namespace Fraud.Component.DataAccessLayer.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDao>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<PersonDao, Person>();
        }
          
    }
}
