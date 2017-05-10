using System.Web.Http;
using AngularJSAuth.API.Models;

namespace AngularJSAuth.API.Controllers {
	[RoutePrefix("api/Orders")]
	public class OrdersController : ApiController {

		[Authorize]
		[Route("")]
		public IHttpActionResult Get() {
			return Ok(Order.CreateOrders());
		}
	}
}