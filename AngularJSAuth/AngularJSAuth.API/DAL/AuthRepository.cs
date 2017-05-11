using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularJSAuth.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuth.API {
	public class AuthRepository : IDisposable {

		private AuthContext _authContext;
		private UserManager<IdentityUser> _userManager;

		public AuthRepository() {
			_authContext = new AuthContext();
			_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_authContext));
		}

		public async Task<IdentityResult> RegisterUser(UserModel userModel) {
			IdentityUser user = new IdentityUser {
				UserName = userModel.UserName
			};
			var result = await _userManager.CreateAsync(user, userModel.Password);
			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password) {
			IdentityUser user = await _userManager.FindAsync(userName, password);
			return user;
		}

		public Client FindClient(string clientId) {
			var client = _authContext.Clients.Find(clientId);

			return client;
		}

		public async Task<bool> AddRefreshToken(RefreshToken token) {

			var existingToken = _authContext.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

			if (existingToken != null) {
				var result = await RemoveRefreshToken(existingToken);
			}

			_authContext.RefreshTokens.Add(token);

			return await _authContext.SaveChangesAsync() > 0;
		}

		public async Task<bool> RemoveRefreshToken(string refreshTokenId) {
			var refreshToken = await _authContext.RefreshTokens.FindAsync(refreshTokenId);

			if (refreshToken != null) {
				_authContext.RefreshTokens.Remove(refreshToken);
				return await _authContext.SaveChangesAsync() > 0;
			}

			return false;
		}

		public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken) {
			_authContext.RefreshTokens.Remove(refreshToken);
			return await _authContext.SaveChangesAsync() > 0;
		}

		public async Task<RefreshToken> FindRefreshToken(string refreshTokenId) {
			var refreshToken = await _authContext.RefreshTokens.FindAsync(refreshTokenId);

			return refreshToken;
		}

		public List<RefreshToken> GetAllRefreshTokens() {
			return _authContext.RefreshTokens.ToList();
		}

		public void Dispose() {
			_authContext.Dispose();
			_userManager.Dispose();
		}
	}
}