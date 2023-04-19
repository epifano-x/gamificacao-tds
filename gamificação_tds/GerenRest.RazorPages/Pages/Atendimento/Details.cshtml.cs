using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Details : PageModel
    {
        private readonly AppDbContext _context;
        public AtendimentoModel AtenModel { get; set; } = new();
        public Details(AppDbContext context)
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
    }
}