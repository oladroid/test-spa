using System.Threading.Tasks;
using System.Web.Http;

namespace AngularJSAuth.API.Controllers {
	[RoutePrefix("api/RefreshTokens")]
	public class RefreshTokensController : ApiController {

		private AuthRepository _authRepository = null;

		public RefreshTokensController() {
			_authRepository = new AuthRepository();
		}

		[Authorize(Users = "Admin")]
		[Route("")]
		public IHttpActionResult Get() {
			return Ok(_authRepository.GetAllRefreshTokens());
		}

		//[Authorize(Users = "Admin")]
		[AllowAnonymous]
		[Route("")]
		public async Task<IHttpActionResult> Delete(string tokenId) {
			var result = await _authRepository.RemoveRefreshToken(tokenId);
			if (result) {
				return Ok();
			}
			return BadRequest("Token Id does not exist");

		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				_authRepository.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}