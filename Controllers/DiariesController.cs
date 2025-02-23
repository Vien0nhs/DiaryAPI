using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiaryAPI.Data;
using DiaryAPI.Models;

namespace DiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiariesController : ControllerBase
    {
        private readonly DiaryDbContext _context;

        public DiariesController(DiaryDbContext context)
        {
            _context = context;
        }

        // GET: api/Diaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diary>>> GetDiaries()
        {
            return await _context.Diaries.ToListAsync();
        }

        // GET: api/Diaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diary>> GetDiary(int id)
        {
            var diary = await _context.Diaries.FindAsync(id);

            if (diary == null)
            {
                return NotFound();
            }

            return diary;
        }

        // PUT: api/Diaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiary(int id, Diary diary)
        {
            if (id != diary.DiaryID)
            {
                return BadRequest();
            }

            _context.Entry(diary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Diaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diary>> PostDiary(Diary diary)
        {
            _context.Diaries.Add(diary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiary", new { id = diary.DiaryID }, diary);
        }

        // DELETE: api/Diaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiary(int id)
        {
            var diary = await _context.Diaries.FindAsync(id);
            if (diary == null)
            {
                return NotFound();
            }

            _context.Diaries.Remove(diary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiaryExists(int id)
        {
            return _context.Diaries.Any(e => e.DiaryID == id);
        }
    }
}
