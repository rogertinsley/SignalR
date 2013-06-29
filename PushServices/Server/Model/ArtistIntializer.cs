using System.Collections.Generic;
using System.Data.Entity;

namespace Server.Model
{
    public class ArtistIntializer : DropCreateDatabaseIfModelChanges<ArtistContext>
    {
        protected override void Seed(ArtistContext context)
        {
            var customers = new List<Artist>
                {
                    new Artist { Name = "The Beatles", Trigger = "N" },
                    new Artist { Name = "The Who", Trigger = "N"  },
                    new Artist { Name = "Oasis", Trigger = "N"  },
                    new Artist { Name = "Blur", Trigger = "N"  },
                };

            customers.ForEach(a => context.Artists.Add(a));
            context.SaveChanges();
        }
    }
}