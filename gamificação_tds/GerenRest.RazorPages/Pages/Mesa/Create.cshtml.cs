using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public MesaModel MesaModel { get; set; } = new();
        public List<MesaModel>? ListMesa { get; set; }
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();
            
            ListMesa = await _context.Mesas!.ToListAsync();
            if(ListMesa != null) {
                foreach(var listMesa in ListMesa) {
                    if(listMesa.Numero == MesaModel.Numero) {
                        TempData["ErroMesa"] = "Número de mesa repetido!";
                        return RedirectToPage("/Mesa/Create");
                    }
                }
            }
            if(MesaModel.Numero == null){
                TempData["ErroMesa"] = "Número de mesa Não informado!";
                        return RedirectToPage("/Mesa/Create");
            }
            MesaModel.Ocupada = "Livre";
            MesaModel.HoraAbertura = DateTime.Now;

            try {
                _context.Add(MesaModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Mesa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }

    }
}