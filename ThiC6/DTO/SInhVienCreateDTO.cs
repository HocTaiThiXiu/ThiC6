using System.ComponentModel.DataAnnotations;

namespace ThiC6.DTO
{
    public class SInhVienCreateDTO
    {
        [Required(ErrorMessage = "Ten khong duoc de trong")]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
