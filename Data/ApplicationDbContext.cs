using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Models;

namespace SmartAppointmentSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }
}