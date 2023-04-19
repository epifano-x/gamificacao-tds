using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ProdutoModel ProdModel { get; set; } = new();
        public List<CategoriaModel>? CatModel { get; set; }
        [BindProperty]
        public int? CatId { get; set; }
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            ProdModel.Categoria = await _context.Categorias!.FindAsync(CatId);

            try {
                _context.Add(ProdModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produto/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync() {
            CatModel = await _context.Categorias!.ToListAsync();

            if (CatModel.Count == 0) {
                TempData["ErroCategoria"] = "Não há categorias disponíveis!";
                return RedirectToPage("/Produto/Index");
            }
            
            return Page();
        }
    }
}