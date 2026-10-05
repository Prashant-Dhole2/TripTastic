using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Auth;
using TripTastic.Application.Interfaces.Auth;
using TripTastic.Domain.Entities;
using TripTastic.Infrastructure.Data;

namespace TripTastic.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterCustomerAsync(
        RegisterCustomerRequest request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (existingUser != null)
        {
            return false;
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,

            // Temporary for our basic registration flow.
            // Password hashing will be added in the authentication phase.
            PasswordHash = request.Password,

            Role = "CUSTOMER",
            IsActive = true
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        var customer = new Customer
        {
            UserId = user.Id,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        _context.Customers.Add(customer);

        await _context.SaveChangesAsync();

        return true;
    }
}