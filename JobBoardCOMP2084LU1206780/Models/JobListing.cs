using System.ComponentModel.DataAnnotations;

namespace JobBoardCOMP2084LU1206780.Models
{
	public class JobListing
	{
		public int JobListingId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		[DisplayFormat(DataFormatString = "{0:c}")]
		public int Salary { get; set; }

		public int CompanyId { get; set; }
		public Company? Company { get; set; }
	}
}
