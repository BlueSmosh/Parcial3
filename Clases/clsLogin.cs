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
                // Crea la clase encriptación
                //clsCypher cifrar = new clsCypher();
                //se lee el usuario en base de datos
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
                    //el usuario existe se lee el salt
                    // byte[] arrBytesSalt = Convert.FromBase64String(Admin.Salt);
                    //se envia a encriptar la clave
                    //string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                    //asigno la clave encriptada en el objecto login
                    //login.Clave = ClaveCifrada;
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
                return from A in DBExamen.Set<Administrador>()
                       join EV in DBExamen.Set<Evento>()
                       on A.Documento equals EV.TipoEvento
                       where A.Usuario == login.Usuario &&
                               A.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = A.Usuario,
                           Autenticado = true,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> List = new List<LoginRespuesta>();
                List.Add(loginRespuesta);
                return List.AsQueryable();
            }
        }
    }
}