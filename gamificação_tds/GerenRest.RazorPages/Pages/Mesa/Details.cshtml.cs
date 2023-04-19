using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Details : PageModel
    {
        private readonly AppDbContext _context;
        public MesaModel MesaModel { get; set; } = new();

        public List<MesaModel> MesaModels { get; set; } = new();
        public AtendimentoModel AtenModel { get; set; } = new();
        public Details(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Mesas == null) {
                return NotFound();
            }

            var mesaModel = await _context.Mesas.FirstOrDefaultAsync(e => e.MesaID == id);

            if(mesaModel == null) {
                return NotFound();
            }

            MesaModel = mesaModel;

            return Page();
        }
    }
}