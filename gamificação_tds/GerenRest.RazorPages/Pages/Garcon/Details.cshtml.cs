using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Details : PageModel
    {
        private readonly AppDbContext _context;
        public GarconModel GarconModel { get; set; } = new();
        public Details(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Mesas == null) {
                return NotFound();
            }

            var garconModel = await _context.Garcons!.FirstOrDefaultAsync(e => e.GarconID == id);

            if(garconModel == null) {
                return NotFound();
            }

            GarconModel = garconModel;

            return Page();
        }
    }
}