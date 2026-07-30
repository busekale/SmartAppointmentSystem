using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Models;

namespace SmartAppointmentSystem.Controllers;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AppointmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
    string? search,
    string? status)
{
    var query = _context.Appointments.AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(a =>
            a.CustomerName.Contains(search) ||
            a.Email.Contains(search) ||
            a.Phone.Contains(search) ||
            a.Service.Contains(search));
    }

    if (!string.IsNullOrWhiteSpace(status))
    {
        query = query.Where(a => a.Status == status);
    }

    ViewBag.Search = search;
    ViewBag.Status = status;

    var appointments = await query
        .OrderBy(a => a.AppointmentDate)
        .ToListAsync();

    return View(appointments);
}

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Appointment
        {
            AppointmentDate = DateTime.Now.AddDays(1)
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        appointment.Status = appointment.Status == "Bekliyor"
            ? "Tamamlandı"
            : "Bekliyor";

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}