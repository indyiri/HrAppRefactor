using HrApp.Services.Interfaces;
using HrApp.Services.Results;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HrApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityServiceResult> LoginAsync(string username, string email, string password)
        {
            var result = new IdentityServiceResult();

            try
            {
                IdentityUser user = null;

                if (!string.IsNullOrWhiteSpace(username))
                {
                    user = await _userManager.FindByNameAsync(username);
                }
                else if (!string.IsNullOrWhiteSpace(email))
                {
                    user = await _userManager.FindByEmailAsync(email);
                }

                if (user == null)
                {
                    result.Failed("User not found.");
                    return result;
                }

                var signIn = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

                result.SignInResult = signIn;
            }
            catch (Exception ex)
            {
                result.Failed(ex.Message);
            }

            return result;
        }

        public async Task<IdentityServiceResult> RegisterAsync(RegisterViewModel vm)
        {
            var result = new IdentityServiceResult();
            
            try 
            {
                var identityUser = new IdentityUser
                {
                    Email = vm.Email,
                    UserName = vm.UserName
                };

                result.IdentityResult = await _userManager.CreateAsync(identityUser, vm.Password);

            } 
            catch (Exception ex) 
            {
                result.Failed(ex.Message);
            }

            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
