using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datagrid.Models;

namespace Datagrid.Api
{
    public class CustomerApiController : ApiController
    {
        // Put api/<controller>
        public HttpResponseMessage Put([FromBody]Customer customer)
        {
            if (customer == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);
            if (!ModelState.IsValid) return new HttpResponseMessage(HttpStatusCode.BadRequest);

            using (var db = new CustomerContext())
            {
                db.Customers.Attach(customer);
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}