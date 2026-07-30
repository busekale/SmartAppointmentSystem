using System.ComponentModel.DataAnnotations;

namespace SmartAppointmentSystem.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [Display(Name = "Ad Soyad")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    [Display(Name = "Telefon")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
    [Display(Name = "Hizmet")]
    public string Service { get; set; } = string.Empty;

    [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
    [Display(Name = "Randevu Tarihi")]
    public DateTime AppointmentDate { get; set; }

    [Display(Name = "Notlar")]
    public string? Notes { get; set; }

    [Display(Name = "Durum")]
    public string Status { get; set; } = "Bekliyor";
}