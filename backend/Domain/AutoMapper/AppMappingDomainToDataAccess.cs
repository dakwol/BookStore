using AutoMapper;

namespace Domain.AutoMapper;

public class AppMappingDomainToDataAccess : Profile
{
    public AppMappingDomainToDataAccess()
    {
        CreateMap<Models.Author, DataAccess.Models.Author>().ReverseMap();
        CreateMap<Models.Book, DataAccess.Models.Book>().ReverseMap();
        CreateMap<Models.Country, DataAccess.Models.Country>().ReverseMap();
        CreateMap<Models.Genre, DataAccess.Models.Genre>().ReverseMap();
        CreateMap<Models.Publisher, DataAccess.Models.Publisher>().ReverseMap();
        CreateMap<Models.User, DataAccess.Models.User>().ReverseMap();
    }
}