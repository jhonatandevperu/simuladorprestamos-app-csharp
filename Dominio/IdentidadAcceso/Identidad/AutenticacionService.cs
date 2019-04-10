using System;

namespace Dominio.IdentidadAcceso.Identidad
{
    public class AutenticacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacionService(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        public DescriptorUsuario Autenticar(string nombreUsuario, string clave)
        {
            if(nombreUsuario == null)
            {
                throw new Exception("El nombre de usuario no debe ser nulo.");
            }

            if(clave == null)
            {
                throw new Exception("La clave no debe ser nula.");
            }

            var usuario = this._usuarioRepository.UsuarioAPartirDeCredenciales(nombreUsuario, clave);

            if(usuario != null)
            {
                return usuario.DescriptorUsuario;
            }
            
            //Mejorar.
            throw new Exception("El usuario no fue encontrado.");
        }
    }
}