using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Deliverables.Data.Base;

namespace Deliverables.Data.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser, IBaseEntity
    {
        #region Properties

        public int? UserCompanyId { get; set; }

        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        public string Notes { get; set; }

        public bool PasswordChanged { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }

        public bool SendNotificationsByEmail { get; set; }

        public bool IsFirmAdmin { get; set; }

        #endregion


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> userManager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            if (UserCompanyId.HasValue)
                userIdentity.AddClaim(new Claim("userFirmId", UserCompanyId.Value.ToString()));
            userIdentity.AddClaim(new Claim("isFirmAdmin", IsFirmAdmin.ToString()));

            return userIdentity;
        }
    }
}
