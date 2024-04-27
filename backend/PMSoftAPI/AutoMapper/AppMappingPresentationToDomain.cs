using AutoMapper;
using DataAccess.Models;
using PMSoftAPI.Models;

namespace PMSoftAPI.AutoMapper;

public class AppMappingPresentationToDomain : Profile
{
    public AppMappingPresentationToDomain()
    {
        //Authors
        CreateMap<AuthorCreate, Domain.Models.Author>().ReverseMap();
        CreateMap<AuthorGet, Domain.Models.Author>().ReverseMap();
        CreateMap<AuthorUpdate, Domain.Models.Author>().ReverseMap();        
        
        //Books
        CreateMap<BookCreate, Domain.Models.Book>().ReverseMap();
        CreateMap<BookGet, Domain.Models.Book>().ReverseMap();
        CreateMap<BookUpdate, Domain.Models.Book>().ReverseMap();
        
        //Countries
        CreateMap<CountryCreate, Domain.Models.Country>().ReverseMap();
        CreateMap<CountryGet, Domain.Models.Country>().ReverseMap();
        CreateMap<CountryUpdate, Domain.Models.Country>().ReverseMap();
        
        //Genres
        CreateMap<GenreCreate, Domain.Models.Genre>().ReverseMap();
        CreateMap<GenreGet, Domain.Models.Genre>().ReverseMap();
        CreateMap<GenreUpdate, Domain.Models.Genre>().ReverseMap();
        
        //Publishers
        CreateMap<PublisherCreate, Domain.Models.Publisher>().ReverseMap();
        CreateMap<PublisherGet, Domain.Models.Publisher>().ReverseMap();
        CreateMap<PublisherUpdate, Domain.Models.Publisher>().ReverseMap();
        
        //Users
        CreateMap<UserCreate, Domain.Models.User>().ReverseMap();
        CreateMap<UserGet, Domain.Models.User>().ReverseMap();
    }
}