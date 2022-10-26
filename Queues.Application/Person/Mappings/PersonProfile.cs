using AutoMapper;
using Queues.Application.Person.DTOs;

namespace Queues.Application.Person.Mappings;
public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonDetailDto, Domain.Entities.Person>().ReverseMap();
    }
}
