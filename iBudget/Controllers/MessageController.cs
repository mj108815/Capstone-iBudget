using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBudget.Data;
using iBudget.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace iBudget.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private int memberId;
        public MessageController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            memberId = 0;
        }

        [HttpGet("{group_id}")]
        public IEnumerable<Message> GetById(int group_id)
        {
            memberId = group_id;
            return _context.Message.Where(gb => gb.GroupId == group_id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MessageViewModel message)
        {
            Message new_message = new Message { AddedBy = _userManager.GetUserName(User), message = message.message, GroupId = message.GroupId };

            _context.Message.Add(new_message);
            _context.SaveChanges();
            var options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true
            };
            var pusher = new Pusher(
                "625509",
                "2d909534416d3efe8b5f",
                "d24b62fa24a88da34cfd",
                options
            );
            var result = await pusher.TriggerAsync(
                "private-" + message.GroupId,
                "new_message",
            new { new_message },
            new TriggerOptions() { SocketId = message.SocketId });

            return new ObjectResult(new { status = "success", data = new_message });
        }

    }
}