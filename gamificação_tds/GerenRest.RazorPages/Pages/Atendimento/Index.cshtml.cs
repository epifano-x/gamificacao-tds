using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        public List<AtendimentoModel> AtenModel { get; set; } = new();
        public Index(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            AtenModel = await _context.Atendimentos!
                .Include(p => p.ListaProdutos)
                .Include(k => k.GarconResponsavel)
                .Include(l => l.MesaAtendida)
                .ToListAsync();

            return Page();
        }
    }
}