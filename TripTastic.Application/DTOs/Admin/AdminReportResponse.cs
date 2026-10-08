namespace TripTastic.Application.DTOs.Admin;

public class AdminReportResponse
{
    public int TotalCustomers { get; set; }

    public int TotalDrivers { get; set; }

    public int TotalVehicles { get; set; }

    public int TotalBookings { get; set; }

    public int CompletedBookings { get; set; }

    public int RejectedBookings { get; set; }

    public int TotalPayments { get; set; }

    public int TotalReviews { get; set; }
}