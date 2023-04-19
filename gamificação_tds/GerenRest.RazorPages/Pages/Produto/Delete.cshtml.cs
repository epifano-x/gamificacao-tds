using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ProdutoModel ProdModel { get; set; } = new();
        public Delete(AppDbContext context)
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
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var prodToDelete = await _context.Produtos!.FindAsync(id);

            if(prodToDelete == null) {
                return NotFound();
            }

            try {
                _context.Produtos.Remove(prodToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produto/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}