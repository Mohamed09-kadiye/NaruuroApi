using System;
namespace NaruuroApi.Model
{
    // Expense.cs
    public class Expense
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public string ?Description { get; set; }
        public string? CategoryId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? updated { get; set; }
        public string? userid { get; set; }
    }

	
}

