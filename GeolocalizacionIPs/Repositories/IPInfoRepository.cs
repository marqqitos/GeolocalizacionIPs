using GeolocalizacionIPs.DBContext;
using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.Entities;
using GeolocalizacionIPs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Repositories
{
    public class IPInfoRepository : IIPInfoRepository
    {
        private readonly GeolocalizacionIPsContext _context;

        public IPInfoRepository(GeolocalizacionIPsContext context)
        {
            _context = context;
        }

        public async Task<IPInfo> GetByCodigoPais(string codigoPais)
        {
            var ipInfo = await _context.IPInfos.Where(ipInfo => ipInfo.CodigoISOPais.Equals(codigoPais)).FirstOrDefaultAsync();

            return ipInfo;
        }

        public async Task<IPInfo> Insert(IPInfo ipInfo)
        {
            ipInfo.Invocaciones = 1;
            _context.IPInfos.Add(ipInfo);
            await _context.SaveChangesAsync();
            return ipInfo;
        }

        public async Task<IPInfo> GetDistanciaABuenosAires(bool masCercana)
        {
            if(masCercana)
            {
                return await _context.IPInfos.OrderBy(d => d.DistanciaABuenosAires).FirstAsync();
            }
            else
            {
                return await _context.IPInfos.OrderByDescending(d => d.DistanciaABuenosAires).FirstAsync();
            }
        }

        public async Task UpdateInvocaciones(IPInfo ipInfo)
        {
            ipInfo.Invocaciones++;
            _context.IPInfos.Update(ipInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<IPInfo>> GetDistanciaPromedio()
        {
            return await _context.IPInfos.ToListAsync();
        }
    }
}
