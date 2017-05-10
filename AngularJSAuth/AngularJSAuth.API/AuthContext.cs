using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuth.API {
	public class AuthContext : IdentityDbContext<IdentityUser> {
		public AuthContext() : base("AuthContext") { }
	}
}