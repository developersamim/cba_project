using AutoMapper;
using TradeProject.Core.DTO;
using TradeProject.Domain;

namespace TradeProject.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountEntity, AccountDto>();
    }

}
