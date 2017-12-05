using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;
using Audecyzje.Infrastructure.Dtos;
using AutoMapper;

namespace Audecyzje.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration(config =>
        {
            config.CreateMap<Document, DocumentDto>();
            config.CreateMap<Localization, LocalizationDto>();

        }).CreateMapper();
    }
}
