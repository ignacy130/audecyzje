using Audecyzje.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure
{
	public interface IPostsService
	{
		void Create(int parentId, string title, string content, string authorId, bool isPublished);
		Task Edit(int id, int parentId, string title, string content, bool isPublished, string editorId);
		Task<Post> Get(int id);
		Task<IEnumerable<Post>> GetAll();
		void Delete(int id);
	}
}
