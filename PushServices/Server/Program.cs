using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EFChangeNotify;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Server.Hubs;
using Server.Model;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:8080";

            #region Database initialiser

            using (var context = new ArtistContext())
            {
                Database.SetInitializer<ArtistContext>(new ArtistIntializer());
                context.Artists.ToList();
            }
            
            #endregion

            #region Notifier

            using (var notifer = new EntityChangeNotifier<Artist, ArtistContext>(artist => artist.Trigger == "Y"))
            {
                notifer.Changed += OnNotifierChanged;

                using (WebApplication.Start<Startup>(url))
                {
                    Console.WriteLine("Server running on {0}", url);
                    Console.ReadLine();
                }
            }

            #endregion
        }
         
        private static void OnNotifierChanged(object sender, EntityChangeEventArgs<Artist> e)
        {
            using (var db = new ArtistContext())
            {
                foreach (var p in e.Results)
                {
                    Console.WriteLine("Triggered fired => {0}:{1}", p.ArtistId, p.Name);
                    var artist = db.Artists.Single(a => a.ArtistId == p.ArtistId);
                    artist.Trigger = "N";
                }

                db.SaveChanges();
            }

            Console.WriteLine("Pushing notification to clients...");
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();

            IEnumerable<Artist> changedArtists = e.Results;

            // Send a list of changed artists
            hubContext.Clients.All.refresh(changedArtists);
        }
    }
}
