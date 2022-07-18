using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperFrias.Contexts;
using SuperFrias.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperFrias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public ClientesController(ConexionSQLServer contexts)
        {
            BD = contexts;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        [Route("ObtenerCLientes")]
        public async Task<IEnumerable<Cliente>> ObtenerCLientes()
        {
            return await BD.Cliente.ToListAsync();
        }

        private ConexionSQLServer BD;

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {

            var cliente = await BD.Cliente.FindAsync(id);
            if (cliente==null)
            {
                return NotFound();
            }

            


            return cliente;
        }
    }
}
