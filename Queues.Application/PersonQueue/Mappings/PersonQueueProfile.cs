using AutoMapper;
using Queues.Application.PersonQueue.DTOs;

namespace Queues.Application.PersonQueue.Mappings;
public class PersonQueueProfile : Profile
{
    public PersonQueueProfile()
    {
        CreateMap<PersonQueueAddDto, Domain.Entities.PersonQueue>().ReverseMap();
        CreateMap<PersonQueueDetailDto, Domain.Entities.PersonQueue>().ReverseMap();
    }
}
