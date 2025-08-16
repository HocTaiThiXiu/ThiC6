using System.ComponentModel.DataAnnotations;

namespace ThiC6.Models
{
    public class SinhVien
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ten khong duoc de trong")]
        public string Name { get; set; }
        public string? ImagePath { get; set; }
    }
}
