using HrApp.Services;
using HrApp.Services.Interfaces;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _service;


        public AccountController(IIdentityService service)
        {
            _service = service;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region Login Username

        [HttpGet]
        public IActionResult LoginUserName()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUserName(LoginUserNameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.LoginAsync(vm.UserName, null , vm.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.ErrorString);
                }
            }
            return View(vm);
        }

        #endregion

        #region Login Email

        [HttpGet]
        public IActionResult LoginEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginEmail(LoginEmailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.LoginAsync(null, vm.Email, vm.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.ErrorString);
                }
            }
            return View(vm);
        }

        #endregion


        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.RegisterAsync(vm);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result.ErrorString);                   
                }

            }
            return View(vm);
        }

        #endregion

        #region Logout

        public async Task<IActionResult> LogoutAsync()
        {
            await _service.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}
