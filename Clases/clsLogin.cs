using Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Parcial3.Models.LibLogin;

namespace Parcial3.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        public DBExamen3Entities DBExamen = new DBExamen3Entities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }

        public bool ValidarAdmin()
        {
            try
            {
                Administrador Admin = DBExamen.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario);
                if (Admin == null)
                {
                    //si no existe swe retorna error
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                else
                {

                    return true;
                }


            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                Administrador admin = DBExamen.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);
                if (admin == null)
                {
                    //la clave no coincide NO ES IGUAL
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarAdmin() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);

                return DBExamen.Administradors
                       .Where(a => a.Usuario == login.Usuario && a.Clave == login.Clave)
                       .Select(a => new LoginRespuesta
                       {
                           Usuario = a.Usuario,
                           Autenticado = true,
                           Token = token,
                           Mensaje = ""
                           
                       });
            }
            else
            {
                List<LoginRespuesta> List = new List<LoginRespuesta>();
                List.Add(loginRespuesta); // Esta ya tiene el mensaje de error correspondiente
                return List.AsQueryable();
            }
        }

    }
}