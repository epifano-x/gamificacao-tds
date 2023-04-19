using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        public List<GarconModel> GarconModel { get; set; } = new();
        public Index(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            GarconModel = await _context.Garcons!.ToListAsync();
            return Page();
        }
    }
}