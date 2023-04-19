using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Mesa;

public class Comanda : PageModel
{
    private readonly AppDbContext _context;
    [BindProperty]
    public ClienteModel ClienteModel { get; set; } = new();
    public MesaModel? MesaModel { get; set; }
    public Comanda(AppDbContext context)
    {
        _context = context;
        
    }
    
    public async Task<IActionResult> OnPostAsync(int id)
    {
        var mesaModel = await _context.Mesas!.FirstOrDefaultAsync(e => e.MesaID == id);
        
        if(mesaModel!.Ocupada == "Ocupada"){
            TempData["ErroMesa"] = "Mesa ja em uso!";
                return RedirectToPage("/Mesa/Comanda");
        }else{
            mesaModel.Ocupada = "Ocupada";
            mesaModel.HoraAbertura = DateTime.Now;

        }
        if(ClienteModel.Nome == null){
                    TempData["ErroMesa"] = "Nome não informado!";
                        return RedirectToPage("/Mesa/Comanda");
        }else if(ClienteModel.Sobrenome == null){
                    TempData["ErroMesa"] = "Sobrenome não informado!";
                        return RedirectToPage("/Mesa/Comanda");
        }else if(ClienteModel.Telefone == null){
                    TempData["ErroMesa"] = "Telefone não informado!";
                        return RedirectToPage("/Mesa/Comanda");
        }  
            

        if(!ModelState.IsValid)
            return Page();

        try {
            _context.Add(ClienteModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Mesa/Index");
        } catch(DbUpdateException) {
            return Page();
        }
    }
}