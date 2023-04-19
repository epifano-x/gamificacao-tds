using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public CategoriaModel categoriaModel { get; set; } = new();
        public Create(AppDbContext context)
        {
            _context = context;
        }
  
        
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(categoriaModel.Nome == null){
                TempData["ErroCategoria"] = "Nome não informado";
                    return RedirectToPage("/Categoria/Create");
            }else if(categoriaModel.Descricao == null){
                TempData["ErroCategoria"] = "Descrião não informado";
                    return RedirectToPage("/Categoria/Create");
            }



            if(!ModelState.IsValid)
                return Page();

            try {
                _context.Add(categoriaModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Categoria/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}