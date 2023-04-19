using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        public List<ProdutoModel> ProdModel { get; set; } = new();
        public Index(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            ProdModel = await _context.Produtos!.Include(p => p.Categoria).ToListAsync();
            return Page();
        }
    }
}