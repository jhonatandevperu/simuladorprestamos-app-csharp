using Dominio.IdentidadAcceso.Identidad;

namespace Dominio.IdentidadAcceso.Acceso
{
    public class AutorizacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        
        public AutorizacionService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
        {
            this._usuarioRepository = usuarioRepository;
            this._rolRepository = rolRepository;
        }

        public bool EstaElUsuarioEnElRol(string nombreUsuario, string nombreRol)
        {
            var usuario = this._usuarioRepository.UsuarioConNombreUsuario(nombreUsuario);
            return usuario != null && this.EstaElUsuarioEnElRol(usuario, nombreRol);
        }
        
        public bool EstaElUsuarioEnElRol(Usuario usuario, string nombreRol)
        {
            //Verificar si el usuario esta activo
            //Determinar si tiene el rol correspondiente
            var rol = this._rolRepository.RolConNombre(nombreRol);
            if(rol != null)
            {
                return this._rolRepository.ExisteUsuarioEnRol(usuario.Id, rol.Id);
            }

            return false;
        }
    }
}