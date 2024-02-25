namespace NaruuroApi.Model
{
    public class StaffM
    {
        internal string? RegisteredDateAsString;

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? RoleId { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedAtAsString { get; internal set; }
        //public string ?RoleTitle { get; set; }
    }
}