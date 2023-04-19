using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public AtendimentoModel AtenModel { get; set; } = new();
        public List<GarconModel>? GarconModel { get; set; }
        public List<MesaModel>? MesaModel { get; set; }
        public List<ProdutoModel>? ProdModel { get; set; }
        [BindProperty]
        public int? GarconId { get; set; }
        [BindProperty]
        public int? MesaId { get; set; }
        [BindProperty]
        public int? ProdId { get; set; }
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return Page();

            int[] prodConsumidos = Request.Form["ProdSelec"].Select(int.Parse!).ToArray();
            
            if(prodConsumidos.Length == 0) {
                TempData["ErroSelecaoProd"] = "Nenhum produto foi selecionado!";
                return RedirectToPage("/Atendimento/Create");
            }

            List<ProdutoModel>? listProds = new();
            AtenModel.PrecoTotal = 0;

            foreach(int prodID in prodConsumidos) {
                ProdutoModel? prodModel = await _context.Produtos!.FindAsync(prodID);
                listProds.Add(prodModel!);
                AtenModel.PrecoTotal += prodModel!.Preco*AtenModel.quantidade;
            }
            
            AtenModel.ListaProdutos = listProds;
            AtenModel.GarconResponsavel = await _context.Garcons!.FindAsync(GarconId);
            AtenModel.HorarioAtendimento = DateTime.Now;

            var mesaSelecionada = await _context.Mesas!.FindAsync(MesaId);
            AtenModel.MesaAtendida = mesaSelecionada;
            mesaSelecionada!.Ocupada = "Ocupada";
            mesaSelecionada!.HoraAbertura = DateTime.Now;

            try {
                _context.Add(AtenModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Atendimento/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync() {

            GarconModel = await _context.Garcons!.ToListAsync();

            if (GarconModel.Count == 0) {
                TempData["ErroGarcon"] = "Não há garçons disponíveis!";
                return RedirectToPage("/Atendimento/Index");
            }

            MesaModel = await _context.Mesas!.ToListAsync();

            if (MesaModel.Count == 0) {
                TempData["ErroMesaRegistro"] = "Não há mesas registradas!";
                return RedirectToPage("/Atendimento/Index");
            }

            int countMesasOcupadas = 0;
            foreach(var mesaModel in MesaModel) {
                if(mesaModel.Ocupada == "Ocupada") {
                    countMesasOcupadas++;
                }
            }

            if (countMesasOcupadas == 0) {
                TempData["ErroMesasOcupadas"] = "Não ha mesas sendo ocupadas ou com registro de comanda!";
                return RedirectToPage("/Atendimento/Index");
            }

            ProdModel = await _context.Produtos!.ToListAsync();

            if (ProdModel.Count == 0) {
                TempData["ErroProduto"] = "Não há produtos registrados!";
                return RedirectToPage("/Atendimento/Index");
            }
            
            return Page();
        }
    }
}