namespace Dominio.IdentidadAcceso.Identidad
{
    public interface IUsuarioRepository
    {
        Usuario UsuarioAPartirDeCredenciales(string nombreUsuario, string clave);
        Usuario UsuarioConNombreUsuario(string nombreUsuario);
        Usuario UsuarioConId(int id);
    }
}