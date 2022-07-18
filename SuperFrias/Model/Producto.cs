using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SuperFrias.Model
{
    public class Producto
    {
        public int? Id { get; set; }
        public int? Id_categoria { get; set; }
        public string? nombre { get; set; }
        public double? precio { get; set; }

    }
}
