using System.Data.Entity;

namespace Server.Model
{
    public class ArtistContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
    }
}