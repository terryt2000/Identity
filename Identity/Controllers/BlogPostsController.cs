using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Identity.Models;
using Microsoft.AspNet.Identity;

namespace Identity.Controllers
{
    [Authorize]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var posts = await db.BlogPosts
                .Where(p => p.UserId == userId)
                .Select(p => new BlogPostViewModel
                {
                    Body = p.Body,
                    Id = p.Id,
                    Title = p.Title
                })
                .ToListAsync();
            return View(posts);
        }

        // GET: BlogPosts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(new BlogPostViewModel {Body = blogPost.Body, Id = blogPost.Id, Title = blogPost.Title});
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserId,Title,Body")] BlogPostViewModel blogPost)
        {
            if (ModelState.IsValid)
            {
                //get the authenticated user's id
                var userId = User.Identity.GetUserId();
                db.BlogPosts.Add(new BlogPost
                {
                    Body = blogPost.Body,
                    Title = blogPost.Title,
                    UserId = userId
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(new BlogPostViewModel { Body = blogPost.Body, Id = blogPost.Id, Title = blogPost.Title });
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Body")] BlogPostViewModel blogPost)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var entity = new BlogPost{ Body = blogPost.Body, Id = blogPost.Id, Title = blogPost.Title, UserId = userId};
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            db.BlogPosts.Remove(blogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
