using AutoMapper;

using DTOs;
using Models;

namespace MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
