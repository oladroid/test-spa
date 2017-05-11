using System.Data.Entity;
using AngularJSAuth.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuth.API {
	public class AuthContext : IdentityDbContext<IdentityUser> {
		public AuthContext() : base("AuthContext") { }

		public DbSet<Client> Clients { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }
	}
}