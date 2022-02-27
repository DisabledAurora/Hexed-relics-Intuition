using System.ComponentModel.DataAnnotations;

namespace Hexed_Relics.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateCreated { get; set; }
        public float? TotalBalance { get; set; }
    }
}
