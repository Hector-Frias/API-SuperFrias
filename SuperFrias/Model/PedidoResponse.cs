namespace SuperFrias.Model
{
    public class PedidoResponse
    {
        public int? id { get; set; }
        public int id_client { get; set; }
        public DateTime fecha { get; set; }
        public List<DetallesPedidos> detalle { get; set;  }
        public double total { get; set; }
    }
}
