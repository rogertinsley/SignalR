using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class ArtistsController : ApiController
    {
        // Get
        public IEnumerable<Artist> Get()
        {
            using (var db = new ArtistContext())
            {
                return db.Artists.ToList();
            }
        }
    }
}
