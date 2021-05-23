using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeolocalizacionIPs.Exceptions;
using GeolocalizacionIPs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeolocalizacionIPs.Controllers
{
    [Route("api/ipinfo")]
    [ApiController]
    public class IPInfoController : ControllerBase
    {
        private readonly IIPInfoService _ipInfoService;

        public IPInfoController(IIPInfoService ipInfoService)
        {
            _ipInfoService = ipInfoService;
        }

        [HttpGet]
        [Route("{ip}")]
        public async Task<IActionResult> GetIPInfoAsync(string IP)
        {
            try
            {
                var ipInfo = await _ipInfoService.GetIPInfo(IP);

                if (ipInfo != null)
                {
                    return Ok(ipInfo);
                }
                else
                {
                    return NotFound("Informacion relacionada con la IP ingresada no fue encontrada.");
                }
            }
            catch (RestCountriesJsonConvertException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("distanciaMasLejanaABuenosAires")]
        public async Task<IActionResult> GetDistanciaMasLejanaABuenosAires()
        {
            var ipDistancia = await _ipInfoService.GetDistanciaABuenosAires(false);

            if (ipDistancia != null)
            {
                return Ok(ipDistancia);
            }
            else
            {
                return NotFound("La distancia mas lejana a Buenos Aires no fue encontrada.");
            }
        }

        [HttpGet]
        [Route("distanciaMasCercanaABuenosAires")]
        public async Task<IActionResult> GetDistanciaMasCercanaABuenosAires()
        {
            var ipDistancia = await _ipInfoService.GetDistanciaABuenosAires(true);

            if (ipDistancia != null)
            {
                return Ok(ipDistancia);
            }
            else
            {
                return NotFound("La distancia mas lejana a Buenos Aires no fue encontrada.");
            }
        }

        [HttpGet]
        [Route("distanciaPromedio")]
        public async Task<IActionResult> GetDistanciaPromedio()
        {
            var ipDistancia = await _ipInfoService.GetDistanciaPromedio();

            if (ipDistancia != null)
            {
                return Ok(ipDistancia);
            }
            else
            {
                return NotFound("No se pudo encontrar la distancia promedio a Buenos Aires.");
            }
        }
    }
}
