﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AngularJSAuth.API.Models;

namespace AngularJSAuth.API.Migrations {
	public class Configuration : DbMigrationsConfiguration<AuthContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(AuthContext context) {
			if (context.Clients.Count() > 0)
				return;

			context.Clients.AddRange(BuildClientsList());
			context.SaveChanges();
		}

		private static List<Client> BuildClientsList() {
			List<Client> ClientsList = new List<Client> {
				new Client {
					Id = "ngAuthApp",
					Secret= Helper.GetHash("abc@123"),
					Name="AngularJS front-end Application",
					ApplicationType =  Models.ApplicationTypes.JavaScript,
					Active = true,
					RefreshTokenLifeTime = 7200,
					AllowedOrigin = "http://testspa"
				},
				new Client {
					Id = "consoleApp",
					Secret=Helper.GetHash("123@abc"),
					Name="Console Application",
					ApplicationType = ApplicationTypes.NativeConfidential,
					Active = true,
					RefreshTokenLifeTime = 14400,
					AllowedOrigin = "*"
				}
			};
			return ClientsList;
		}
	}
}