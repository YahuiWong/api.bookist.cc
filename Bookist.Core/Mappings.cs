using AutoMapper;
using Bookist.Core.Dtos;
using Bookist.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bookist.Core
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Book, BookResponseDto>().AfterMap((src, des) =>
            {
                des.Tags = Mapper.Map<List<TagResponseDto>>(src.BookTags.Select(x => x.Tag));
            });
            CreateMap<BookRequestDto, Book>();

            CreateMap<Link, LinkResponseDto>();
            CreateMap<LinkRequestDto, Link>();

            CreateMap<Tag, TagResponseDto>();

            CreateMap<User, UserResponseDto>();
            CreateMap<UserRequestDto, User>();
        }
    }
}
