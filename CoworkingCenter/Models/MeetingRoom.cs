using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoworkingCenter.Models
{
    public class MeetingRoom
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Вместимость")]
        public int Capacity { get; set; }
        
        [Display(Name = "Оборудование")]
        public string Equipment { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена в час")]
        public decimal PricePerHour { get; set; }
        
        [Display(Name = "Доступно")]
        public bool IsAvailable { get; set; } = true;
    }
}