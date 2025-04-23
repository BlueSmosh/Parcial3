using Parcial3.Clases;
using Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Parcial3.Controllers
{
    [RoutePrefix("api/Eventos")]
    public class EventosController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodosEventos")]
        public List<Evento> ConsultarTodosEventos()
        {
            clsEventos Evento = new clsEventos();
            return Evento.ConsultarTodosEventos();

        }
        [HttpGet]
        [Route("ConsultarPorEvento")]
        public Evento ConsultarPorId(string id)
        {
            clsEventos Evento = new clsEventos();
            return Evento.Consultar(id);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Evento evento)
        {
            clsEventos Evento = new clsEventos();
            Evento.evento = evento;
            return Evento.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Evento evento)
        {
            clsEventos objEvento = new clsEventos();
            objEvento.evento = evento;
            return objEvento.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Evento evento)
        {
            clsEventos objEvento = new clsEventos();
            objEvento.evento = evento;
            return objEvento.Eliminar();
        }
    }
}