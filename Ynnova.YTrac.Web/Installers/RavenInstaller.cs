using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
		}
	}
}

//	builder.Register(c => new RavenUserStore<ApplicationUser>(c.Resolve<IAsyncDocumentSession>(), false)).As<IUserStore<ApplicationUser>>().InstancePerHttpRequest();
//	builder.RegisterType<UserManager<ApplicationUser>>().InstancePerHttpRequest();
