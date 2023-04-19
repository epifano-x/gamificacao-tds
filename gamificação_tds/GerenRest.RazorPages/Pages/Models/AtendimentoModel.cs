namespace GerenRest.RazorPages.Models {
    public class AtendimentoModel {
        public int? AtendimentoID { get; set; }
        public MesaModel? MesaAtendida { get; set; }
        public GarconModel? GarconResponsavel { get; set; }
        public List<ProdutoModel>? ListaProdutos { get; set; }
        public DateTime? HorarioAtendimento { get; set; }
        public float? PrecoTotal { get; set; }
        public int quantidade {get; set;}
    }
}