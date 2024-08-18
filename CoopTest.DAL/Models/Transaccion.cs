﻿namespace CoopTest.DAL.Models
{
    public class Transaccion
    {
        public string Id { get; set; } 
        public string IdCliente { get; set; }
        public string IdFondo { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public decimal Monto { get; set; }
    }
}
