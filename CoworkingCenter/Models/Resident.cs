// Models/Resident.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoworkingCenter.Models
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "ФИО/Компания")]
        public string FullNameOrCompany { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Контакты")]
        public string Contacts { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Тариф")]
        public string Tariff { get; set; } = string.Empty;
        
        [Display(Name = "Дата начала абонемента")]
        public DateTime SubscriptionStartDate { get; set; }
        
        [Display(Name = "Дата окончания абонемента")]
        public DateTime SubscriptionEndDate { get; set; }
        
        [Display(Name = "Активен")]
        public bool IsActive => SubscriptionStartDate <= DateTime.Today && SubscriptionEndDate >= DateTime.Today;
        
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}