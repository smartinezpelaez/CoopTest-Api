namespace CoopTest.DAL.Models
{
    public class FondoVinculado
    {
        public string IdFondo { get; set; }
        public string Nombre { get; set; }
        public decimal MontoMinimo { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaVinculacion { get; set; }
    }
}
