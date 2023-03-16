using System;
namespace PrismMauiTesting.Models
{
	public class LecTeam
	{
		public string Name { get; set; }

        public string OperatingCountry { get; set; }

        public string LogoUrl { get; set; }

        public int Titles { get; set; }

        public DateTime Joined { get; set; }

        public DateTime? Exited { get; set; }

        public bool IsActive { get; set; }
    }
}

