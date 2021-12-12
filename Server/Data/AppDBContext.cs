using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base version of this method as well, else we get an error later on.
            base.OnModelCreating(modelBuilder);

            #region MyRegion

            Category[] categoriesToSeed = new Category[3];

            for (int i = 1; i < 4; i++)
            {
                categoriesToSeed[i - 1] = new Category
                {
                    CategoryId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"A description of category {i}"
                };
            }

            modelBuilder.Entity<Category>().HasData(categoriesToSeed);

            #endregion

            modelBuilder.Entity<Post>(
                entitiy =>
                {
                    entitiy.HasOne(post => post.Category)
                    .WithMany(category => category.Posts)
                    .HasForeignKey("CategoryID");
                });

            #region Posts seed

            Post[] postToSeed = new Post[6];

            for (int i = 0; i < 7; i++)
            {
                string postTitle = string.Empty;
                int categoryId = 0;

                switch (i)
                {
                    case 1:
                        postTitle = "First Post";
                        categoryId = 1;
                        break;
                    case 2:
                        postTitle = "Second post";
                        categoryId = 2;
                        break;
                    case 3:
                        postTitle = "Third Post";
                        categoryId = 3;
                        break;
                    case 4:
                        postTitle = "Forth Post";
                        categoryId = 4;
                        break;
                    case 5:
                        postTitle = "Fifth Post";
                        categoryId = 5;
                        break;
                    case 6:
                        postTitle = "Sixth Post";
                        categoryId = 6;
                        break;
                    default:
                        break;
                }

                postToSeed[i - 1] = new Post
                {
                    PostId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Title = postTitle,
                    Excerpt = $"This is the excerpt for post {1}, An excerpt is a little extraction from a larger piece of text. Sort of like a preview",
                    Content = String.Empty,
                    PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm"),
                    Published = true,
                    Author = "Robin H",
                    CategoryID = categoryId
                };
            }

            modelBuilder.Entity<Post>().HasData(postToSeed);

            #endregion

        }
    }
}