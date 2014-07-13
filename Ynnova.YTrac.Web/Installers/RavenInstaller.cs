using AspNet.Identity.RavenDB.Stores;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ynnova.YTrac.Web.Models;

namespace Ynnova.YTrac.Web.Installers
{
	public class RavenInstaller : IWindsorInstaller
	{
		const string RavenDefaultDatabase = "YTrac";

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component
					.For<IDocumentStore>()
					.LifeStyle
						.Singleton
					.UsingFactoryMethod(() => 
						{
							var ravenStore = new DocumentStore
								{
									Url = "http://localhost:8080",
									DefaultDatabase = RavenDefaultDatabase
								}.Initialize();

							ravenStore.DatabaseCommands.EnsureDatabaseExists(RavenDefaultDatabase);

							return ravenStore; 
						}));

			container.Register(
				Component
					.For<IAsyncDocumentSession>()
					.LifeStyle
						.PerWebRequest
					.UsingFactoryMethod(() =>
						{
							return container
								.Resolve<IDocumentStore>()
								.OpenAsyncSession();
						}));

			container.Register(
				Component
					.For<IUserStore<ApplicationUser>>()
					.LifeStyle
						.PerWebRequest
					.UsingFactoryMethod<IUserStore<ApplicationUser>>(() => 
						{
							var session = container.Resolve<IAsyncDocumentSession>();
							session.Advanced.UseOptimisticConcurrency = true;

							return new RavenUserStore<ApplicationUser>(session); 
						}));

			container.Register(
				Component
					.For<UserManager<ApplicationUser>>()
					.LifeStyle
						.PerWebRequest);
		}
	}
}

