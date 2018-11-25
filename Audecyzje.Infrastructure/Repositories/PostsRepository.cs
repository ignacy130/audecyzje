using Audecyzje.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audecyzje.Infrastructure.Repositories
{
	public class PostsRepository : Repository<Post>
	{
		public PostsRepository(WarsawContext context) : base(context)
		{
		}
	}
}
