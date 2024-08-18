namespace CoopTest.BLL.DTOs
{
    public class TransaccionDTO
    {
        public string Id { get; set; } // Correspondiente al _id en MongoDB
        public string IdCliente { get; set; }
        public string IdFondo { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public decimal Monto { get; set; }
    }
}
