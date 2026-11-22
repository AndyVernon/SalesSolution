using System.ComponentModel.DataAnnotations;

namespace Sales.Contracts
{
    public class DailySalesDTO
    {
        public Guid Id { get; set; }
        public DateTime SalesDate { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? BranchId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
