using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThiC6.Data;
using ThiC6.DTO;
using ThiC6.Models;

namespace ThiC6.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly QLSVDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SinhVienController(QLSVDbContext db, IWebHostEnvironment env)
        {
            _db=db;
            _env=env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.SinhViens.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SInhVienCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Image!=null)
            {
                var ext = Path.GetExtension(dto.Image.FileName).ToLower();
                var allow = new[] { ".jpg", ".jpeg", ".tiff" };
                if(!allow.Contains(ext))
                {
                    return BadRequest("Sai dinh dang");
                }
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(_env.WebRootPath,"Images", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);
                var sv = new SinhVien()
                {
                    Name = dto.Name,
                    ImagePath = "Images/"+fileName
                };
                await _db.SinhViens.AddAsync(sv);
                await _db.SaveChangesAsync();
                return Ok(dto);
            }
            return BadRequest("Vui long chon anh");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] SInhVienCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var svien = await _db.SinhViens.FindAsync(id);
            if (svien==null)
            {
                return NotFound("Khong tim thay sinh vien");
            }
            svien.Name = dto.Name;
            if (dto.Image!=null)
            {
                var ext = Path.GetExtension(dto.Image.FileName).ToLower();
                var allow = new[] { ".jpg", ".jpeg", ".tiff" };
                if (!allow.Contains(ext))
                {
                    return BadRequest("Sai dinh dang");
                }
                if(!string.IsNullOrEmpty(svien.ImagePath))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath,svien.ImagePath);
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Exists(oldImagePath);
                    }
                }
                var fileName = Guid.NewGuid().ToString();
                var path = Path.Combine(_env.WebRootPath, "Images", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);
                svien.ImagePath = "Images/"+fileName;
            }
            _db.SinhViens.Update(svien);
            await _db.SaveChangesAsync();
            return Ok(svien);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var sv = await _db.SinhViens.FindAsync(id);
            if(sv == null)
            {
                return NotFound();
            }
            _db.SinhViens.Remove(sv);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
