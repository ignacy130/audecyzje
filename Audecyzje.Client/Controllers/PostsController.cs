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
    [Authorize]
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

        [HttpPost]
        public void Create([FromBody]Post post)
        {
            _postsService.Create(post.ParentId, post.Title, post.Content, GetUserId(), post.IsPublished);
        }

        [HttpPatch]       
        public async Task Edit([FromBody]Post post)
        {
            await _postsService.Edit(post.Id, post.ParentId, post.Title, post.Content, post.IsPublished, GetUserId());
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(int id)
        {
            return await _postsService.Get(id);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postsService.GetAll();
        }

        [HttpGet("GetAllPublished")]
        [AllowAnonymous]
        public async Task<IEnumerable<Post>> GetAllPublished()
        {
            if(!string.IsNullOrEmpty(GetUserId().Trim()))
            {
                return (await _postsService.GetAll());
            }
            else
            {
                return (await _postsService.GetAll()).Where(x => x.IsPublished);
            }
            
        }

        [Authorize]
        public void Delete(int id)
        {
            _postsService.Delete(id);
        }
    }
}
