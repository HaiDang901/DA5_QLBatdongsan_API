using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatdongsanAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BatdongsanAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaiKhoanController : Controller
    {

        private readonly CoreDbContext _context;
        public TaiKhoanController(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Register([FromBody] TblTaiKhoan user)
        {
            var us = user = await _context.TblTaiKhoans.FirstOrDefaultAsync(u => u.TaiKhoan == user.TaiKhoan && u.MatKhau == user.MatKhau && u.LoaiTk == "user");
            return Ok(us);
        }
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TblTaiKhoan>>> getLoai(string id)
        {
            return await _context.TblTaiKhoans.Where(x => x.LoaiTk == id.ToLower()).ToListAsync();
        }

        [HttpPost]
        public async Task<int> addAdmin(TblTaiKhoan _taikhoan)
        {
            TblTaiKhoan _tk = new TblTaiKhoan()
            {
                MaTk = "",
                HoTen = _taikhoan.HoTen,
                TaiKhoan = _taikhoan.TaiKhoan,
                MatKhau = _taikhoan.MatKhau,
                DiaChi = _taikhoan.DiaChi,
                Sdt = _taikhoan.Sdt, 
                Email = _taikhoan.Email,
                SoDuTk = 0,
                LoaiTk = "user",
                TrangThai = "1"
            };
            _context.TblTaiKhoans.Add(_tk);
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        [HttpGet("{id}")]
        public async Task<int> TrangThai(string id)
        {
            TblTaiKhoan _tk = await _context.TblTaiKhoans.FindAsync(id);
            if (_tk.TrangThai == "1")
            {
                _tk.TrangThai = "0";
            }
            else
            if (_tk.TrangThai == "0")
            {
                _tk.TrangThai = "1";
            }
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
