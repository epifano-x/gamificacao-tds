using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public MesaModel? MesaModel { get; set; }
        public AtendimentoModel? Atendimentos { get; set; }
        public Edit(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> OnGetAsync(int? id) {
            MesaModel = await _context.Mesas!.FindAsync(id);
            MesaModel!.HoraAbertura = null;
            MesaModel!.Ocupada = "Livre";
           
              
            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Mesa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}