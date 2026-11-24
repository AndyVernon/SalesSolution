namespace SalesProcessing.Api
{
    public class WeeklySales
    {
        public Guid Id { get; set; }
        public int YearNumber { get; set; }
        public int WeekNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public decimal Amount { get; set; }
    }
}
