using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new();
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(GarconModel.Nome == null){
                    TempData["ErroGarcon"] = "Nome não informado!";
                        return RedirectToPage("/Garcon/Create");
            }else if(GarconModel.Sobrenome == null){
                    TempData["ErroGarcon"] = "Sobrenome não informado!";
                        return RedirectToPage("/Garcon/Create");
            }else if(GarconModel.NumIdentificao == null){
                    TempData["ErroGarcon"] = "Número não informado!";
                        return RedirectToPage("/Garcon/Create");
            }else if(GarconModel.Telefone == null){
                    TempData["ErroGarcon"] = "Telefone não informado!";
                        return RedirectToPage("/Garcon/Create");
            }

            if(!ModelState.IsValid)
                return Page();

            try {
                _context.Add(GarconModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Garcon/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}