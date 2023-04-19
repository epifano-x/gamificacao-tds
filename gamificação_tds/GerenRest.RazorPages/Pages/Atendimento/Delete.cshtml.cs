using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public AtendimentoModel AtenModel { get; set; } = new();
        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Atendimentos == null) {
                return NotFound();
            }

            var atenModel = await _context.Atendimentos
                                        .Include(p => p.GarconResponsavel)
                                        .Include(k => k.ListaProdutos)
                                        .Include(l => l.MesaAtendida)
                                        .FirstOrDefaultAsync(e => e.AtendimentoID == id);

            if(atenModel == null) {
                return NotFound();
            }

            AtenModel = atenModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var atenToDelete = await _context.Atendimentos!.FindAsync(id);

            if(atenToDelete == null) {
                return NotFound();
            }

            try {
                _context.Atendimentos.Remove(atenToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Atendimento/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}