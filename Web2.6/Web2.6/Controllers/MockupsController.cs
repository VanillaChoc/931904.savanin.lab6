using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web2._6.Data;
using Web2._6.Models;

namespace Web2._6.Controllers
{
    public class MockupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _appEnvironment;

        public MockupsController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: ForumCategories
        public async Task<IActionResult> AllForums()
        {
            return _context.Forums != null ?
                        View(await _context.Forums.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SingleForum()
        {
            var error = CheckForNullTables();
            if (error != null) return error;

            var firstForum = _context.Forums.First();

            ViewBag.ForumName = firstForum.Name;

            var forumTopics = await _context.Topics.Where(rec => rec.ParentForumId == firstForum.Id).ToListAsync();

            var replies = _context.Replies;

            ViewBag.TopicsRepliesAmount = new Dictionary<int, int>();
            ViewBag.TopicsReplies = new Dictionary<int, Reply[]>();

            foreach (var topic in forumTopics)
            {
                var tempReplies = await replies.Where(rec => rec.ParentTopicId == topic.Id).ToListAsync();

                ViewBag.TopicsRepliesAmount[topic.Id] = tempReplies.Count;
                ViewBag.TopicsReplies[topic.Id] = new Reply[2];
                ViewBag.TopicsReplies[topic.Id][0] = tempReplies.OrderBy(x => x.CreatedDate).FirstOrDefault();
                ViewBag.TopicsReplies[topic.Id][1] = tempReplies.OrderBy(x => x.CreatedDate).LastOrDefault();
            }

            return View(forumTopics);
        }

        public async Task<IActionResult> SingleTopic()
        {
            var error = CheckForNullTables();
            if (error != null) return error;

            var firstTopic = _context.Topics.First();
            var parentForum = _context.Forums.Where(x => x.Id == firstTopic.ParentForumId).First();
            ViewBag.ParentTopic = firstTopic;
            ViewBag.ParentForum = parentForum;

            var topicReplies = await _context.Replies.Where(rec => rec.ParentTopicId == firstTopic.Id).ToListAsync();

            ViewBag.RepliesAttachedFiles = new Dictionary<int, List<UserFile>>();

            var files = _context.AttachedFiles;

            foreach (var reply in topicReplies)
            {
                var tempFiles = await files.Where(rec => rec.ParentReplyId == reply.Id).ToListAsync();
                ViewBag.RepliesAttachedFiles[reply.Id] = tempFiles;
            }

            return View(topicReplies);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, int replyId)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                UserFile file = new UserFile { Name = uploadedFile.FileName, Path = path, ParentReplyId = replyId };
                _context.AttachedFiles.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private IActionResult? CheckForNullTables() {
            if (_context.Forums == null)
                return Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
            if (_context.Topics == null)
                return Problem("Entity set 'ApplicationDbContext.Topics'  is null.");
            if (_context.Replies == null)
                return Problem("Entity set 'ApplicationDbContext.Replies'  is null.");
            return null;
        }
    }
}