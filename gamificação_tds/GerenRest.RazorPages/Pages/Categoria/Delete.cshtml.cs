using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public CategoriaModel CatModel { get; set; } = new();
        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Categorias == null) {
                return NotFound();
            }

            var catModel = await _context.Categorias.FirstOrDefaultAsync(e => e.CategoriaID == id);

            if(catModel == null) {
                return NotFound();
            }

            CatModel = catModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var categoriaToDelete = await _context.Categorias!.FindAsync(id);

            if(categoriaToDelete == null) {
                return NotFound();
            }

            try {
                _context.Categorias.Remove(categoriaToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Categoria/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}