namespace NaruuroApi.Model
{
    public class Customer
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Names { get; set; }
        public string? Gender { get; set; }
        public string? Tell { get; set; }
        public string? Address { get; set; }
        public string? ID_Documents { get; set; }
        public string? drivelinkid { get; set; }

        public DateTime RegisteredDate { get; set; }
        public DateTime Updated { get; set; }
        public string? RegisteredBy { get; set; }
    }

 

}
