using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreIndentityAuth.Entities;
using NetCoreIndentityAuth.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreIndentityAuth.Pages
{
    public class LoginModelPage : PageModel
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public LoginModelPage(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [BindProperty]
        public LoginModel model { get; set; }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return Page();

            }

            var userFromDb = await _userManager.FindByEmailAsync(model.Email);
            if (userFromDb == null)
            {
                ModelState.AddModelError("model.Password", "User not found,register first!");
                return Page();

            }

            var isPassCorect = _userManager.PasswordHasher.VerifyHashedPassword(userFromDb, userFromDb.Password, model.Password);
            if (isPassCorect != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("model.Password", "Incorrect Password");
                return Page();

            }
            var listClaims = new List<Claim>();
            listClaims.Add(new Claim(ClaimTypes.Name, userFromDb.Name));
            listClaims.Add(new Claim(ClaimTypes.Surname, userFromDb.Surname));
            listClaims.Add(new Claim(ClaimTypes.Email, userFromDb.Email));

            await _signInManager.SignInWithClaimsAsync(userFromDb, true, listClaims);
            return Redirect("/private");


        }

    }

}

