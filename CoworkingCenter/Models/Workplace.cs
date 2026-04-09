using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoworkingCenter.Models
{
    public class Workplace
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Номер стола")]
        public string TableNumber { get; set; } = string.Empty;
        
        [Display(Name = "Оснащение")]
        public string Equipment { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена в час")]
        public decimal PricePerHour { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена в день")]
        public decimal PricePerDay { get; set; }
        
        [Display(Name = "Доступно")]
        public bool IsAvailable { get; set; } = true;
        
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}