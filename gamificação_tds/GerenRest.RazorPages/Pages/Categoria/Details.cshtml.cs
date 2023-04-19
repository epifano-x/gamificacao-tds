using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Details : PageModel
    {
        private readonly AppDbContext _context;
        public CategoriaModel CatModel { get; set; } = new();
        public Details(AppDbContext context)
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
    }
}