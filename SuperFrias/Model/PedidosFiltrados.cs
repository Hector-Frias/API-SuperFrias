namespace SuperFrias.Model
{
    public class PedidosFiltrados
    {
        public int? id_client {get; set;}
        public DateTime? fechaDesde { get; set; }
        public DateTime? fechaHasta { get; set; }
        public double? precioDesde { get; set; }
        public double? precioHasta { get; set; }
    }
}
