using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoworkingCenter.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Сумма")]
        public decimal Amount { get; set; }
        
        [Required]
        [Display(Name = "Услуга")]
        public string Service { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("Resident")]
        [Display(Name = "Резидент")]
        public int ResidentId { get; set; }
        
        public virtual Resident? Resident { get; set; }
    }
}