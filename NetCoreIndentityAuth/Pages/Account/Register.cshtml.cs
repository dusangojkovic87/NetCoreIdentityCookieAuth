using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreIndentityAuth.Services.IRepository;
using NetCoreIndentityAuth.Models;
using Microsoft.AspNetCore.Identity;
using NetCoreIndentityAuth.Entities;

namespace NetCoreIndentityAuth.Pages
{
    [BindProperties]
    public class RegisterModelPage : PageModel
    {
        [BindProperty]
        public RegisterModel model { get; set; }

        private IAuthenticate _repository { get; set; }
        private UserManager<User> _userManager { get; set; }
        public RegisterModelPage(IAuthenticate repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();


            }

            var isRegistered = _repository.isRegisterd(model);
            if (isRegistered == true)
            {
                return Redirect("/account/login");

            }

            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                NormalizedEmail = model.Email,
                Password = model.Password,
                UserName = model.Email

            };



            var hashPass = _userManager.PasswordHasher.HashPassword(user, model.Password);
            user.Password = hashPass;

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return Redirect("/account/login");
            }

            ModelState.AddModelError("model.Password", "User registration failed!");

            return Page();


        }

    }
}
