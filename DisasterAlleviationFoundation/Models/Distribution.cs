using DisasterAlleviationFoundation.Models;

public class Distribution
{
    public int DistributionID { get; set; }
    public int CrisisID { get; set; }
    public int ResourceID { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }

    // Navigation properties
    public Crisis Crisis { get; set; }
    public Resource Resource { get; set; }

    // Optional: link to Donation
    public int? DonationID { get; set; }
    public Donation Donation { get; set; }
}
