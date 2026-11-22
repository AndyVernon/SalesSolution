namespace SalesProcessing.Api
{
    public class DailySales
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
