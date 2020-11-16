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
            var createUser = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            if (createUser.Succeeded)
            {
                RedirectToAction("Confirm");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Confirm(/*ConfirmModel model*/) {
            var _model = new ConfirmModel();
            return View(_model);
        }
        [HttpPost]
        public async Task <ActionResult> Confirm(ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    ModelState.AddModelError("not-found", "User not found");
                    return View(model);
                var result = await _userManager.ConfirmEmailAsync(user, model.Code);
                if (result.Succeeded)
                    return RedirectToAction("Index","Home");
                else
                    foreach (var item in result.Errors)
                        ModelState.AddModelError(item.Code, item.Description);

            }
            return View(model);
        }
    }
}
