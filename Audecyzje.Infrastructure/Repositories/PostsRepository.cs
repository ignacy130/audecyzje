using Audecyzje.Core.Domain;
using Audecyzje.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audecyzje.Infrastructure.Repositories
{
	public class PostsRepository : Repository<Post>, IPostsRepository
	{
		public PostsRepository(WarsawContext context) : base(context)
		{
		}
	}
}
