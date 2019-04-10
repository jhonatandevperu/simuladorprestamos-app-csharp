namespace Dominio.IdentidadAcceso.Acceso
{
    public interface IRolRepository
    {
        Rol RolConNombre(string nombreRol);
        bool ExisteUsuarioEnRol(int usuarioId, int rolId);
    }
}