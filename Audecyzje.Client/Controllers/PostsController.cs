using Audecyzje.Core.Domain;
using Audecyzje.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.Client.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;
        private readonly UserManager<IdentityUser> _userManager;
        private string GetUserId() => _userManager.GetUserId(HttpContext.User);

        public PostsController(IPostsService postsService, UserManager<IdentityUser> userManager)
        {
            _postsService = postsService;
            _userManager = userManager;
        }

        [Authorize]
        public void Create(int parentId, string title, string content, string authorId, bool isPublished)
        {
            _postsService.Create(parentId, title, content, GetUserId(), isPublished);
        }

        [Authorize]
        public async Task Edit(int id, int parentId, string title, string content, bool isPublished)
        {
            await _postsService.Edit(id, parentId, title, content, isPublished, GetUserId());
        }

        public async Task<Post> Get(int id)
        {
            return await _postsService.Get(id);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postsService.GetAll();
        }

        [Authorize]
        public void Delete(int id)
        {
            _postsService.Delete(id);
        }
    }
}
