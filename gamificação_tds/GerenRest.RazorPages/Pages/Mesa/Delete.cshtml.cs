using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public MesaModel MesaModel { get; set; } = new();
        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Mesas == null) {
                return NotFound();
            }

            var mesaModel = await _context.Mesas.FirstOrDefaultAsync(e => e.MesaID == id);

            if(mesaModel == null) {
                return NotFound();
            }

            MesaModel = mesaModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var mesaToDelete = await _context.Mesas!.FindAsync(id);

            if(mesaToDelete == null) {
                return NotFound();
            }

            try {
                _context.Mesas.Remove(mesaToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Mesa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}