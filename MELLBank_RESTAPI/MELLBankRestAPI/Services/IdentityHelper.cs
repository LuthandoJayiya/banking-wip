using log4net;
using MELLBankRestAPI.AuthModels;
using Microsoft.AspNetCore.Identity;

namespace MELLBankRestAPI.Services
{
    public class IdentityHelper
    {
        private static readonly ILog logger = LogManager.GetLogger("IdentityHelper");

        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityHelper(UserManager<ApplicationUser> userManager, AuthenticationContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _authContext = context;
            _roleManager = roleManager;
        }

        public async Task<bool> IsUserInRole(string userId, string role)
        {
            bool isInRole = false;
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user is not null)
                {
                    List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                    isInRole = userRoles.Any(userRole => userRole.Equals(role, StringComparison.CurrentCultureIgnoreCase));
                }
                return isInRole;
            }
            catch (Exception)
            {
                return isInRole;
            }
        }

        public async Task<bool> IsSuperUserRole(string userId)
        {
            string superRole = "Administrator";
            string superRole2 = "BankStaff";
            bool isSuperUser = false;

            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user is not null)
                {
                    List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                    isSuperUser = (userRoles.Contains(superRole)) || (userRoles.Contains(superRole2));
                }

                return isSuperUser;
            }
            catch (Exception ex)
            {
                logger.Error($"IsSuperUserRole Error for userId {userId}: " + ex);
                return isSuperUser;
            }
        }
        public async Task<bool> IsAdminUserRole(string userId)
        {
            string superRole = "Administrator";
            bool isSuperUser = false;

            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user is not null)
                {
                    List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                    isSuperUser = (userRoles.Contains(superRole));
                }

                return isSuperUser;
            }
            catch (Exception ex)
            {
                logger.Error($"IsAdminUserRole Error for userId {userId}: " + ex);
                return isSuperUser;
            }
        }

        public async Task<string> GetUserAccessLevelRole(string userId)
        {
            string adminRole = "Administrator";
            string bankStaffRole = "BankStaff";
            string customerRole = "Customer";

            string userRole = string.Empty;
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user is not null)
                {
                    List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                    if (userRoles.Contains(adminRole))
                    {
                        userRole = adminRole;
                    }
                    else if (userRoles.Contains(bankStaffRole))
                    {
                        userRole = bankStaffRole;
                    }
                    else
                    {
                        userRole = customerRole;
                    }
                }

                return userRole;
            }
            catch (Exception ex)
            {
                logger.Error($"Couldn't get User role for userId {userId}: " + ex);
                return userRole;
            }
        }
    }
}
