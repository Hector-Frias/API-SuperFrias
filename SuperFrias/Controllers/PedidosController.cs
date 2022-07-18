using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperFrias.Contexts;
using SuperFrias.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperFrias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        public PedidosController(ConexionSQLServer contexts)
        {
            BD = contexts;
        }

        private ConexionSQLServer BD;

        // GET: api/<PedidosController>
        [HttpGet]
        [Route("ObtenerPedidos")]
        public async Task<IEnumerable<PedidoResponse>> ObtenerPedidos()
        {
            List<Pedidos> pedidos = await BD.Pedidos.ToListAsync();
            List <PedidoResponse> result = new List<PedidoResponse>();

            List<DetallesPedidos> detalles= await BD.DetallesPedidos.ToListAsync(); 


           foreach (var pedido in pedidos)
            {
                PedidoResponse nuevo = new PedidoResponse();
                nuevo.id_client = pedido.id_client;
                nuevo.id = pedido.id;   
                nuevo.fecha=pedido.fecha;
                nuevo.detalle = new List<DetallesPedidos>();
                nuevo.total = 0;
                foreach (var det in detalles )
                {
                    if (nuevo.id==det.id_pedidos)
                    {
                        nuevo.detalle.Add(det);
                        nuevo.total += (det.precio_historico * det.cant);
                    }
                }

                result.Add(nuevo);
            }


            return result;
        }

        //Crear Pedidos
        [HttpPost]
        [Route("CrearPedidos")]
        public async Task<string> PostPedidos(PedidoInput inPedido)
        {
            PedidoResponse pedidos = new PedidoResponse();
            pedidos.id_client = inPedido.id_client;
            List<DetallesPedidos> items = new List<DetallesPedidos>();
            foreach (var item in inPedido.detalle)
            {
                DetallesPedidos nn = new DetallesPedidos();
                nn.id_producto = item.id_producto;
                nn.cant = item.cant;
                nn.precio_historico = item.precio_historico;
                items.Add(nn);
            }
            pedidos.detalle = items;
            pedidos.fecha = inPedido.fecha;
            

            Pedidos nuevoPedido = new Pedidos();
            nuevoPedido.id_client = (int)pedidos.id_client;
            nuevoPedido.fecha = pedidos.fecha;
            List<DetallesPedidos> detallesNuevos = pedidos.detalle;

            BD.Add(nuevoPedido);
            await BD.SaveChangesAsync();
            foreach (var detalle in detallesNuevos)
            {
                detalle.id = 0;
                detalle.id_pedidos = nuevoPedido.id;
                BD.Add(detalle);
            }

            await BD.SaveChangesAsync();
            return "OK";
        }



        // POST api/<PedidosController
        [HttpPost]
        [Route("ObtenerPedidosFiltrados")]
        public async Task<IEnumerable<PedidoResponse>> Post([FromBody] PedidosFiltrados value)
        {
            List<PedidoResponse> all = (List<PedidoResponse>) await ObtenerPedidos();
            List<PedidoResponse> res = new List<PedidoResponse>();

            foreach (var item in all) {
                bool esValido = true;

                if (value.id_client != null) {
                    if (value.id_client != item.id_client) {
                        esValido = false;
                    }
                }

                if (esValido && value.fechaDesde != null)
                {
                    if (item.fecha < value.fechaDesde) {
                        esValido = false;
                    }
                }

                if (esValido && value.fechaHasta != null)
                {
                    if (item.fecha > value.fechaHasta)
                    {
                        esValido = false;
                    }
                }

                if (esValido && value.precioDesde != null)
                {
                    if (item.total < value.precioDesde)
                    {
                        esValido = false;
                    }
                }

                if (esValido && value.precioHasta != null)
                {
                    if (item.total > value.precioHasta)
                    {
                        esValido = false;
                    }
                }

                if (esValido) {
                    res.Add(item);
                }
            }

            return res;
        }
    }
}
