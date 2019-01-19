using Audecyzje.Core.Domain;
using Audecyzje.Core.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Services
{
	public class PostsService : IPostsService
	{
		private readonly IPostsRepository _postsRepository;
		private readonly IMapper _mapper;

		public PostsService(IPostsRepository documentRepository, IMapper mapper)
		{
			_postsRepository = documentRepository;
			_mapper = mapper;
		}

		public void Create(int parentId, string title, string content, string authorId, bool isPublished)
		{
			_postsRepository.Create(new Post()
			{
				AuthorId = authorId,
				IsPublished = isPublished,
				ModifiedAt = null,
				ParentId = parentId,
				PublishedAt = null,
				Title = title,
				CreatedAt = DateTime.Now,
                Content = content
			});
		}

		public void Delete(int id)
		{
			_postsRepository.Delete(id);
		}

		public async Task Edit(int id, int parentId, string title, string content, bool isPublished, string editorId)
		{
			var post = await _postsRepository.Get(id);
			post.ParentId = parentId;
			post.Title = title;
			post.Content = content;
			post.ModifiedAt = DateTime.Now;

			if(!post.IsPublished && isPublished && post.PublishedAt == null)
			{
				post.IsPublished = isPublished;
				post.PublishedAt = DateTime.Now;
			}
			else
			{
				post.IsPublished = isPublished;
			}

			await _postsRepository.Update(post);
		}

		public async Task<Post> Get(int id)
		{
			return await _postsRepository.Get(id);
		}

		public async Task<IEnumerable<Post>> GetAll()
		{
			return await _postsRepository.GetAll();
		}
	}
}
