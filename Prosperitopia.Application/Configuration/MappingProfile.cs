using AutoMapper;
using Prosperitopia.Domain.Model.Dto;
using Prosperitopia.Domain.Model.Entity;
namespace Prosperitopia.Application.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<CreUpdateItem, Item>().ReverseMap();

            //TODO: Add mapping config when applicable.
        }
    }
}
