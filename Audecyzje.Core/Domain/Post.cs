using System;
using System.Collections.Generic;
using System.Text;

namespace Audecyzje.Core.Domain
{
    public class Post : BaseEntity
    {
		public string Title { get; set; }
		public string AuthorId { get; set; }
		public int ParentId { get; set; }
		public bool IsPublished { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? PublishedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public string Content { get; set; }
	}
}
