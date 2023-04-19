using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new();
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Garcons == null) {
                return NotFound();
            }

            var garconModel = await _context.Garcons!.FirstOrDefaultAsync(e => e.GarconID == id);

            if(garconModel == null) {
                return NotFound();
            }

            GarconModel = garconModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            var garconToUpdate = await _context.Garcons!.FindAsync(id);

            if(garconToUpdate == null) {
                return NotFound();
            }

            garconToUpdate.Nome = GarconModel.Nome;
            garconToUpdate.Sobrenome = GarconModel.Sobrenome;
            garconToUpdate.NumIdentificao = GarconModel.NumIdentificao;
            garconToUpdate.Telefone = GarconModel.Telefone;

            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Garcon/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}