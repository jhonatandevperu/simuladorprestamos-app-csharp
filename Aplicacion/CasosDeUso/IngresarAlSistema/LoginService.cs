#region Header
// Creado por: Christian
// Fecha: 29/05/2018 09:08
// Actualizado por ultima vez: 29/05/2018 09:08
#endregion

using System;
using Dominio.IdentidadAcceso.Identidad;
using Infraestructura.Persistencia.Vanilla.IdentidadAcceso.Identidad;

namespace Aplicacion.CasosDeUso.IngresarAlSistema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LoginService
    {
        private readonly AutenticacionService _autenticacionService;
        
        public LoginService(AutenticacionService autenticacionService)
        {
            this._autenticacionService = autenticacionService;
        }
        
        public LoginOutput Login(string nombreUsuario, string clave)
        {
            try
            {
                var descriptorUsuario = this._autenticacionService.Autenticar(nombreUsuario, clave);
                return new LoginOutput(descriptorUsuario.NombreUsuario, descriptorUsuario.Nombre);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}