using AutoMapper;
using BookApi.Data.Entity;
using BookApi.Data.Model;
using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApi
{
    /// <summary>
    /// AutoMapper configurator
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookDTO>()
                    .ForMember(o => o.Authors, o => o.MapFrom(a => a.Authors.Split(',')));
            });
        }
    }
}
