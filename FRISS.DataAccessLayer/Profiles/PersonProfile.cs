using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FRISS.Common.Models;

namespace FRISS.DataAccessLayer.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDAO>().ReverseMap();
        }

    }
}
