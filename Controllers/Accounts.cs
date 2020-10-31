using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web31.Models.Accounts;

namespace WebAdvert.Web31.Controllers
{
    public class Accounts : Controller
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public Accounts(SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._pool = pool;
        }
        public async Task<IActionResult> SignUp() {
            var model = new SignupModel();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(SignupModel model) {
            var user = _pool.GetUser(model.Email);
            if (ModelState.IsValid)
            {
                if (user.Status != null)
                {
                    ModelState.AddModelError("UserExists","User with this code already exists");
                }
            }
            //user.Attributes.Add(CognitoAttributesConstants.Name, model.Email);
            user.Attributes.Add(CognitoAttribute.Name.ToString() , model.Email);
            var createUser = await _userManager.CreateAsync(user, model.Password);
            if (createUser.Succeeded)
            {
                RedirectToAction("Confirm");
            }
            return View();
        }
    }
}
