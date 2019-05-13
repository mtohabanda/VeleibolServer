using angular.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Veleibol.Models;
using Microsoft.AspNetCore.Cors;

namespace Veleibol.Controllers
{
    [Route("api/Equipo")]
    [EnableCors("CorsPolicy")]
    public class EquipoController : Controller
    {
        private DataContext db = new DataContext();

        [Produces("application/json")]
        [HttpGet("existeEquiposParaIniciarPartido")]
        public int ExisteEquiposParaIniciarPartido() {
            int existeEquipo = 0;
            try
            {
                existeEquipo = db.Equipos.ToList().Count();
                return existeEquipo;

            }
            catch (Exception e)
            {
                return existeEquipo;
            }
        }

        [Produces("application/json")]
        [HttpGet("obtenerEquipoInfo/{equipoId}")]
        public async Task<IActionResult> ObtenerEquipoInfo(int equipoId)
        {
            try
            {

                var equipo = from e in db.Equipos
                             join p in db.PuntuacionEquipos on e.equipoId equals p.equipoId
                             where e.equipoId == equipoId
                             select new
                             {   equipoId = e.equipoId,
                                 nombre = e.nombre,
                                 puntajeActual = p.puntaje
                             };

                return Ok(equipo.SingleOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("obtenerEquipos")]
        public async Task<IActionResult> ObtenerEquipos()
        {
            try
            {

                var equipos = from e in db.Equipos
                             join p in db.PuntuacionEquipos on e.equipoId equals p.equipoId
                             select new
                             {   equipoId = e.equipoId,
                                 nombre = e.nombre,
                                 puntajeActual = p.puntaje
                             };

                return Ok(equipos.ToList());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] Equipo equipo) {
            try
            {
                db.Equipos.Add(equipo);
                db.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("eliminar/{equipoId}")]
        public async Task<IActionResult> Eliminar(int equipoId)
        {
            try
            {

                db.Remove(db.Equipos.Find(equipoId));
                db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}