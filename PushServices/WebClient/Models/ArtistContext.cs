using System.Data.Entity;

namespace WebClient.Models
{
    public class ArtistContext : DbContext
    {
        public ArtistContext()
            : base("ArtistsConnectionstring")
        {

        }

        public DbSet<Artist> Artists { get; set; }
    }
}