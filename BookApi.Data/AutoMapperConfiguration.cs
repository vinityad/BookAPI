using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApi.Data
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entity.Book, Model.BookDTO>()
                    .ForMember(o => o.Authors, o => o.MapFrom(a => a.Authors.Split(',')));
                cfg.CreateMap<Model.BookDTO, Entity.Book>()
                    .ForMember(o => o.Authors, o => o.MapFrom(a => string.Join(",", a.Authors)));
            });
        }
    }
}
