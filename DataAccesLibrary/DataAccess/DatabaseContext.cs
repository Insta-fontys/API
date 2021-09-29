using DataAccesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Creator> Creator { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
