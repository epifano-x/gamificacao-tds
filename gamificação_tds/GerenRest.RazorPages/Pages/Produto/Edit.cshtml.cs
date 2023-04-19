using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ProdutoModel ProdModel { get; set; } = new();
        public List<CategoriaModel>? CatModel { get; set; }
        [BindProperty]
        public int? CatId { get; set; }
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var prodModel = await _context.Produtos!.Include(p => p.Categoria).FirstOrDefaultAsync(e => e.ProdutoID == id);

            if (prodModel == null)
            {
                return NotFound();
            }

            CatId = prodModel.Categoria!.CategoriaID;
            CatModel = await _context.Categorias!.ToListAsync();
            ProdModel = prodModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var prodToUpdate = await _context.Produtos!.FindAsync(id);

            if (prodToUpdate == null)
            {
                return NotFound();
            }

            prodToUpdate.Nome = ProdModel.Nome;
            prodToUpdate.Descricao = ProdModel.Descricao;
            prodToUpdate.Preco = ProdModel.Preco;
            prodToUpdate.Categoria = await _context.Categorias!.FindAsync(CatId);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produto/Index");
            }
            catch (DbUpdateException)
            {
                return Page();
            }
        }
    }
}