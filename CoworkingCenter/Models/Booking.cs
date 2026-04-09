using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoworkingCenter.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Время начала")]
        public DateTime StartTime { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Время окончания")]
        public DateTime EndTime { get; set; }
        
        [Required]
        [ForeignKey("Resident")]
        [Display(Name = "Резидент")]
        public int ResidentId { get; set; }
        
        [Required]
        [ForeignKey("Workplace")]
        [Display(Name = "Рабочее место")]
        public int WorkplaceId { get; set; }
        
        [Display(Name = "Общее время аренды (часы)")]
        public double TotalHours => (EndTime - StartTime).TotalHours;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Стоимость")]
        public decimal TotalCost { get; set; }
        
        public virtual Resident? Resident { get; set; }
        public virtual Workplace? Workplace { get; set; }
    }
}