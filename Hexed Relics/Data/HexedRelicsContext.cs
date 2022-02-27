using Hexed_Relics.Models;
using Microsoft.EntityFrameworkCore;

namespace Hexed_Relics.Data
{
    public class HexedRelicsContext :DbContext
    {
        public HexedRelicsContext (DbContextOptions<HexedRelicsContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Transactions> Transactions { get; set; }
    }
}
