using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperFrias.Contexts;
using SuperFrias.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperFrias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        public ProductosController(ConexionSQLServer context) {
            BD = context;
        }

        private readonly ConexionSQLServer BD;

        // GET: api/<ProductosController>
        [HttpGet]
        [Route("ObtenerProductos")]
        public async Task<IEnumerable<ProductoResponse>> Obtenerproducto()
        {
            List<Producto> productos = await  BD.Producto.ToListAsync();
            List<Categoria> categorias = await BD.Categoria.ToListAsync();

            List<ProductoResponse> result = new List<ProductoResponse>();

            foreach (var prod in productos) {
                ProductoResponse nuevo = new ProductoResponse();
                nuevo.Id = prod.Id;
                nuevo.nombre = prod.nombre;
                nuevo.precio = prod.precio;
                nuevo.Id_categoria = prod.Id_categoria;

                foreach (var categoria in categorias) { 
                    if(categoria.Id == nuevo.Id_categoria)
                    {
                        nuevo.nombre_categoria = categoria.nombre;
                        nuevo.color_categoria = categoria.color;
                        nuevo.url_categoria = categoria.imagen;
                        break;
                    }
                }
                result.Add(nuevo);
            }

            return result;
        }

        // GET api/<ProductosController>/5
        //ObtenerProdFiltado
        [HttpGet("categoria/{id}")]
        
        public async Task<IEnumerable<Producto>> Get(int id)
        {
            List<Producto> res = new List<Producto>();
            List <Producto> list = await BD.Producto.ToListAsync();

            foreach (Producto item in list) { 
                if(item.Id_categoria == id)
                {
                    res.Add(item);
                }
            }

            return res;
        }

        // PUT api/<ProductosController>/5
        //Crear Producto
        [HttpPost]
        public async Task<string> PostProducto(ProductoInput producto)
        {
            Producto nuevo = new Producto();
            nuevo.nombre = producto.nombre;
            nuevo.precio = producto.precio;
            nuevo.Id_categoria = producto.Id_categoria;
             BD.Add(nuevo);
            await BD.SaveChangesAsync();
            return "OK";
        }
        // POST api/<ProductosController>
        [HttpPost("update")]
        
        public async Task<string> PostProductoUpdate(Producto producto)
        {
            BD.Entry(producto).State=EntityState.Modified;

            try
            {
                await BD.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!productoExists((int)producto.Id))
                {
                    return "Fail";
                }
                else
                {
                    throw;
                }
            }

            return "OK";
        }

        private bool productoExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
