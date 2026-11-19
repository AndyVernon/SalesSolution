using System.ComponentModel.DataAnnotations;

namespace Sales.Contracts
{
    public class SalesRequest
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [Required]
        public Guid? BranchId { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a non-negative value.")]
        public decimal? Amount { get; set; }
    }
}
