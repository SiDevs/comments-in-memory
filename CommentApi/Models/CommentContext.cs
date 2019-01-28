using Microsoft.EntityFrameworkCore;

namespace CommentApi.Models
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options)
            : base(options)
        {
        }

        public DbSet<CommentItem> CommentItems { get; set; }
    }
}
