using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Details : PageModel
    {
        private readonly AppDbContext _context;
        public ProdutoModel ProdModel { get; set; } = new();
        public Details(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Mesas == null) {
                return NotFound();
            }

            var prodModel = await _context.Produtos!.Include(p => p.Categoria).FirstOrDefaultAsync(e => e.ProdutoID == id);

            if(prodModel == null) {
                return NotFound();
            }

            ProdModel = prodModel;

            return Page();
        }
    }
}