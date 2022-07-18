namespace SuperFrias.Model
{
    public class PedidoInput
    {
        public int id_client { get; set; }
        public DateTime fecha { get; set; }
        public List<DetallePedidoInput> detalle { get; set; }
    }
}
