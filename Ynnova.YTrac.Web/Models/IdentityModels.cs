using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AspNet.Identity.RavenDB.Entities;

namespace Ynnova.YTrac.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : RavenUser
    {
		public ApplicationUser(string userName)
			: base(userName) { }

		public ApplicationUser(string userName, string email)
			: base(userName, email) { }

		public string Note { get; set; }
    }
}