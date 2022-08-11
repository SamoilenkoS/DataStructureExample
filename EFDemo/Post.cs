using System.Collections.Generic;

namespace EFDemo
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"{PostId} {Title} {Content} {Blog}";
        }
    }
}
