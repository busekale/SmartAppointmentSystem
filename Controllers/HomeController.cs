using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data;

namespace SmartAppointmentSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalAppointments =
            await _context.Appointments.CountAsync();

        ViewBag.PendingAppointments =
            await _context.Appointments
                .CountAsync(a => a.Status == "Bekliyor");

        ViewBag.CompletedAppointments =
            await _context.Appointments
                .CountAsync(a => a.Status == "Tamamlandı");

        ViewBag.TodayAppointments =
            await _context.Appointments
                .CountAsync(a => a.AppointmentDate.Date == DateTime.Today);

        return View();
    }
}