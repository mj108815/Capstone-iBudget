using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBudget.Data;
using iBudget.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iBudget.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _GroupContext;
        public ChatController(
          UserManager<IdentityUser> userManager,
          ApplicationDbContext context
          )
        {
            _userManager = userManager;
            _GroupContext = context;
        }
        public IActionResult Index()
        {

            var groups = _GroupContext.UserGroup
                      .Where(gp => gp.UserName == _userManager.GetUserName(User))
                      .Join(_GroupContext.Groups, ug => ug.GroupId, g => g.ID, (ug, g) =>
                              new UserGroupViewModel
                              {
                                  UserName = ug.UserName,
                                  GroupId = g.ID,
                                  GroupName = g.GroupName
                              })
                      .ToList();

            ViewData["UserGroups"] = groups;      
            ViewData["Users"] = _userManager.Users;
            return View();
        }
    }
}