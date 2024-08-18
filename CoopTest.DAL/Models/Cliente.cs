namespace CoopTest.DAL.Models
{
    public class Cliente
    {
        public string Id { get; set; } 
        public string Nombre { get; set; }
        public decimal Saldo { get; set; }
        public List<FondoVinculado> Fondos { get; set; } = new List<FondoVinculado>();
    }
}
