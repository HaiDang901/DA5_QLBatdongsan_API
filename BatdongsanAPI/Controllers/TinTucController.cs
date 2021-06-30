using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatdongsanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BatdongsanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinTucController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public TinTucController(CoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTinTuc>>> GetAllNew()
        {
            return await _context.TblTinTucs.OrderByDescending(x => x.MaTinTuc).Take(6).ToListAsync();
        }
   
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TblTinTuc>>> NewTT(string id)
        {
            return await _context.TblTinTucs.OrderByDescending(x => x.MaTinTuc).Take(5).ToListAsync();
        }

        [HttpPost]
        public async Task<int> editTTTT(TblTinTuc post)
        {
            TblBaiDang _post = await _context.TblBaiDangs.FindAsync(post.MaTinTuc);
            //_post.TrangThai = post.TrangThai;
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

        [HttpPost]
        public async Task<int> addPostTT(TblTinTuc post)
        {
            TblTinTuc _post = new TblTinTuc()
            {
                MaTinTuc = "",
                TieuDe = post.TieuDe,
                NoiDung = post.NoiDung
            };
            _context.TblTinTucs.Add(_post);
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            TblTinTuc _news = _context.TblTinTucs.OrderByDescending(x => x.MaTinTuc).FirstOrDefault();

            return res;

        }


        [HttpPost]
        public ResponseModel GetNews([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            //var result = formData["id_user"].ToString();
            var result = formData["total"].ToString();
            List<TblTinTuc> _post = null;
            int _skip = (page - 1) * 6;
            response.TotalItems = _context.TblTinTucs.Where(x => x.MaTinTuc == result).Count();

            _post = _context.TblTinTucs.Where(x => x.MaTinTuc == result).OrderByDescending(x => x.MaTinTuc).Skip(_skip).Take(6).ToList();

            response.Data = _post;
            response.Page = page;
            return response;

            //return null;
        }

        // PUT api/<BaiDangController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BaiDangController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
