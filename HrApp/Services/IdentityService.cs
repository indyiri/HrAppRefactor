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
                var searchUserByEmail = await _userManager.FindByEmailAsync(email);

                if (searchUserByEmail is not null)
                {
                    result.SignInResult = await _signInManager.PasswordSignInAsync(searchUserByEmail, password, false, false);
                }
                else if (searchUserByEmail is null)
                {
                    var searchUserByUserName = await _userManager.FindByNameAsync(username);
                    result.SignInResult = await _signInManager.PasswordSignInAsync(searchUserByUserName, password, false, false);
                }
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
