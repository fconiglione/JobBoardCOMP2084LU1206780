namespace JobBoardCOMP2084LU1206780.Models
{
	public class Company
	{
		public int CompanyId { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public string Industry { get; set; }

		public List<JobListing> JobListings { get; set; }
	}
}
