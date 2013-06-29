using System.Collections.Generic;
using System.Data.Entity;
using Datagrid.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Datagrid.Models;

namespace Datagrid
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer(new CustomerInitializer());
        }
    }

    public class CustomerInitializer : DropCreateDatabaseAlways<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Roger", Surname = "Tinsley", HouseNumber = 66, Street = "Main Road", Town = "Lisburn", PostCode = "BT28 3QW"},
                new Customer { FirstName = "Sara", Surname = "Tinsley", HouseNumber = 66, Street = "Main Road", Town = "Lisburn", PostCode = "BT28 3QW"},
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();
        }
    }
}