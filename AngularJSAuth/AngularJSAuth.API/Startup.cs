using System;
using System.Web.Http;
using AngularJSAuth.API.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(AngularJSAuth.API.Startup))]
namespace AngularJSAuth.API {
	public class Startup {

		public void Configuration(IAppBuilder app) {
			ConfigureOAuth(app);
			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);
		}

		public void ConfigureOAuth(IAppBuilder app) {
			OAuthAuthorizationServerOptions oauthServerOptions = new OAuthAuthorizationServerOptions() {
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
				Provider = new SimpleAuthorizationServerProvider()
			};

			app.UseOAuthAuthorizationServer(oauthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}