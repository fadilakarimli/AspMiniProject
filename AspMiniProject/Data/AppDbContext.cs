using AspMiniProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}
