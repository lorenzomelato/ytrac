using AspNet.Identity.RavenDB.Stores;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Raven.Client;
using Ynnova.YTrac.Web.Models;

namespace Ynnova.YTrac.Web.Installers
{
	public class IdentityInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
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