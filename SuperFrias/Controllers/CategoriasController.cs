using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperFrias.Contexts;
using SuperFrias.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperFrias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public  CategoriasController(ConexionSQLServer contexts )
        {
            BD= contexts;
        }
        private readonly ConexionSQLServer BD;

        // GET: api/<CategoriasController>
        [HttpGet]
        [Route("ObtenerCategorias")]
        public async Task<IEnumerable<Categoria>> ObtenerCategoria()
        {
            return await BD.Categoria.ToListAsync();
        }

    }
}
