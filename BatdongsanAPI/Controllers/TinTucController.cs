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
        public async Task<int> addNew(TinTucModel news)
        {
            TblTinTuc _new = new TblTinTuc()
            {
                MaTinTuc = "",
                TieuDe = news.TieuDe,
                NoiDung = news.NoiDung
            };
            _context.TblTinTucs.Add(_new);
            int res;
            try
            {
                res = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //TblTinTuc _news = _context.TblTinTucs.OrderByDescending(x => x.MaTinTuc).FirstOrDefault();

            return res;

        }


        [HttpPost]
        public ResponseModel getNews([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            List<TblTinTuc> _news = null;
            int _skip = (page - 1) * 10;
            response.TotalItems = _context.TblTinTucs.Count();

            _news = _context.TblTinTucs.OrderByDescending(x => x.MaTinTuc).Skip(_skip).Take(10).ToList();

            response.Data = _news;
            response.Page = page;
            return response;

            //return null;
        }

        [HttpDelete("{id}")]
        public async Task<int> deleteTT(string id)
        {
            try
            {
                TblTinTuc _news = await _context.TblTinTucs.FindAsync(id);
                if (_news == null) return -1;
                _context.TblTinTucs.Remove(_news);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        [HttpPut("{id}")]
        public async Task<int> editNew(string id, TinTucModel news)
        {
            try
            {
                TblTinTuc _news = await _context.TblTinTucs.FindAsync(id);
                if (_news == null) return -1;
                _news.TieuDe = news.TieuDe;
                _news.NoiDung = news.NoiDung;
                _context.TblTinTucs.Update(_news);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblTinTuc>> getDetail(string id)
        {
            var news = await _context.TblTinTucs.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return news;
        }

        // GET: api/<TinTucController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<TinTucController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TinTucController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TinTucController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TinTucController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
