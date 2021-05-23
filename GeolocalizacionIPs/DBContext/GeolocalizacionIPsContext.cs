using GeolocalizacionIPs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.DBContext
{
    public class GeolocalizacionIPsContext : DbContext
    {
        public GeolocalizacionIPsContext(DbContextOptions<GeolocalizacionIPsContext> options) : base(options) { }
        public DbSet<IPInfo> IPInfos { get; set; }
    }
}
