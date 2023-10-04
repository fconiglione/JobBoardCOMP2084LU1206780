using JobBoardCOMP2084LU1206780.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobBoardCOMP2084LU1206780.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<JobBoardCOMP2084LU1206780.Models.Company> Companies { get; set; }
		public DbSet<JobListing> JobListings { get; set; }
	}
}