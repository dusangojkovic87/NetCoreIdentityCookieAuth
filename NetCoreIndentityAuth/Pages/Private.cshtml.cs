using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NetCoreIndentityAuth.Entities;

namespace MyApp.Namespace
{

    [Authorize]
    public class PrivateModel : PageModel
    {
        private SignInManager<User> _signInManager;


        public PrivateModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }


    }
}
