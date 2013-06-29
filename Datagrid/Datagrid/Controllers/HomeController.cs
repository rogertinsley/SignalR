using System.Linq;
using System.Web.Mvc;
using Datagrid.Models;

namespace Datagrid.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerContext db = new CustomerContext();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}