namespace NaruuroApi.Model
{
    public class Booking
    {
            public int ID { get; set; }
            public string? CUSTOMER_TELL { get; set; }
            public decimal Amount { get; set; }
            public string? ROOM_NUMBER { get; set; }
            public DateTime? DATE_TIME { get; set; }
            public DateTime? CHECKOUT { get; set; }
            public string? Discription { get; set; }
            public DateTime? updated { get; set; }
            public DateTime? dailyupdate { get; set; }
            public string? USERNAME { get; set; }
            public string? Status { get; set; }
    }
  
}
