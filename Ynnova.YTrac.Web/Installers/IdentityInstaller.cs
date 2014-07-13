using AspNet.Identity.RavenDB.Stores;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
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
					.UsingFactoryMethod(() =>
						{
							var session = container.Resolve<IAsyncDocumentSession>();
							session.Advanced.UseOptimisticConcurrency = true;

							return new RavenUserStore<ApplicationUser>(session);
						}));

			container.Register(
				Component
					.For<UserManager<ApplicationUser>>()
					.LifeStyle
						.PerWebRequest
					.UsingFactoryMethod(() =>
						{
							var manager = new UserManager<ApplicationUser>(container.Resolve<IUserStore<ApplicationUser>>());

							manager.UserValidator = new UserValidator<ApplicationUser>(manager)
								{
									AllowOnlyAlphanumericUserNames = false,
									RequireUniqueEmail = true
								};

							manager.PasswordValidator = new PasswordValidator
								{
									RequiredLength = 8,
									// Attenzione! Il nome della proprietà è un po' dubbio!
									// Qui sotto significa che devo obbligatoriamente mettere un carattere che sia diverso da una lettera o da un numero.
									RequireNonLetterOrDigit = false,
									RequireDigit = true,
									RequireLowercase = true,
									RequireUppercase = true,
								};

							manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
								new DpapiDataProtectionProvider("YTrac").Create("Email Confirmation"));

							return manager;
						}));
		}
	}
}