using AutoMapper;
using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Mapping
{
    public class IPInfoMapping : Profile
    {
        public IPInfoMapping()
        {
            CreateMap<IPInfo, IPInfoDTO>().ForMember(dest => dest.FranjasHorarias, src => src.MapFrom(s => s.FranjasHorarias.Replace(" ", "").Split(new char[] { ',' }).ToList()))
                .ForMember(dest => dest.Idiomas, src => src.MapFrom(s => s.Idiomas.Replace(" ", "").Split(new char[] { ',' }).ToList()));

            CreateMap<RestCountriesResponseDTO, IPInfo>().ForMember(dest => dest.Pais, src => src.MapFrom(s => s.Translations.Es))
                .ForMember(dest => dest.CodigoISOPais, src => src.MapFrom(s => s.Alpha2Code))
                .ForMember(dest => dest.FranjasHorarias, src => src.MapFrom(s => string.Join(", ", s.Timezones)))
                .ForMember(dest => dest.Idiomas, src => src.MapFrom(s => string.Join(", ", s.Languages.Select(lang => lang.NativeName + " (" + lang.Iso639_1 + ")"))))
                .ForMember(dest => dest.Moneda, src => src.MapFrom(s => s.Currencies.Select(curr => curr.Code).FirstOrDefault()));

            CreateMap<IPInfo, IPDistanciaDTO>().ForMember(dest => dest.Distancia, src => src.MapFrom(s => s.DistanciaABuenosAires));
        }
    }
}
