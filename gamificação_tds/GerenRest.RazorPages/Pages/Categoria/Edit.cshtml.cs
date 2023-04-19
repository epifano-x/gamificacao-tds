using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public CategoriaModel CatModel { get; set; } = new();
        public Edit(AppDbContext context)
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
            if(!ModelState.IsValid)
                return Page();

            var catToUpdate = await _context.Categorias!.FindAsync(id);

            if(catToUpdate == null) {
                return NotFound();
            }

            catToUpdate.Nome = CatModel.Nome;
            catToUpdate.Descricao = CatModel.Descricao;

            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Categoria/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}