namespace TripTastic.Application.DTOs.Admin;

public class AdminDashboardResponse
{
    public int TotalCustomers { get; set; }

    public int TotalDrivers { get; set; }
    public int ApprovedDrivers { get; set; }
    public int PendingDrivers { get; set; }

    public int TotalVehicles { get; set; }
    public int ApprovedVehicles { get; set; }
    public int PendingVehicles { get; set; }

    public int TotalBookings { get; set; }
    public int PendingBookings { get; set; }
    public int AcceptedBookings { get; set; }
    public int StartedBookings { get; set; }
    public int CompletedBookings { get; set; }
    public int RejectedBookings { get; set; }

    public int TotalPayments { get; set; }

    public int TotalReviews { get; set; }
}