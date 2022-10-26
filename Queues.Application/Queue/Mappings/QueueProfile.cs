using AutoMapper;
using Queues.Application.Queue.DTOs;

namespace Queues.Application.Queue.Mappings;
public class QueueProfile : Profile
{
    public QueueProfile()
    {
        CreateMap<QueueAddDto, Domain.Entities.Queue>().ReverseMap();
        CreateMap<QueueUpdateDto, Domain.Entities.Queue>().ReverseMap();
        CreateMap<QueueDetailDto, Domain.Entities.Queue>().ReverseMap();
    }
}
