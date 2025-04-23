using Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Parcial3.Clases
{
    public class clsEventos
    {
        private DBExamen3Entities dbExamen = new DBExamen3Entities();

        public Evento evento { get; set; }

        public string Insertar()
        {
            try
            {
                dbExamen.Eventos.Add(evento);
                dbExamen.SaveChanges();
                return "Evento registrado con exito";
            }
            catch (Exception ex)
            {
                return "Error al registrar el evento: " + ex.Message;

            }
        }

        public string Actualizar()
        {
            try
            {
               
                Evento even = Consultar(evento.TipoEvento);
                if (even == null)
                {
                    return "evento no existe";
                }

                dbExamen.Eventos.AddOrUpdate(evento); 
                dbExamen.SaveChanges(); 
                return "evento actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el evento: " + ex.Message; 
            }
        }

        public Evento Consultar(string id)
        {

            Evento empLeado = dbExamen.Eventos.FirstOrDefault(e => e.TipoEvento == id);
            return empLeado;
        }

        public string Eliminar()
        {

            try
            {
                Evento even = Consultar(evento.TipoEvento);
                if (even == null)
                {
                    return "El evento No Existe";
                }
          
                dbExamen.Eventos.Remove(even);
                dbExamen.SaveChanges();
                return "evento eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "error al eliminar el evento" + ex.Message;
            }
        }

        public List<Evento> ConsultarTodosEventos()
        {
            return dbExamen.Eventos
                .OrderBy(e => e.idEventos)
                .ToList();
        }
    }
}