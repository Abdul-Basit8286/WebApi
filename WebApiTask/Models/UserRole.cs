namespace WebApiTask.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string? Admin { get; set; }
        public string? SuperAdmin { get; set; }
        public string? Manager { get; set; }
        public string? Approver { get; set; }
        public string? Editor { get; set; }
        public string? Creater  { get; set; }
        public string? Writer  { get; set; }
    }
}
