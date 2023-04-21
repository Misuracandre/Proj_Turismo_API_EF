using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proj_Turismo_API_EF.Models;

namespace Proj_Turismo_API_EF.Data
{
    public class Proj_Turismo_API_EFContext : DbContext
    {
        public Proj_Turismo_API_EFContext (DbContextOptions<Proj_Turismo_API_EFContext> options)
            : base(options)
        {
        }

        public DbSet<Proj_Turismo_API_EF.Models.City> City { get; set; } = default!;

        public DbSet<Proj_Turismo_API_EF.Models.Address>? Address { get; set; }

        public DbSet<Proj_Turismo_API_EF.Models.Hotel>? Hotel { get; set; }

        public DbSet<Proj_Turismo_API_EF.Models.Client>? Client { get; set; }

        public DbSet<Proj_Turismo_API_EF.Models.Ticket>? Ticket { get; set; }

        public DbSet<Proj_Turismo_API_EF.Models.Package>? Package { get; set; }
    }
}
