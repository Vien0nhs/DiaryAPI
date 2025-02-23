using DiaryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.Data
{
	public class DiaryDbContext : DbContext
	{
		public DiaryDbContext(DbContextOptions<DiaryDbContext> options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Diary> Diaries { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany<Diary>()
				.WithOne(d => d.User)
				.HasForeignKey(d => d.UserID)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
