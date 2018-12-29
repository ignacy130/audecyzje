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
        public void Create(int parentId, string title, string content, bool isPublished)
        {
            _postsService.Create(parentId, title, content, GetUserId(), isPublished);
        }

        [Authorize]
        public async Task Edit(int id, int parentId, string title, string content, bool isPublished)
        {
            await _postsService.Edit(id, parentId, title, content, isPublished, GetUserId());
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(int id)
        {
            return await _postsService.Get(id);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Post>> GetAll()
        {
            var post = new Post()
            {
                Id = 1,
                AuthorId = Guid.NewGuid().ToString(),
                Content = "Bitwa pod Aguere – bitwa stoczona 14 listopada 1494 r. pomiędzy Kastylijczykami (1000 ludzi) i Guanczami (ok. 5000 ludzi), jeden z epizodów podboju Wysp Kanaryjskich[1].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                Title = "Bitwa pod Aguere",
                ParentId = -1,
            };

            var post2 = new Post()
            {
                Id = 2,
                AuthorId = Guid.NewGuid().ToString(),
                Title = "Tło",
                Content = "Alonso Luis Fernández de Lugo rozpoczął podbój Teneryfy 1 maja 1494 r., po kilku wcześniejszych, nieudanych próbach podporządkowania wyspy Kastylii. Teneryfa była wówczas ostatnią wyspą archipelagu Wysp Kanaryjskich, która opierała się europejskiej inwazji od czasu, gdy Lugo opanował Gran Canarię i La Palmę. Wyspa była wówczas podzielona pomiędzy 9 królestw rządzonych przez osobnych wodzów, z czego 4 zdecydowało się na współpracę z najeźdźcami[2].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = post.Id,
            };

            var post3 = new Post()
            {
                Id = 3,
                AuthorId = Guid.NewGuid().ToString(),
                Title = "Bitwa i następstwa",
                Content = "Lugo przeżył bitwę[3], wycofał się na Gran Canarię i powrócił na wyspę[2] z silniejszym oddziałem i stoczył z tubylcami bitwę pod Aguere, na otwartym terenie, gdzie mógł wykorzystać swoją przewagę techniczną. Europejczycy rozpoczęli bitwę od salwy artyleryjskiej, a następnie nastąpiła szarża kawalerii ubezpieczanej przez piechotę. Efektem starcia było zdecydowane zwycięstwo Kastylijczyków[3].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = post.Id,
            };

            return new List<Post>() { post, post2, post3 };
            return await _postsService.GetAll();
        }

        [HttpGet("GetAllPublished")]
        public async Task<IEnumerable<Post>> GetAllPublished()
        {
            var post = new Post()
            {
                Id = 1,
                AuthorId = Guid.NewGuid().ToString(),
                Content = "Bitwa pod Aguere – bitwa stoczona 14 listopada 1494 r. pomiędzy Kastylijczykami (1000 ludzi) i Guanczami (ok. 5000 ludzi), jeden z epizodów podboju Wysp Kanaryjskich[1].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                Title = "Bitwa pod Aguere",
                ParentId = -1,
            };

            var post2 = new Post()
            {
                Id = 2,
                AuthorId = Guid.NewGuid().ToString(),
                Title = "Tło",
                Content = "Alonso Luis Fernández de Lugo rozpoczął podbój Teneryfy 1 maja 1494 r., po kilku wcześniejszych, nieudanych próbach podporządkowania wyspy Kastylii. Teneryfa była wówczas ostatnią wyspą archipelagu Wysp Kanaryjskich, która opierała się europejskiej inwazji od czasu, gdy Lugo opanował Gran Canarię i La Palmę. Wyspa była wówczas podzielona pomiędzy 9 królestw rządzonych przez osobnych wodzów, z czego 4 zdecydowało się na współpracę z najeźdźcami[2].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = post.Id,
            };

            var post3 = new Post()
            {
                Id = 3,
                AuthorId = Guid.NewGuid().ToString(),
                Title = "Bitwa i następstwa",
                Content = "Lugo przeżył bitwę[3], wycofał się na Gran Canarię i powrócił na wyspę[2] z silniejszym oddziałem i stoczył z tubylcami bitwę pod Aguere, na otwartym terenie, gdzie mógł wykorzystać swoją przewagę techniczną. Europejczycy rozpoczęli bitwę od salwy artyleryjskiej, a następnie nastąpiła szarża kawalerii ubezpieczanej przez piechotę. Efektem starcia było zdecydowane zwycięstwo Kastylijczyków[3].",
                CreatedAt = DateTime.Now,
                IsPublished = true,
                ModifiedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                ParentId = post.Id,
            };

            return new List<Post>() { post, post2, post3 };
        }

        [Authorize]
        public void Delete(int id)
        {
            _postsService.Delete(id);
        }
    }
}
