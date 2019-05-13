using angular.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Veleibol.Models;

namespace Veleibol.Controllers
{
    [Route("api/Puntaje")]
    public class PuntajeController : Controller
    {
        const int VENTAJA_DE_DOS = 2;
        const int PUNTOS_PARA_GANAR = 25;

        private DataContext db = new DataContext();

   
    
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] Puntuacion puntuacionEquipo)
        {
            try
            {
                db.PuntuacionEquipos.Add(puntuacionEquipo);
                db.SaveChanges();
                return Ok(puntuacionEquipo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Puntuacion puntuacionEquipo)
        {
            try
            {
                db.Entry(puntuacionEquipo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Ok(puntuacionEquipo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpDelete("eliminar/{equipoId}")]
        public async Task<IActionResult> Eliminar(int equipoId)
        {
            try
            {

                db.Remove(db.PuntuacionEquipos.Find(equipoId));
                db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("obtenerGanador/{idEquipo1}/{idEquipo2}")]
        public async Task<IActionResult> ObtenerGanador(int idEquipo1, int idEquipo2)
        {
            
            try
            {
                int puntajeEquipo1 = ObtenerPuntajeActual(idEquipo1);
                int puntajeEquipo2 = ObtenerPuntajeActual(idEquipo2);

                if (puntajeEquipo1 >= PUNTOS_PARA_GANAR && 
                   (puntajeEquipo1 - puntajeEquipo2) == VENTAJA_DE_DOS)
                    return Ok(idEquipo1);
                else
                {
                    if (puntajeEquipo2 >= PUNTOS_PARA_GANAR && 
                       (puntajeEquipo2 - puntajeEquipo1) == VENTAJA_DE_DOS)
                        return Ok(idEquipo2);
                }
                return Ok("No hay ganador");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        public int ObtenerPuntajeActual(int equipoId)
        {
            try
            {
                var equipoPuntuacion = db.PuntuacionEquipos.SingleOrDefault(p => p.equipoId == equipoId);
                return equipoPuntuacion.puntaje;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

    }
}