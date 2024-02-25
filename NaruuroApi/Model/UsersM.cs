namespace NaruuroApi.Model
{
    public class UserM
    {
        public int Id { get; set; }
        public string? Stafid { get; set; }
        public string? Roleid { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }

}
