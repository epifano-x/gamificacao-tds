using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        public List<MesaModel> MesaModel { get; set; } = new();
        public Index(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            MesaModel = await _context.Mesas!.ToListAsync();
            return Page();
        }
    }
}