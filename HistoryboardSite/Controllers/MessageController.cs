using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsSite.Contract;
using NewsSite.DataAccess;
using NewsSite.Models;

namespace NewsSite.Controllers
{
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> logger;
        private readonly MessageDbContext dbContext;

        public MessageController(ILogger<MessageController> logger, MessageDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Index(CancellationToken ct)
        {
            var messages = dbContext.Messages;
            var sortedMessage = messages.OrderByDescending(time => time.PublishDate);
            logger.LogWarning("Update storys");


            return View(sortedMessage.ToList());
        }

        [NonAction]
        public ActionResult UpdateStory()
        {

            return View();
        }

        [HttpGet]
        public ActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromForm] CreateMessageRequest request, CancellationToken ct)
        {

            if (ModelState.IsValid)
            {
                var message = new Message(request.Title, request.Content);

                await dbContext.AddAsync(message, ct);
                await dbContext.SaveChangesAsync(ct);

                logger.LogWarning("Created new history");

                return RedirectToAction("Index");
            }

            return RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
