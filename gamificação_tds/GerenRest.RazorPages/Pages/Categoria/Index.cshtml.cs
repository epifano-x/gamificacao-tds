using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        public List<CategoriaModel> CategoriaList { get; set; } = new();
        public Index(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            CategoriaList = await _context.Categorias!.ToListAsync();
            return Page();
        }
    }
}