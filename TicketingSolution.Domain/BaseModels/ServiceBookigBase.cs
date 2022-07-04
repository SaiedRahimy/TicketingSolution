using System.ComponentModel.DataAnnotations;

namespace TicketingSolution.Domain
{
    public abstract class ServiceBookigBase
    {   
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string Family { get; set; }

        [Required]
        [StringLength(20)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("date must grate than now", new[] {nameof(Date)});
            }
        }
    }
}